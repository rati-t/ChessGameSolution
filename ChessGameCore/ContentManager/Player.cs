using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameCore.ContentManager
{
    public class Player
    {
        public Player(string id)
        {
            Id = id;
        }
        public Player(string id, PieceColor choosenColor)
        {
            Id = id;
            Color = choosenColor;
        }
        public string Id { get; set; }
        public PieceColor Color { get; set; }
        public long LastPing { get; set; } 
        public int TimeForThink { get; set; }
    }
}
