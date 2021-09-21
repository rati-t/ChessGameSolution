using System;
using ChessGameCore.ContentManager;

namespace ChessGameView.Models
{
    public class PlayerViewModel
    {
        public PlayerViewModel(string playerId, string color)
        {
            Id = playerId;
            Color = color;
        }

        public PlayerViewModel(string playerId, string color, int timeForThink)
        {
            Id = playerId;
            Color = color;
            TimeForThink = timeForThink;
        }

        public string Id { get; set; }
        public string Color { get; set; }
        public int TimeForThink { get; set; }
    }
}