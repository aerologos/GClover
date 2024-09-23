namespace Glover.ViewModels
{
    /// <summary>
    /// Executes the actions happening on the <see cref="MainWindowView"/>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the application name with version number.
        /// </summary>
        public string AppName
        {
            get
            {
                //var assemblyVersion = typeof(MainWindowViewModel).Assembly.GetName().Version;
                return $"Glover";
            }
        }

        /// <summary>
        /// Instantiates the new <see cref="MainWindowViewModel"/> object.
        /// </summary>
        /// <param name="licenseService">The license service.</param>
        public MainWindowViewModel()
        {
        }

        /// <inheritdoc cref="ViewModelBase.OnContentRendered"/>
        public override void OnContentRendered()
        {
        }
    }
}