using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameCore.ContentManager
{
    public class PlayerManager
    {
        public PlayerManager()
        {
            PlayerArray = new List<Player>();
        }

        public List<Player> PlayerArray { get; set; }
        public string AddPlayerToList(string playerId)
        {
            PlayerArray.Add(new Player(playerId));        // Add player to list of player List
            return PlayerArray.Last().Id;
        }
    }
}
