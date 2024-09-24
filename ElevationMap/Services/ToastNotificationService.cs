using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using ElevationMap.Extensions;
using ElevationMap.Services.Abstract;
using System.Threading.Tasks;

namespace ElevationMap.Services
{
    /// <summary>
    /// Provides the functionality for notifying the user about performed actions.
    /// </summary>
    public class ToastNotificationService : INotificationService
    {
        public INotificationManager NotificationManager { get; set; }

        /// <inheritdoc/>
        public void NotifyAboutSuccess(string message)
        {
            NotificationManager.Show(new Notification("", message, NotificationType.Success));
        }

        /// <inheritdoc/>
        public async Task NotifyAboutSuccessAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotifyAboutSuccess(message);
        }

        public void NotifyAboutFailure(string message)
        {
            NotificationManager.Show(new Notification("", message, NotificationType.Warning));
        }

        /// <inheritdoc/>
        public async Task NotifyAboutFailureAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotifyAboutFailure(message);
        }

        /// <inheritdoc/>
        public async Task NotifyAboutErrorAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotificationManager.Show(new Notification("", message, NotificationType.Error));
        }

        /// <inheritdoc/>
        public async Task NotifyAboutCancellationAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotificationManager.Show(new Notification("", message));
        }

        /// <inheritdoc/>
        public async Task NotifyAboutStatusAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotificationManager.Show(new Notification("", message));
        }
    }
}