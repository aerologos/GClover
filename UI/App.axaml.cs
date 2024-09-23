using System.Globalization;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Glover.Services;
using Glover.Services.Abstract;
using Glover.ViewModels;
using Glover.Views;
using NLog;
using Prism.DryIoc;
using Prism.Ioc;

namespace Glover
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

            //DispatcherUnhandledException += App_DispatcherUnhandledException;

            SetupCulture();
        }
        
        /*private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            _logger.Error(e.Exception);

            // Prevent default unhandled exception processing
            e.Handled = true;
        }*/

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // config
            //var config = Config.Instance;
            //containerRegistry.RegisterSingleton<IConfig>(() => config);
            containerRegistry.RegisterSingleton<INotificationService, ToastNotificationService>();
            containerRegistry.RegisterSingleton<IFileService, FileService>();
            
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