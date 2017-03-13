using System;
using System.Threading;
using MiniPlayer.Objects;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using PlayStateEventArgs = MiniPlayer.Objects.PlayStateEventArgs;
using TrackChangeEventArgs = MiniPlayer.Objects.TrackChangeEventArgs;
using TrackTimeChangeEventArgs = MiniPlayer.Objects.TrackTimeChangeEventArgs;

namespace Spotify
{
    public class SpotifyPlayer : IPlayer
    {
        public string Name => "Spotify";

        public event EventHandler<TrackChangeEventArgs> OnTrackChange;
        public event EventHandler<PlayStateEventArgs> OnPlayStateChange;
        public event EventHandler<TrackTimeChangeEventArgs> OnTrackTimeChange;
        private readonly SpotifyLocalAPI _spotify = new SpotifyLocalAPI();

        public bool IsPlaying => _spotify.GetStatus().Playing;
        public double PlayingPosition => _spotify.GetStatus().PlayingPosition;

        public SpotifyPlayer()
        {
            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                SpotifyLocalAPI.RunSpotify();
                Thread.Sleep(5000);
            }

            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                SpotifyLocalAPI.RunSpotifyWebHelper();
                Thread.Sleep(4000);
            }

            _spotify.Connect();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _spotify.OnTrackChange += SpotifyOnTrackChange;
            _spotify.OnPlayStateChange += SpotifyOnPlayStateChange;
            _spotify.OnTrackTimeChange += SpotifyOnTrackTimeChange;

            _spotify.ListenForEvents = true;
        }

        public Track GetCurrentTrack()
        {
            var track = _spotify.GetStatus()?.Track;
            if (track == null) return null;

            return new Track()
            {
                Length = track.Length,
                AlbumArtUri = new Uri(track.GetAlbumArtUrl(AlbumArtSize.Size160)),
                ArtistResource = new Resource()
                {
                    Name = track.ArtistResource.Name
                },
                AlbumResource = new Resource()
                {
                    Name = track.AlbumResource.Name
                },
                TrackResource = new Resource()
                {
                    Name = track.TrackResource.Name
                }
            };
        }

        private void SpotifyOnTrackChange(object sender, SpotifyAPI.Local.TrackChangeEventArgs trackChangeEventArgs)
        {
            var track = new Track()
            {
                Length = trackChangeEventArgs.NewTrack.Length,
                AlbumArtUri = new Uri(trackChangeEventArgs.NewTrack.GetAlbumArtUrl(AlbumArtSize.Size160)),
                ArtistResource = new Resource()
                {
                    Name = trackChangeEventArgs.NewTrack.ArtistResource.Name
                },
                AlbumResource = new Resource()
                {
                    Name = trackChangeEventArgs.NewTrack.AlbumResource.Name
                },
                TrackResource = new Resource()
                {
                    Name = trackChangeEventArgs.NewTrack.TrackResource.Name
                }
            };

            var eventArgs = new TrackChangeEventArgs()
            {
                NewTrack = track
            };

            OnTrackChange?.Invoke(this, eventArgs);
        }

        private void SpotifyOnPlayStateChange(object sender, SpotifyAPI.Local.PlayStateEventArgs playStateEventArgs)
        {
            var eventArgs = new PlayStateEventArgs
            {
                Playing = playStateEventArgs.Playing
            };

            OnPlayStateChange?.Invoke(this, eventArgs);
        }

        private void SpotifyOnTrackTimeChange(object sender, SpotifyAPI.Local.TrackTimeChangeEventArgs e)
        {
            var eventArgs = new TrackTimeChangeEventArgs()
            {
                TrackTime = e.TrackTime
            };

            OnTrackTimeChange?.Invoke(this, eventArgs);
        }

        public void Play()
        {
            _spotify.Play();
        }

        public void Pause()
        {
            _spotify.Pause();
        }

        public void Next()
        {
            _spotify.Skip();
        }

        public void Previous()
        {
            _spotify.Previous();
        }
    }
}
