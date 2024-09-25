using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extype;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using DomainModel;
using NLog;
using Prism.Mvvm;
using SkiaSharp;
using Glover.Services.Abstract;
using DynamicData;

namespace Glover.Models
{
    public record Elevation(double x, double y)
    {
    }

    /// <summary>
    /// Provides the functionality for managing the state of the map that requires regeneration after being changed.
    /// </summary>
    public class Map : BindableBase
    {
        private readonly Logger _logger = LogManager.GetLogger(nameof(Map));

        /// We index distances to reduce the amount of calls to <see cref="GeoCalculator"/>.
        private readonly Dictionary<string, double> _flyingDistancesIndex = new Dictionary<string, double>();

        private readonly List<ArucoMarker> _markers = new List<ArucoMarker>();

        private readonly INotificationService _notificationService;
        private readonly CartesianChart _elevationMapControl;
        private static readonly SolidColorPaint lightGray = new SolidColorPaint(SKColors.LightGray);
        private static readonly SKColor s_gray1 = new(160, 160, 160);
        private static readonly SKColor s_gray2 = new(90, 90, 90);
        private static readonly SKColor s_dark3 = new(60, 60, 60);

        private ISeries[] _series = [];
        public ISeries[] Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        private Axis[] _xAxes =
        [
            new Axis
            {
                Name = "Aruco markers",
                NameTextSize = 12,
                NamePaint = lightGray,
                TextSize = 12,
                LabelsPaint = lightGray,
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = new SKColor(220, 220, 220).WithAlpha(88),
                    StrokeThickness = 1,
                    PathEffect = new DashEffect(new float[] { 3, 3 }),
                },
                Padding = new Padding(5, 15, 5, 5),
            }
        ];
        public Axis[] XAxes
        { 
            get => _xAxes;
            set => SetProperty(ref _xAxes, value);
        }

        private Axis[] _yAxes =
        [
            new Axis
            {
                Name = "Z axis",
                NameTextSize = 12,
                NamePaint = lightGray,
                TextSize = 12,
                LabelsPaint = lightGray,
                Padding = new Padding(5, 0),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = new SKColor(220, 220, 220).WithAlpha(68),
                    StrokeThickness = 1,
                    PathEffect = new DashEffect(new float[] { 3, 3 }),
                },
                ShowSeparatorLines = true,
            }
        ];
        public Axis[] YAxes
        {
            get => _yAxes;
            set => SetProperty(ref _yAxes, value);
        }

        /// <summary>
        /// Instantiates the new <see cref="Map"/> object.
        /// </summary>
        /// <param name="notificationService">The user notification service.</param>
        /// <param name="flightMission">The map flight mission.</param>
        /// <param name="elevationMapControl">The elevation map control.</param>
        /// <param name="demService">The digital elevation model.</param>
        public Map(
            INotificationService notificationService,
            CartesianChart elevationMapControl)
        {
            _notificationService = notificationService.ThrowIfNull(nameof(notificationService));
            _elevationMapControl = elevationMapControl.ThrowIfNull(nameof(elevationMapControl));
        }

        public void ClearMap()
        {
            _markers.Clear();
        }

        public void AddMarker(ArucoMarker marker)
        {
            _markers.Add(marker);
        }

        public void AddMarkers(ArucoMarker[] markers)
        {
            _markers.AddRange(markers);
        }

        public void RemoveMarker(ArucoMarker marker)
        {
            var markerToRemove = _markers.FirstOrDefault(m => m.Id == marker.Id);
            if (markerToRemove != null)
            {
                _markers.Remove(markerToRemove);
            }
        }

        /// <summary>
        /// Regenerates the elevation map.
        /// </summary>
        public void RegenerateMap()
        {
            var routePoints = _markers.ToArray();
            
            if (routePoints.Length < 1) 
            {
                Series = new ISeries[] {};
                return;
            }
            
            var flyingElevations = GenerateFlyingMap(routePoints);

            int maxElevation = (int)flyingElevations.Max(e => e.y);

            var elevationDif = (int)maxElevation;
            var elevationLim = elevationDif * 0.04;
            YAxes[0].MaxLimit = maxElevation + elevationLim;
            YAxes[0].MinLimit = 0;

            RaisePropertyChanged(nameof(YAxes));

            Series = new ISeries[] {
                new LineSeries<Elevation>
                {
                    Values = flyingElevations,
                    Mapping = (sample, _) => 
                    {
                        return new LiveChartsCore.Kernel.Coordinate(sample.x, sample.y);
                    },
                    Stroke = new SolidColorPaint(SKColors.Lavender, 4),
                    GeometryStroke = null,
                    LineSmoothness = 0,
                    GeometrySize = 9,
                    Fill = null,
                }
            };
        }

        private Elevation[] GenerateFlyingMap(ArucoMarker[] arucoMarkers)
        {
            var homePointAltitude = 0;
            double currentDistance = 0;

            var s_blue = SKColors.Blue;
            var brown = SKColors.Brown;
            var lightGray = SKColors.LightGray;
            var flyingDistances = new List<double> { 0 };
            var flyingElevations = new List<double> { 0 };

            foreach (var marker in arucoMarkers)
            {
                currentDistance += 3;
                flyingDistances.Add(currentDistance);
                
                flyingElevations.Add(homePointAltitude + marker.Altitude);
            }

            var elevations = new List<Elevation>();
            for (int i = 0; i < flyingDistances.Count; i++)
            {
                elevations.Add(new Elevation(flyingDistances[i], flyingElevations[i]));
            }
            
            //currentDistance += 3;
            //elevations.Add(new Elevation(currentDistance, 0));

            return elevations.ToArray();
        }
    }
}