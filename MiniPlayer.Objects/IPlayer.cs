using System;

namespace MiniPlayer.Objects
{
    public interface IPlayer
    {
        string Name { get; }

        void Play();
        void Pause();
        void Next();
        void Previous();

        bool IsPlaying { get; }
        double PlayingPosition { get; }

        event EventHandler<TrackChangeEventArgs> OnTrackChange;
        event EventHandler<PlayStateEventArgs> OnPlayStateChange;
        event EventHandler<TrackTimeChangeEventArgs> OnTrackTimeChange;

        Track GetCurrentTrack();
    }
}
