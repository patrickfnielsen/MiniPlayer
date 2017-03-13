using System;

namespace MiniPlayer.Objects
{
    public class VolumeChangeEventArgs : EventArgs
    {
        public double OldVolume { get; set; }
        public double NewVolume { get; set; }
    }
}
