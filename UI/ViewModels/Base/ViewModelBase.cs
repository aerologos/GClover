using System;
using Prism.Mvvm;

namespace Glover.ViewModels
{
    /// <summary>
    /// Provides the baseline functionality for view models.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IDisposable
    {
        /// <summary>
        /// Handles the UI content rendered event.
        /// </summary>
        public virtual void OnContentRendered()
        {
        }

        /// <summary>
        /// Handles the application shutdown event.
        /// </summary>
        public virtual void OnShutdown()
        {
        }

        private bool _disposed;
        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            if (_disposed) return;

            PerformDisposal();
            GC.SuppressFinalize(this);

            _disposed = true;
        }

        /// <summary>
        /// Performs the safe dispose of resources.
        /// </summary>
        protected virtual void PerformDisposal()
        {
        }
    }
}