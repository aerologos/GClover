using System;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

namespace ElevationMap.Extensions
{
    public static class DispatcherExtensions
    {
        public static NotifyContextCompletion SwitchToMyContext(this Dispatcher dispatcher)
        {
            return new NotifyContextCompletion(dispatcher);
        }

        public readonly struct NotifyContextCompletion : INotifyCompletion
        {
            private readonly Dispatcher _dispatcher;

            public bool IsCompleted => _dispatcher.CheckAccess();

            public NotifyContextCompletion(Dispatcher dispatcher) => _dispatcher = dispatcher;

            public NotifyContextCompletion GetAwaiter() => this;

            public void OnCompleted(Action callback) => _dispatcher.InvokeAsync(callback);

            public void GetResult()
            {
            }
        }
    }
}