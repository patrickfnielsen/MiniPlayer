using MiniPlayer.Helper;
using MiniPlayer.Objects;
using MiniPlayer.ViewModel;

namespace MiniPlayer
{
    public sealed class ViewModelLocator
    {
        public ViewModelLocator()
        {
            App.Ioc.Register<IWindowService, WindowService>();
            App.Ioc.Register<MainViewModel>();
            App.Ioc.Register<SettingsViewModel>();
            App.Ioc.Register<ControllerViewModel>();
        }

        public MainViewModel Main => App.Ioc.Resolve<MainViewModel>();
        public SettingsViewModel Settings => App.Ioc.Resolve<SettingsViewModel>();
        public ControllerViewModel Controller => App.Ioc.Resolve<ControllerViewModel>();
    }
}
