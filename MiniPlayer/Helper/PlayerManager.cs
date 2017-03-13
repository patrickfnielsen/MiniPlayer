using System.Collections.Generic;
using System.IO;
using MiniPlayer.Objects;

namespace MiniPlayer.Helper
{
    public class PlayerManager
    {
        private List<IPlayer> _players = new List<IPlayer>();
        private readonly AssemblyManager _assembly = new AssemblyManager();

        public void RegisterPlayers(string path)
        {
            Directory.CreateDirectory(path);
            _players = _assembly.LoadFromPath<IPlayer>(path);
        }

        public List<IPlayer> GetPlayers()
        {
            return _players;
        }
    }
}
