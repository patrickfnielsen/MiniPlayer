using System;
using System.Windows.Threading;

namespace MiniPlayer.Helper
{
    public static class Execute
    {
        private static Action<System.Action> _executor = action => action();
        public static void InitializeWithDispatcher()
        {

            var dispatcher = Dispatcher.CurrentDispatcher;
            _executor = action => {
                if (dispatcher.CheckAccess())
                    action();
                else dispatcher.BeginInvoke(action);
            };
        }

        public static void OnUIThread(this System.Action action)
        {
            _executor(action);
        }
    }
}
