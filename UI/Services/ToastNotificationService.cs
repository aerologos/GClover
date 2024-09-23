using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using Glover.Extensions;
using Glover.Services.Abstract;
using System.Threading.Tasks;

namespace Glover.Services
{
    /// <summary>
    /// Provides the functionality for notifying the user about performed actions.
    /// </summary>
    public class ToastNotificationService : INotificationService
    {
        public INotificationManager NotificationManager { get; set; }

        /// <inheritdoc/>
        public async Task NotifyAboutSuccessAsync(string message)
        {
            await Dispatcher.UIThread.SwitchToMyContext();
            NotificationManager.Show(new Notification("", message, NotificationType.Success));
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