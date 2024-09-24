using System.Globalization;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Codenizer.Avalonia.Map;
using ElevationMap.Operations;
using ElevationMap.Services;
using ElevationMap.Services.Abstract;
using ElevationMap.ViewModels;
using ElevationMap.Views;
using NLog;
using Prism.DryIoc;
using Prism.Ioc;

namespace ElevationMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private readonly Logger _logger = LogManager.GetLogger(nameof(App));

        /// <inheritdoc/>
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();

            SetupCulture();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // config
            var config = Config.Instance;
            containerRegistry.RegisterSingleton<IConfig>(() => config);
            containerRegistry.RegisterSingleton<INotificationService, ToastNotificationService>();
            containerRegistry.RegisterSingleton<IFileService, FileService>();
            containerRegistry.RegisterSingleton<MarkerMonitor>();

            // this singlton is crucial. Never remove it if you don't want to have troubles with displaying elevation map
            containerRegistry.RegisterSingleton<ElevationMapViewModel>();
            
            // though it might be dangerous, I need to dynamically replace data link dependencies
            containerRegistry.RegisterSingleton<IContainerRegistry>(() => containerRegistry);
        }

        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override AvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// TODO: think about more elegant approach. Would be better to have the culture of the chosen installer.
        /// </summary>
        private static void SetupCulture()
        {
            var culture = CultureInfo.CurrentCulture;
            if (culture.Name != "ru-RU" &&
                culture.Name.StartsWith("ru-"))
            {
                culture = new CultureInfo("ru-RU");
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}