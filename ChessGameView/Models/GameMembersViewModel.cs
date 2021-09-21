using System;
using ChessGameCore.ContentManager;

namespace ChessGameView.Models
{
    public class GameMembersViewModel
    {
        public GameMembersViewModel(PlayerViewModel playerWhoMadeGame, PlayerViewModel playerWhoJoined)
        {
            PlayerWhoMadeGame = playerWhoMadeGame;
            PlayerWhoJoined = playerWhoJoined;
        }
        public PlayerViewModel PlayerWhoMadeGame { get; set; }
        public PlayerViewModel PlayerWhoJoined { get; set; }
    }
}