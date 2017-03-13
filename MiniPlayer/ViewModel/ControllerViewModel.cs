using System.Linq;
using System.Windows.Media.Imaging;
using MiniPlayer.Helper;
using MiniPlayer.Objects;

namespace MiniPlayer.ViewModel
{
    public class ControllerViewModel : NotifyBase
    {
        public ControllerViewModel(PlayerManager playerManger)
        {
            Player = playerManger.GetPlayers().SingleOrDefault(x => x.Name == Properties.Settings.Default.PlayerMode);
            if (Player == null) return;

            Player.OnTrackChange += OnTrackChange;
            Player.OnTrackTimeChange += OnTrackTimeChange;

            UpdateTrackInformation(Player.GetCurrentTrack());
        }

        private void OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            Execute.OnUIThread(() => UpdateTrackInformation(e.NewTrack));
        }

        private void OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            UpdateTime(e.TrackTime);
        }

        private void UpdateTime(double time)
        {
            CurrentTime = time;
        }

        private void UpdateTrackInformation(Track track)
        {
            if (track == null) return;

            CurrentTime = _player.PlayingPosition;

            var smallCoverImage = new BitmapImage(track.AlbumArtUri);
            TrackName = track.TrackResource.Name;
            ArtistName = track.ArtistResource.Name;
            TotalTime = track.Length;
            Cover = smallCoverImage;
        }

        private IPlayer _player;
        public IPlayer Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        private double _currentTime;
        public double CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        private double _totalTime;
        public double TotalTime
        {
            get { return _totalTime; }
            set
            {
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        private string _trackName;
        public string TrackName
        {
            get { return _trackName; }
            set
            {
                _trackName = value;
                OnPropertyChanged();
            }
        }

        private string _artistName;
        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                _artistName = value;
                OnPropertyChanged();
            }
        }

        private BitmapSource _cover;
        public BitmapSource Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged();
            }
        }

    }
}
