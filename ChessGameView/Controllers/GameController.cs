using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessGameCore.ContentManager;
using ChessGameView.Models;
using ChessGameCore;

namespace ChessGameView.Controllers
{
    public class GameController : Controller
    {

        private readonly GameManager _gameManager;
        private readonly PlayerManager _playerManager;
        

        public GameController(GameManager gameManager, PlayerManager playerManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
          
        }
        public IActionResult DeleteGameFromList(string gameId, int finishStatus)
        {
            _gameManager.DeleteGameFromList(gameId, finishStatus);
            return Json(0);
        }
        public IActionResult GameEndCheck(string gameId)
        {
            return Json(_gameManager.GameEndCheck(gameId));
        }
        public IActionResult AddPlayerToList(string playerId)
        {
            _playerManager.AddPlayerToList(playerId);
            return Json(0);
        }
        public IActionResult GetMoveCount(string gameId)
        {
            return Json(_gameManager.GetMoveCount(gameId));
        }
        public IActionResult IncreaseMoveCount(string gameId)
        {
            _gameManager.IncreaseMoveCount(gameId);
            return Json(0);
        }
        public IActionResult GetGameListVersion()
        {
            return Json(_gameManager.Version);
        }
        public IActionResult GetTimeForThink(string gameId)
        {
            Game game = _gameManager.GetTimeForThink(gameId);

            PlayerViewModel playerWhoMadeGame = new(game.PlayerWhoMadeGame.Id, game.PlayerWhoMadeGame.Color.ToString(), game.PlayerWhoMadeGame.TimeForThink);
            PlayerViewModel playerWhoJoined = new(game.PlayerWhoJoined.Id, game.PlayerWhoJoined.Color.ToString(), game.PlayerWhoJoined.TimeForThink);

            GameMembersViewModel gameMembersModel = new(playerWhoMadeGame, playerWhoJoined);
           
            return Json(gameMembersModel);
        }
       
        public IActionResult GetGames(string playerId)
        {
            var playerActiveGameList = _gameManager.GetGames(playerId);
            var viewGameList = playerActiveGameList.Select(game => new GameViewModel
            {
                GameId = Guid.Parse(game.GameId),
                WhitePlayerName = (game.PlayerWhoMadeGame?.Color == PieceColor.White ? game.PlayerWhoMadeGame : game.PlayerWhoJoined)?.Id,
                BlackPlayerName = (game.PlayerWhoMadeGame?.Color != PieceColor.White ? game.PlayerWhoMadeGame : game.PlayerWhoJoined)?.Id,
                IsStarted = game.IsStarted
            }).ToList();
            return Json(viewGameList);
        }
        public IActionResult GetPlayerInfo(string gameId)
        {
            var game = _gameManager.GetPlayerInfo(gameId);

            PlayerViewModel playerWhoMadeGame = new(game.PlayerWhoMadeGame.Id, game.PlayerWhoMadeGame.Color.ToString());
            PlayerViewModel playerWhoJoined = new(game.PlayerWhoJoined.Id, game.PlayerWhoJoined.Color.ToString());

            GameMembersViewModel GameMembers = new(playerWhoMadeGame, playerWhoJoined);
           
            return Json(GameMembers);
        }
        public IActionResult GetMoveNumber(string gameId)
        {
            return Json(_gameManager.GetMoveNumber(gameId));
        }
        public IActionResult PieceOwnerValidator(string gameId, string playerId, int horizontal, int vertical)
        {
            return Json(_gameManager.PieceOwnerValidator(gameId, playerId, horizontal, vertical));
        }
        public IActionResult RevivePiece(int horizontal, int vertical, string pieceToBeRevived, string Color, string gameId)
        {
           _gameManager.RevivePiece(horizontal, vertical, pieceToBeRevived, Color, gameId);
            return Json(0);
        }
        public IActionResult ChangeSituation(int fromHorizontal, int fromVertical, int toHorizontal, int toVertical, string gameId)
        {
            return Json(_gameManager.ChangeSituation(fromHorizontal, fromVertical, toHorizontal, toVertical, gameId));
        }
        public IActionResult GetPiece(int horizontal, int vertical, string gameId)
        {
            return Json(_gameManager.GetPiece(horizontal, vertical, gameId));
        }
        public IActionResult GetPlayerId(string gameId)
        {
            return Json(_gameManager.GetPlayerId(gameId));
        }
        public IActionResult GetMovements(int horizontal, int vertical, string gameId)
        {
            return Json(_gameManager.GetMovements(horizontal,vertical,gameId));   
        }

        public IActionResult CreateGame(string playerWhoCreatedGame, string choosenColor)
        {
            _gameManager.AddToGameList(playerWhoCreatedGame, choosenColor);
            return Json(_gameManager.GamesArray[^1].GameId);
        }
        public IActionResult JoinPlayer(string gameId, string playerWhoJoined)
        {
            _gameManager.JoinPlayer(gameId, playerWhoJoined);
            return Json(0);
        }
        public IActionResult PingPlayer(string gameId, string playerId, int date)
        {
            _gameManager.PingPlayer(gameId, playerId, date);     
            return Json(_gameManager.CurrentDate);
        }
        public IActionResult GetGameVersion(string gameId)
        {
            return Json(_gameManager.GetGameVersion(gameId));
        }
        
        public IActionResult GetGameInfo(string gameId)
        {
            var chessBoard = _gameManager.GetGameInfo(gameId);
            var PieceViewList = chessBoard.Select(piece => new PieceViewModel
            {
                HorizontalPosition = piece.HorizontalPosition,
                VerticalPosition = piece.VerticalPosition,
                Name = piece.Name.ToString(),
                Color = piece.Color.ToString(),
            }).ToList();
            return Json(PieceViewList);

        }

       
        public IActionResult GetGameSize(string gameId, string playerId)
        {
            return Json(_gameManager.GetGameSize(gameId, playerId));
        }


        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult PlayerPage()
        {
            return View();
        }

        public IActionResult GamePage()
        {
            return View();
        }

        public IActionResult GameView()
        {   
            return View();
        }

        

        
    }
}
