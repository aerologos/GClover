using Avalonia.Controls.Notifications;
using System.Threading.Tasks;

namespace Glover.Services.Abstract
{
    /// <summary>
    /// Describes the contract for notifying the user about performed actions and errors happened.
    /// </summary>
    public interface INotificationService
    {
        public INotificationManager NotificationManager { get; set; }
        
        /// <summary>
        /// Notifies the user about some successfully performed action.
        /// </summary>
        /// <param name="message">The message to display.</param>
        Task NotifyAboutSuccessAsync(string message);

        /// <summary>
        /// Notifies the user about failure happened on some performed action.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void NotifyAboutFailure(string message);

        /// <summary>
        /// Notifies the user about failure happened on some performed action.
        /// </summary>
        /// <param name="message">The message to display.</param>
        Task NotifyAboutFailureAsync(string message);

        /// <summary>
        /// Notifies the user about error happened on some performed action.
        /// </summary>
        /// <param name="message">The message to display.</param>
        Task NotifyAboutErrorAsync(string message);

        /// <summary>
        /// Notifies the user about cancelled operation.
        /// </summary>
        /// <param name="message">The message to display.</param>
        Task NotifyAboutCancellationAsync(string message);

        /// <summary>
        /// Notifies the user about incoming UAV status.
        /// </summary>
        /// <param name="message">The message to display.</param>
        Task NotifyAboutStatusAsync(string message);
    }
}