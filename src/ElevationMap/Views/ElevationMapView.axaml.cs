using System;
using Avalonia.Controls;
using Avalonia.Threading;
using Extype;
using ElevationMap.Const;
using ElevationMap.ViewModels;

namespace ElevationMap.Views
{
    public partial class ElevationMapView : UserControl
    {
        public ElevationMapView(ElevationMapViewModel viewModel)
        {
            InitializeComponent();
            
            viewModel.LinkElevationMap(ElevationMap);  
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