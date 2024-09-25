using DomainModel;
using Glover.Services.Abstract;
using Extype;
using Parallops;
using System.Collections.Generic;
using System.IO;
using NLog;
using System;
using Glover.Models;

namespace Glover.Operations;

public class MarkerMonitor : RepetitiveOperationBase
{
    private readonly Logger _logger = LogManager.GetLogger(nameof(MarkerMonitor));

    private readonly INotificationService _notificationService;
    private readonly IConfig _config;

    public MarkerMonitor(
        INotificationService notificationService,
        IConfig config)
        : base(downtime: config.MonitorDowntimeInMs)
    {
        _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
        _config = config.ThrowIfNull(nameof(config));
    }

    public Map Map { get; set; }

    private int _lastNumberOfPoints = 0;
    protected override void RepeatOperation()
    {
        if (Map == null)
        {
            _logger.Warn("The map file in the monitor is not specified.");
            return;
        }

        if (!File.Exists(_config.FlightMissionFile))
        {
            _notificationService.NotifyAboutFailure($"Не найден файл {_config.FlightMissionFile}.");
            return;
        }

        var points = ReadPoints();
        if (points.Length == _lastNumberOfPoints) return;
        
        _lastNumberOfPoints = points.Length;
        Map.ClearMap();
        Map.AddMarkers(points);
        Map.RegenerateMap();
    }

    private ArucoMarker[] ReadPoints()
    {
        var points = new List<ArucoMarker>();

        var fileContent = File.ReadAllText(_config.FlightMissionFile);
        using (var reader = new StringReader(fileContent))
        while(true)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) break;

            var parts = line.Split(',');
            if (parts.Length < 2)
            { 
                _logger.Warn($"There is a line with a wrong format in {_config.FlightMissionFile}");
                continue;
            }

            var arucoNumber = int.Parse(parts[0]);
            var altitude = float.Parse(parts[1]);

            var point = new ArucoMarker(arucoNumber, altitude, Guid.NewGuid().ToString());
            points.Add(point);
        }

        return points.ToArray();
    }
}
