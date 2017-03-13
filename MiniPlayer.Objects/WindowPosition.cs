using System.Configuration;

namespace MiniPlayer.Objects
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class WindowPosition
    {
        public double Top { get; set; }
        public double Left { get; set; }
    }
}
