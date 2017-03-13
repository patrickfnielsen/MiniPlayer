using System.Windows;
using MiniPlayer.Helper;

namespace MiniPlayer
{
    public partial class App : Application
    {
        public static readonly IoC Ioc = new IoC();

        public App()
        {
            Ioc.Register<ViewManager>();
            Ioc.Register<PlayerManager>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }
    }
}
