using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MiniPlayer.Helper;
using MiniPlayer.Objects;

namespace MiniPlayer.ViewModel
{
    public class MainViewModel : NotifyBase, IClosing
    {
        private readonly IWindowService _windowService;
        private readonly ViewManager _viewManager;

        public MainViewModel(IWindowService windowService, PlayerManager playerManager, ViewManager viewManager)
        {
            _windowService = windowService;
            _viewManager = viewManager;
            Execute.InitializeWithDispatcher();

            ExitApplication = new SimpleDelegateCommand(ExitApplicationAction);
            ShowSettings = new SimpleDelegateCommand(ShowSettingsAction);

            SetWindowPosition();

            playerManager.RegisterPlayers("players");
            viewManager.RegisterViews("views");
            RegisterEvents();

            SetViewMode(Properties.Settings.Default.ViewMode);
        }

        public bool OnClosing()
        {
            Properties.Settings.Default.WindowPosition = Position;
            Properties.Settings.Default.Save();

            return true;
        }

        private void RegisterEvents()
        {
            Properties.Settings.Default.SettingChanging += (sender, args) =>
            {
                if (args.SettingName != "ViewMode") return;
                SetViewMode(args.NewValue as string);
            };
        }

        private void SetViewMode(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var view = _viewManager.GetViews().First();
                Content = Activator.CreateInstance(view.GetType());
            }
            else
            {
                var view = _viewManager.GetViewByName(name);
                Content = Activator.CreateInstance(view.GetType());
            }
        }

        private void SetWindowPosition()
        {
            if (Properties.Settings.Default.SaveWindowPosition && Properties.Settings.Default.WindowPosition != null)
            {
                Position = Properties.Settings.Default.WindowPosition;
            }
            else
            {
                Position = new WindowPosition()
                {
                    Top = 200,
                    Left = 200
                };
            }
        }

        private object _content;
        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExitApplication { get; set; }
        public void ExitApplicationAction(object param)
        {
            Application.Current.Shutdown(0);
        }

        public ICommand ShowSettings { get; set; }
        public void ShowSettingsAction(object param)
        {
            _windowService.ShowSettingsWindow();
        }

        private WindowPosition _position;
        public WindowPosition Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }
    }
}
