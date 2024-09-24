using System;
using System.Collections.Generic;
using System.Linq;
using Extype;
using LiveChartsCore.SkiaSharpView.Avalonia;
using IndoorMap.Services.Abstract;
using IndoorMap.Views;
using IndoorMap.Models;
using DomainModel;
using NLog;
using Prism.Commands;
using Prism.Dialogs;
using System.IO;
using System.Diagnostics;

namespace IndoorMap.ViewModels
{
    public class FlightMissionViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetLogger(nameof(FlightMissionViewModel));
        
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;
        private readonly IConfig _config;

        public ArucoMarkerLine[] ArucoMarkersSet1 { get; }

        public ArucoMarkerLine[] ArucoMarkersSet2 { get; }

        public ArucoMarkerLine[] ArucoMarkersSet3 { get; }

        /// <summary>
        /// Gets the command to add the point to the flight mission.
        /// </summary>
        public DelegateCommand<object> AddPointCommand { get; set; }
        
        /// <summary>
        /// Gets the command to remove the point from the flight mission.
        /// </summary>
        public DelegateCommand<object> RemovePointCommand { get; }
        
        /// <summary>
        /// Gets the command to start the flight mission.
        /// </summary>
        public DelegateCommand StartMissionCommand { get; }
        
        /// <summary>
        /// Gets the command to clear the flight mission.
        /// </summary>
        public DelegateCommand ClearMissionCommand { get; }

        public FlightMissionViewModel(
            INotificationService notificationService,
            IDialogService dialogService,
            IConfig config)
        {
            _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
            _dialogService = dialogService.ThrowIfNull(nameof(dialogService));
            _config = config.ThrowIfNull(nameof(config));

            AddPointCommand = new DelegateCommand<object>(AddPoint);
            RemovePointCommand = new DelegateCommand<object>(RemovePoint);
            StartMissionCommand = new DelegateCommand(StartMission);
            ClearMissionCommand = new DelegateCommand(ClearMission);

            ArucoMarkersSet1 = GetArucoMarkerSet(0, 29);
            ArucoMarkersSet2 = GetArucoMarkerSet(30, 59);
            ArucoMarkersSet3 = GetArucoMarkerSet(60, 89);

            EnsureFlightMissionFileExists();
        }

        private ArucoMarkerLine[] GetArucoMarkerSet(int from, int to)
        {
            var arucoSet = new List<ArucoMarkerLine>();
            var z = 2;

            for (int i = from + 2; i <= to; i += 3)
            {
                var marker1 = new ArucoMarker(i - 2, z, Guid.NewGuid().ToString());
                var marker2 = new ArucoMarker(i - 1, z, Guid.NewGuid().ToString());
                var marker3 = new ArucoMarker(i, z, Guid.NewGuid().ToString());

                var line = new ArucoMarkerLine(marker1, marker2, marker3);
                arucoSet.Add(line);
            }

            return arucoSet.ToArray();
        }
        
        public void AddPoint(object commandParam)
        {
            var arucoMarker = (ArucoMarker)commandParam;
            _notificationService.NotifyAboutSuccess("Точка {" + arucoMarker.Number + "} добавлена");
            AddPointToFile(arucoMarker);
        }

        private void AddPointToFile(ArucoMarker arucoMarker)
        {
            var fileStream = new FileStream(_config.FlightMissionFile, FileMode.Append, FileAccess.Write);
            var steamWriter = new StreamWriter(fileStream, System.Text.Encoding.ASCII);

            try
            {
                var coords = $"{arucoMarker.Number},{arucoMarker.Z}";
                steamWriter.WriteLine(coords);
            }
            catch (Exception ex)
            {
                _notificationService.NotifyAboutFailure(ex.Message);
                _logger.Error(ex, ex.Message);
            }
            finally
            {
                steamWriter.Close();
                fileStream.Close();
            }
        }

        private void RemovePoint(object commandParam)
        {
        }

        private void StartMission()
        {
            _notificationService.NotifyAboutSuccess("Полетное задание запущено");
            run_cmd("python3", _config.DroneScriptFile);
        }

        private void run_cmd(string processName, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = processName;
            start.Arguments = string.Format("{0}", args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using(Process process = Process.Start(start))
            {
                using(StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        private void ClearMission()
        {
            ClearMissionFile();
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
    }
}