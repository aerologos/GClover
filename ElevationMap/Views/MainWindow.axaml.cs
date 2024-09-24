using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Extype;
using ElevationMap.Const;
using ElevationMap.Views;
using ElevationMap.ViewModels;
using ElevationMap.Services.Abstract;
using ElevationMap.Models;
using Prism.Navigation.Regions;

namespace ElevationMap.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(
            INotificationService notificationService,
            IRegionManager regionManager,
            IFileService fileService)
        {
            notificationService.ThrowIfNull(nameof(notificationService));
            regionManager.ThrowIfNull(nameof(regionManager));
            fileService.ThrowIfNull(nameof(fileService));

            InitializeComponent();

            // window dependent types
            notificationService.NotificationManager = new WindowNotificationManager(this)
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            fileService.Target = this;

            regionManager.RegisterViewWithRegion<ElevationMapView>(PrismRegions.ElevationMapViewContentRegion);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            if (DataContext is ViewModelBase main)
            {
                main.OnContentRendered();
            }
        }

        private void View_OnClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is ViewModelBase main)
            {
                main.OnShutdown();
                main.Dispose();
            }
        }
    }
}