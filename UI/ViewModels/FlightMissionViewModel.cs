using System;
using Extype;
using LiveChartsCore.SkiaSharpView.Avalonia;
using Glover.Services.Abstract;
using Glover.Views;
using NLog;
using Prism.Commands;
using Prism.Dialogs;

namespace Glover.ViewModels
{
    public class FlightMissionViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetLogger(nameof(FlightMissionViewModel));
        
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Gets the command to add the point to the flight mission.
        /// </summary>
        public DelegateCommand<string> AddPointCommand { get; }
        
        /// <summary>
        /// Gets the command to remove the point from the flight mission.
        /// </summary>
        public DelegateCommand<string> RemovePointCommand { get; }

        public FlightMissionViewModel(
            INotificationService notificationService,
            IDialogService dialogService)
        {
            _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
            _dialogService = dialogService.ThrowIfNull(nameof(dialogService));

            AddPointCommand = new DelegateCommand<string>(AddPoint);
            RemovePointCommand = new DelegateCommand<string>(RemovePoint);
        }

        private void AddPoint(string commandParam)
        {
        }

        private void RemovePoint(string commandParam)
        {
        }
    }
}