using MiniPlayer.Objects;
using MiniPlayer.View;

namespace MiniPlayer.Helper
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow()
        {
            var win = new SettingsWindow();
            win.Show();
        }
    }
}
