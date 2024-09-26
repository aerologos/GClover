using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glover.ViewModels.Base;
using Glover.Services.Abstract;
using NLog;
using Extype;
using Prism.Commands;
using Prism.Dialogs;
using DomainModel;
using Glover.Const;
using Glover.Views;


namespace Glover.ViewModels;

public class ModuleDialogViewModel : DialogViewModelBase
{
    private readonly Logger _logger = LogManager.GetLogger(nameof(ModuleDialogViewModel));
    private readonly INotificationService _notificationService;
    private readonly IConfig _config;

    private readonly string[] _allModules = 
    { 
        nameof(IndoorMapView),
        nameof(ElevationMapView),
        nameof(ArtificialHorizonView)
    };

    private readonly string[] _allRegions = 
    {
        nameof(_config.TopLeft),
        nameof(_config.TopRight),
        nameof(_config.BottomLeft),
        nameof(_config.BottomRight)
    };

    /// <summary>
    /// Gets the title of the dialog window.
    /// </summary>
    public override string Title => "Модуль";
        
    /// <summary>
    /// Gets the list of supported modules.
    /// </summary>
    public string[] Modules { get; private set; }

    private string _selectedModule;
    /// <summary>
    /// Gets or sets the selected module.
    /// </summary>
    public string SelectedModule
    {
        get => _selectedModule;
        set => SetProperty(ref _selectedModule, value);
    }
        
    /// <summary>
    /// Gets the list of available regions.
    /// </summary>
    public string[] Regions { get; private set; }

    private string _selectedRegion;
    /// <summary>
    /// Gets or sets the selected region.
    /// </summary>
    public string SelectedRegion
    {
        get => _selectedRegion;
        set => SetProperty(ref _selectedRegion, value);
    }

    /// <summary>
    /// Gets the command to save the data.
    /// </summary>
    public DelegateCommand SaveCommand { get; }

    /// <summary>
    /// Instantiates the new <see cref="ModuleDialogViewModel"/> object.
    /// </summary>
    /// <param name="notificationService">The user notification service.</param>
    public ModuleDialogViewModel(
        INotificationService notificationService,
        IConfig config)
    {
        _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
        _config = config.ThrowIfNull(nameof(config));

        SaveCommand = new DelegateCommand(Save, CanExecuteSaveCommand())
            .ObservesProperty(() => SelectedModule)
            .ObservesProperty(() => SelectedRegion);
    }

    public override void OnDialogOpened(IDialogParameters parameters)
    {
        base.OnDialogOpened(parameters);

        Modules = GetAvailableModules();
        Regions = GetAvailableRegions();

        RaisePropertyChanged(nameof(Modules));
        RaisePropertyChanged(nameof(Regions));
    }

    private string[] GetAvailableModules()
    {
        var availableModules = new List<string>();

        foreach (var module in _allModules)
        {
            if (IsModuleBusy(module)) continue;

            availableModules.Add(module);
        }

        return availableModules.ToArray();
    }

    private bool IsModuleBusy(string module)
    {
        return _config.TopLeft == module
            || _config.TopRight == module
            || _config.BottomLeft == module
            || _config.BottomRight == module;
    }

    private string[] GetAvailableRegions()
    {
        var availableRegions = new List<string>();

        foreach (var region in _allRegions)
        {
            if (IsRegionBusy(region)) continue;

            availableRegions.Add(region);
        }

        return availableRegions.ToArray();
    }

    private bool IsRegionBusy(string region)
    {
        if (nameof(_config.TopLeft) == region) return !string.IsNullOrWhiteSpace(_config.TopLeft);
        else if (nameof(_config.TopRight) == region) return !string.IsNullOrWhiteSpace(_config.TopRight);
        else if (nameof(_config.BottomLeft) == region) return !string.IsNullOrWhiteSpace(_config.BottomLeft);
        else if (nameof(_config.BottomRight) == region) return !string.IsNullOrWhiteSpace(_config.BottomRight);

        return false;
    }

    private async void Save()
    {
        try
        {
            var dialogParameters = new DialogParameters
            {
                { Names.SelectedModule, SelectedModule },
                { Names.SelectedLocation, SelectedRegion },
            };

            RaiseRequestClose(new DialogResult(ButtonResult.OK) { Parameters =  dialogParameters });
        }
        catch (Exception ex)
        {
            await _notificationService.NotifyAboutErrorAsync("Что-то не так. Проверьте логи.");
            _logger.Error(ex);
        }
    }

    private Func<bool> CanExecuteSaveCommand()
    {
        return () => 
            !string.IsNullOrWhiteSpace(SelectedModule)
            && !string.IsNullOrWhiteSpace(SelectedRegion);
    }
}
