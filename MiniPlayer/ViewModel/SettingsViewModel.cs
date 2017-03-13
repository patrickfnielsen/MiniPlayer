using System.Collections.Generic;
using System.Linq;
using MiniPlayer.Helper;
using MiniPlayer.Objects;

namespace MiniPlayer.ViewModel
{
    public class SettingsViewModel : NotifyBase
    {
        public SettingsViewModel(ViewManager viewManager, PlayerManager playerManager)
        {
            SaveWindowPosition = Properties.Settings.Default.SaveWindowPosition;
            SelectedView = Properties.Settings.Default.ViewMode;
            Views = viewManager.GetViews().Select(x => x.Name);

            Players = playerManager.GetPlayers();
            SelectedPlayer = playerManager.GetPlayers().SingleOrDefault(x => x.Name == Properties.Settings.Default.PlayerMode);
        }

        private string _selectedView;
        public string SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged();
                Properties.Settings.Default.ViewMode = value;
                Properties.Settings.Default.Save();
            }
        }

        private IPlayer _selectedPlayer;
        public IPlayer SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
                Properties.Settings.Default.PlayerMode = value.Name;
                Properties.Settings.Default.Save();
            }
        }

        private IEnumerable<string> _views;
        public IEnumerable<string> Views
        {
            get { return _views; }
            set
            {
                _views = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<IPlayer> _players;
        public IEnumerable<IPlayer> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged();
            }
        }

        private bool _saveWindowPosition;
        public bool SaveWindowPosition
        {
            get { return _saveWindowPosition; }
            set
            {
                _saveWindowPosition = value;
                OnPropertyChanged();
                Properties.Settings.Default.SaveWindowPosition = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
