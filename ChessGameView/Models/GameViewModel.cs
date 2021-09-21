using System;


namespace ChessGameView.Models
{

    public class GameViewModel
    {

        public Guid GameId { get; set; }
        public string WhitePlayerName { get; set; }
        public string BlackPlayerName { get; set; }
        public bool IsStarted { get; set; }

    }

}