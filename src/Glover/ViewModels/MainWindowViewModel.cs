using System;
using Extype;
using Prism.Commands;
using Prism.Navigation.Regions;
using Prism.Dialogs;
using NLog;
using Glover.Views;
using Glover.Const;
using Glover.Services.Abstract;

namespace Glover.ViewModels
{
    /// <summary>
    /// Executes the actions happening on the <see cref="MainWindowView"/>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly Logger _logger = LogManager.GetLogger(nameof(MainWindowViewModel));

        private readonly INotificationService _notificationService;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IConfig _config;

        public DelegateCommand AddModuleCommand { get; }

        /// <summary>
        /// Instantiates the new <see cref="MainWindowViewModel"/> object.
        /// </summary>
        /// <param name="licenseService">The license service.</param>
        public MainWindowViewModel(
            INotificationService notificationService,
            IRegionManager regionManager,
            IDialogService dialogService,
            IConfig config)
        {
            _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
            _regionManager = regionManager.ThrowIfNull(nameof(regionManager));
            _dialogService = dialogService.ThrowIfNull(nameof(dialogService));
            _config = config.ThrowIfNull(nameof(config));

            _regionManager.RegisterViewWithRegion<EmptyView>(PrismRegions.TopLeft);
            _regionManager.RegisterViewWithRegion<EmptyView>(PrismRegions.TopRight);
            _regionManager.RegisterViewWithRegion<EmptyView>(PrismRegions.BottomLeft);
            _regionManager.RegisterViewWithRegion<EmptyView>(PrismRegions.BottomRight);

            _regionManager.RegisterViewWithRegion<ElevationMapView>(PrismRegions.TopLeft);
            _regionManager.RegisterViewWithRegion<ElevationMapView>(PrismRegions.TopRight);
            _regionManager.RegisterViewWithRegion<ElevationMapView>(PrismRegions.BottomLeft);
            _regionManager.RegisterViewWithRegion<ElevationMapView>(PrismRegions.BottomRight);

            _regionManager.RegisterViewWithRegion<FlightMissionView>(PrismRegions.TopLeft);
            _regionManager.RegisterViewWithRegion<FlightMissionView>(PrismRegions.TopRight);
            _regionManager.RegisterViewWithRegion<FlightMissionView>(PrismRegions.BottomLeft);
            _regionManager.RegisterViewWithRegion<FlightMissionView>(PrismRegions.BottomRight);

            if (!string.IsNullOrWhiteSpace(_config.TopLeft))
                _regionManager.RequestNavigate(PrismRegions.TopLeft, _config.TopLeft);

            if (!string.IsNullOrWhiteSpace(_config.TopRight))
                _regionManager.RequestNavigate(PrismRegions.TopRight, _config.TopRight);

            if (!string.IsNullOrWhiteSpace(_config.BottomLeft))
                _regionManager.RequestNavigate(PrismRegions.BottomLeft, _config.BottomLeft);

            if (!string.IsNullOrWhiteSpace(_config.BottomRight))
                _regionManager.RequestNavigate(PrismRegions.BottomRight, _config.BottomRight);

            AddModuleCommand = new DelegateCommand(AddModule);
        }

        /// <summary>
        /// Gets the application name with version number.
        /// </summary>
        public string AppName => "Glover";

        private void AddModule()
        {
            _dialogService.ShowDialog(nameof(ModuleDialogView), HandleDialogResult());    
        }

        private Action<IDialogResult> HandleDialogResult()
        {
            return r =>
            {
                switch (r.Result)
                {
                    case ButtonResult.OK:
                        var selectedModule = (string)r.Parameters[Names.SelectedModule];
                        var selectedRegion = (string)r.Parameters[Names.SelectedLocation];

                        _regionManager.RequestNavigate(selectedRegion, selectedModule);
                        SaveSelection(selectedRegion, selectedModule);

                        _notificationService.NotifyAboutSuccess("Модуль добавлен");
                        break;
                }
            };
        }

        private void SaveSelection(string selectedRegion, string selectedModule)
        {
            if (selectedRegion == nameof(_config.TopLeft))
            {
                _config.TopLeft = selectedModule;
            }
            else if (selectedRegion == nameof(_config.TopRight))
            {
                _config.TopRight = selectedModule;
            }
            else if (selectedRegion == nameof(_config.BottomLeft))
            {
                _config.BottomLeft = selectedModule;
            }
            else if (selectedRegion == nameof(_config.BottomRight))
            {
                _config.BottomRight = selectedModule;
            }
            
            _config.Save();
        }
    }
}