using System;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace ElevationMap.ViewModels.Base
{
    /// <summary>
    /// Provides the baseline functionality for dialog view models.
    /// </summary>
    public abstract class DialogViewModelBase : BindableBase, IDialogAware
    {
        /// <inheritdoc cref="IDialogAware.Title"/>
        public virtual string Title { get; }
        
        /// <summary>
        /// Gets the command to simply cancel the dialog.
        /// </summary>
        public DelegateCommand CancelCommand { get; }
        
        /// <inheritdoc cref="IDialogAware.RequestClose"/>
        DialogCloseListener IDialogAware.RequestClose { get; }

        /// <summary>
        /// Instantiates the new <see cref="DialogViewModelBase"/> inheritor's object.
        /// </summary>
        protected DialogViewModelBase()
        {
            CancelCommand = new DelegateCommand(Cancel);
        }

        /// <inheritdoc cref="IDialogAware.OnDialogOpened"/>
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }


        private void Cancel()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        protected void RaiseRequestClose(IDialogResult dialogResult)
        {
            ((IDialogAware) this).RequestClose.Invoke(dialogResult);
        }

        /// <inheritdoc cref="IDialogAware.CanCloseDialog"/>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <inheritdoc cref="IDialogAware.OnDialogClosed"/>
        public void OnDialogClosed()
        {
        }
    }
}