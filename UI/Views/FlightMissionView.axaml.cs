using System;
using Avalonia.Controls;
using Avalonia.Threading;
using Extype;
using Glover.Const;
using Glover.ViewModels;

namespace Glover.Views
{
    public partial class FlightMissionView : UserControl
    {
        public FlightMissionView()
        {
            InitializeComponent();
            
            Dispatcher.UIThread.ShutdownStarted += Dispatcher_ShutDownStarted;
        }

        private void Dispatcher_ShutDownStarted(object sender, EventArgs e)
        {
            if (DataContext is ViewModelBase viewModel)
            {
                viewModel.OnShutdown();
                viewModel.Dispose();
            }

            Dispatcher.UIThread.ShutdownStarted -= Dispatcher_ShutDownStarted;
        }
    }
}