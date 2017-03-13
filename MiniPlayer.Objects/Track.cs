using System;

namespace MiniPlayer.Objects
{
    public class Track
    {
        public double Length { get; set; }
        public Resource AlbumResource { get; set; }
        public Resource TrackResource { get; set; }
        public Resource ArtistResource { get; set; }
        public Uri AlbumArtUri { get; set; }
    }
}
