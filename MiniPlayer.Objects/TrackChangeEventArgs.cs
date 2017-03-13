using System;

namespace MiniPlayer.Objects
{
    public class TrackChangeEventArgs : EventArgs
    {
        public Track NewTrack { get; set; }
    }
}
