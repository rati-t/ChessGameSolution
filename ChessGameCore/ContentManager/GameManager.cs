using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameCore.Board;
using ChessGameCore.Pieces;

namespace ChessGameCore.ContentManager
{
    public class GameManager
    {
        public GameManager()
        {
            GamesArray = new List<Game>();
        }
        public long CurrentDate { get; set; }
        public int Version { get; set; }
        public List<Game> GamesArray { set; get; }

        private readonly int _timeLimitForPlayerToRejoin = 60;

        public int GetGameVersion(string gameId)
        {
            int index = GamesArray.FindIndex(a => a.GameId == gameId);
            if (index > -1)
            {
                return (GamesArray[index].GameVersion);
            }
            return 0;
        }

        public List<Game> GetGames(string playerId)
        {
            List<Game> gameList = new();

            foreach (var game in GamesArray)
            {
                if (!game.IsFinished)
                {
                    gameList.Add(game);
                }
            }
            return gameList;
        }
 
        public int PieceOwnerValidator(string gameId, string playerId, int horizontal, int vertical)
        {
            return GameBeingSearched(gameId).PieceOwnerValidator(playerId, horizontal, vertical);
        }
        
        public int GetMoveNumber(string gameId)
        {
            return GameBeingSearched(gameId).MoveCounter;
        }
        public void PingPlayer(string gameId, string playerId, int date)
        {
            Game game = GameBeingSearched(gameId);
           
            if(game.PlayerWhoMadeGame.Id == playerId)
            {
                game.PlayerWhoMadeGame.LastPing = date;
            }
            if (game.PlayerWhoJoined?.Id == playerId)
            {
                game.PlayerWhoJoined.LastPing = date;
            }
        }

        public int GameEndCheck(string gameId)
        {
            Game game = GameBeingSearched(gameId);
            PieceColor ColorToBeSearched = PieceColor.White;

            if (game.MoveCounter % 2 == 0)
            {
                ColorToBeSearched = PieceColor.Black;
            }

            if (game.MoveCounter % 2 == 1)
            {
                ColorToBeSearched = PieceColor.White;
            }

            for (int verticalIndex = 0; verticalIndex < game.VerticalMax; verticalIndex++)
            {
                for (int horizontalIndex = 0; horizontalIndex < game.HorizontalMax; horizontalIndex++)
                {
                    if(game.GameBoard.Game[verticalIndex, horizontalIndex]?.Color == ColorToBeSearched
                       && game.GameBoard.Game[verticalIndex, horizontalIndex]?.ValidateMoves().Count > 0)
                    {
                        return 0;
                    }
                }
            }

            if(!King.CheckAvailable(game.GameBoard.BlackKing.HorizontalPosition, game.GameBoard.BlackKing.VerticalPosition, PieceColor.Black, game.GameBoard)
                && ColorToBeSearched == PieceColor.Black)
            {
                game.GameVersion++;
                return -1;
            }

            if (King.CheckAvailable(game.GameBoard.BlackKing.HorizontalPosition, game.GameBoard.BlackKing.VerticalPosition, PieceColor.Black, game.GameBoard)
                && ColorToBeSearched == PieceColor.Black)
            {
                game.GameVersion++;
                return -3;
            }

            if (!King.CheckAvailable(game.GameBoard.WhiteKing.HorizontalPosition, game.GameBoard.WhiteKing.VerticalPosition, PieceColor.White, game.GameBoard)
                && ColorToBeSearched == PieceColor.White)
            {
                game.GameVersion++;
                return -2;
            }

            if (King.CheckAvailable(game.GameBoard.WhiteKing.HorizontalPosition, game.GameBoard.WhiteKing.VerticalPosition, PieceColor.White, game.GameBoard)
                && ColorToBeSearched == PieceColor.White)
            {
                game.GameVersion++;
                return -3;
            }

            return 0; // this line never executes;
        }
        public void IncreaseMoveCount(string gameId)
        {
            GameBeingSearched(gameId).MoveCounter = GameBeingSearched(gameId).MoveCounter + 1;
        }

        public int[] ChangeSituation(int fromHorizontal, int fromVertical, int toHorizontal, int toVertical, string gameId)
        {
            return GameBeingSearched(gameId).ChangeSituation(fromHorizontal, fromVertical, toHorizontal, toVertical);
        }
        public DefaultPiece GetPiece(int horizontal, int vertical, string gameId)
        {
      
            Game game = GameBeingSearched(gameId);

            if (game.GameBoard.Game[vertical - 1, horizontal - 1] != null)
            {
                int hor = game.GameBoard.Game[vertical - 1, horizontal - 1].HorizontalPosition;
                int ver = game.GameBoard.Game[vertical - 1, horizontal - 1].VerticalPosition;

                PieceColor color = game.GameBoard.Game[vertical - 1, horizontal - 1].Color;
                PieceName name = game.GameBoard.Game[vertical - 1, horizontal - 1].Name;

                DefaultPiece piece = new(hor, ver, color, name);
                return piece;
            }
            else
            {
                return null;
            }

        }
        public List<Cell> GetMovements(int horizontal, int vertical, string gameId)
        {
            return GameBeingSearched(gameId).GetMovements(horizontal, vertical);
        }

        public string[] GetPlayerId(string gameId)
        {
            return (new string[2] { GameBeingSearched(gameId).PlayerWhoMadeGame.Id, GameBeingSearched(gameId).PlayerWhoJoined.Id });
        }

        public int[] GetGameSize(string gameId, string playerId)
        {
            Game game = GameBeingSearched(gameId);

            if (game.PlayerWhoMadeGame != null)
            {
                if (game.PlayerWhoMadeGame.Id == playerId)
                {
                    if (game.PlayerWhoMadeGame.Color == PieceColor.White)
                    {
                        return new int[3] { game.HorizontalMax, game.VerticalMax, 1 };
                    }
                    if (game.PlayerWhoMadeGame.Color == PieceColor.Black)
                    {
                        return new int[3] { game.HorizontalMax, game.VerticalMax, 0 };
                    }
                }
            }

            if (game.PlayerWhoJoined != null)
            {
                if (game.PlayerWhoJoined.Id == playerId)
                {
                    if (game.PlayerWhoJoined.Color == PieceColor.White)
                    {
                        return new int[3] { game.HorizontalMax, game.VerticalMax, 1 };
                    }
                    if (game.PlayerWhoJoined.Color == PieceColor.Black)
                    {
                        return new int[3] { game.HorizontalMax, game.VerticalMax, 0 };
                    }
                }
            }
            return new int[3] { game.HorizontalMax, game.VerticalMax, 2 };  // this is spectator view
        }
        public int GetMoveCount(string gameId)
        {
            return GameBeingSearched(gameId).MoveCounter;
        }
        public void RevivePiece(int horizontal, int vertical, string pieceToBeRevived, string Color, string gameId)
        {
            GameBeingSearched(gameId).RevivePiece(horizontal, vertical, pieceToBeRevived, Color);
        }
        public List<DefaultPiece> GetGameInfo(string gameId)
        {
            return GameBeingSearched(gameId)?.DefaultPieces;
        }
        public Game GetPlayerInfo(string gameId)
        {
            return GameBeingSearched(gameId);
        }

        public string AddToGameList(string playerWhoMadeGame, string choosenColor)
        {
            if (Enum.TryParse(choosenColor, out PieceColor pieceColor))
            {
                GamesArray.Add(new Game(playerWhoMadeGame, pieceColor));        // Add game to list of active games       
                return GamesArray.Last().GameId;
            }
            return "";  // this line never executes
        }

        public void JoinPlayer(string gameId, string playerWhoJoined)
        {
            GameBeingSearched(gameId)?.JoinPlayer(playerWhoJoined);
        }
        public void DeleteGameFromList(string gameId, int finishStatus)
        {
            GamesArray?.Remove(GameBeingSearched(gameId));
        }

        public void PlayerPingShceduling()
        {
            CurrentDate = (Convert.ToInt64(GetUnixEpoch(DateTime.Now)));
            foreach (var game in GamesArray.ToList())
            {
                if (game.IsStarted && game.PlayerWhoJoined?.LastPing > 0)
                {
                    if (Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
                    {
                        game.IsFinished = true;
                        game.GameVersion = -4;
                    }

                    if (Math.Abs(game.PlayerWhoJoined.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
                    {
                        game.IsFinished = true;
                        game.GameVersion = -5;
                    }

                    if(Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin
                        && Math.Abs(game.PlayerWhoJoined.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
                    {
                        game.IsFinished = true;
                        game.GameVersion = -6;
                    }
                }
                if (!game.IsStarted && game.PlayerWhoMadeGame.LastPing > 0)
                {
                    if (Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
                    {   
                        game.IsFinished = true;
                        game.GameVersion = -7;
                    }
                }
            }
        }

        public void PlayerTimerShceduling()
        {
            foreach (var game in GamesArray.ToList())
            {
                if (game.IsStarted)
                {
                    if (game.MoveCounter % 2 == 0)
                    {
                        if (game.PlayerWhoMadeGame.Color == PieceColor.Black)
                        {
                            game.PlayerWhoMadeGame.TimeForThink = game.PlayerWhoMadeGame.TimeForThink - 1;
                        }

                        if (game.PlayerWhoJoined.Color == PieceColor.Black)
                        {
                            game.PlayerWhoJoined.TimeForThink = game.PlayerWhoJoined.TimeForThink - 1;
                        }

                    }

                    if (game.MoveCounter % 2 == 1)
                    {
                        if (game.PlayerWhoMadeGame.Color == PieceColor.White)
                        {
                            game.PlayerWhoMadeGame.TimeForThink = game.PlayerWhoMadeGame.TimeForThink - 1;
                        }

                        if (game.PlayerWhoJoined.Color == PieceColor.White)
                        {
                            game.PlayerWhoJoined.TimeForThink = game.PlayerWhoJoined.TimeForThink - 1;
                        }

                    }

                    if (game.PlayerWhoMadeGame.TimeForThink == 0)
                    {
                        if (game.PlayerWhoMadeGame.Color == PieceColor.White)
                        {
                            game.GameVersion = -8;
                            game.IsFinished = true;
                        }
                        if (game.PlayerWhoMadeGame.Color == PieceColor.Black)
                        {
                            game.GameVersion = -9;
                            game.IsFinished = true;
                        }
                    }

                    if (game.PlayerWhoJoined.TimeForThink == 0)
                    {
                        if (game.PlayerWhoJoined.Color == PieceColor.White)
                        {
                            game.GameVersion = -8;
                            game.IsFinished = true;
                        }
                        if (game.PlayerWhoJoined.Color == PieceColor.Black)
                        {
                            game.GameVersion = -9;
                            game.IsFinished = true;
                        }
                    }

                }
            }
        }

        public Game GetTimeForThink(string gameId)
        {
            return GameBeingSearched(gameId);
        }

        private static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }

        private Game GameBeingSearched(string gameId)
        {
            int index = GamesArray.FindIndex(a => a.GameId == gameId);  

            if (index > -1)
            {
               return  GamesArray[index];
            }

            return null;
        }
    }
}
