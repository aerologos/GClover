using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Extype;
using ElevationMap.Services.Abstract;
using ElevationMap.Views;
using ElevationMap.Models;
using NLog;
using Prism.Commands;
using Prism.Dialogs;
using System.IO;
using System.Diagnostics;
using ElevationMap.Operations;
using ElevationMap.Extensions;
using Avalonia.Threading;
using DomainModel;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace ElevationMap.ViewModels
{
    public class ElevationMapViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetLogger(nameof(ElevationMapViewModel));
        private static readonly SolidColorPaint lightGray = new SolidColorPaint(SKColors.LightGray);
        
        private readonly INotificationService _notificationService;
        private readonly MarkerMonitor _markerMonitor;
        private readonly IConfig _config;

        public ElevationMapViewModel(
            INotificationService notificationService,
            MarkerMonitor markerMonitor,
            IConfig config)
        {
            _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
            _markerMonitor = markerMonitor.ThrowIfNull(nameof(markerMonitor));
            _config = config.ThrowIfNull(nameof(config));
        }

        private Map _elevation;
        /// <summary>
        /// Gets or sets the elevation map object.
        /// </summary>
        public Map Elevation
        {
            get => _elevation;
            set => SetProperty(ref _elevation, value);
        }

        /// <summary>
        /// Links the view model with the map object.
        /// </summary>
        /// <param name="elevationMapControl">The elevation map control.</param>
        public void LinkElevationMap(CartesianChart elevationMapControl)
        {
            elevationMapControl.ThrowIfNull(nameof(elevationMapControl));

            Elevation = new Map(_notificationService, elevationMapControl);
            _markerMonitor.Map = Elevation;
            _markerMonitor.Start();
        }

        private void ClearMissionFile()
        {
            try
            {
                EnsureFlightMissionFileExists();

                File.WriteAllText(_config.FlightMissionFile, string.Empty);
                _notificationService.NotifyAboutSuccess("Полетное задание очищено");
            }
            catch (Exception ex)
            {
                _notificationService.NotifyAboutFailure("Доступ к файлу полетного задания ограничен");
                _logger.Error(ex, ex.Message);
            }
        }

        private void EnsureFlightMissionFileExists()
        {
            if (!File.Exists(_config.FlightMissionFile))
            {
                File.Create(_config.FlightMissionFile);
            }
        }

        public override void OnShutdown()
        {
            _markerMonitor.Stop();
            base.OnShutdown();
        }

        protected override void PerformDisposal()
        {
            _markerMonitor.Dispose();
            base.PerformDisposal();
        }
    }
}