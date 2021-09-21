using ChessGameCore.Board;
using ChessGameCore.Pieces;
using System;
using System.Collections.Generic;

namespace ChessGameCore.ContentManager
{
    public class Game
    {
        public Game(string playerIdWhoMadeGame, PieceColor choosenColor)
        {
            PlayerWhoMadeGame = MakeNewPlayer(playerIdWhoMadeGame, choosenColor);
            GameId = GenerateId();
            HorizontalMax = 8;
            VerticalMax = 8;
            GameBoard = DefaultStartingPosition();
            DefaultPieces = DefaultJsonData();
            MoveCounter = 1;
            GameVersion = 0;
            IsStarted = false;
            TimeForThink = 600;
        }
        public int TimeForThink { get; set; }
        public bool IsFinished { get; set; }
        public int GameVersion { get; set; }
        public int MoveCounter { get; set; }
        public List<DefaultPiece> DefaultPieces { get; set; }
        public Player PlayerWhoMadeGame { get; set; }
        public Player PlayerWhoJoined { get; set; }
        public int HorizontalMax { get; set; }
        public int VerticalMax { get; set; }
        public string GameId { set; get; }
        public ChessBoard GameBoard { set; get; }
        public bool IsStarted { get; set; }

        private Dictionary<string, Func<int, int, PieceColor, ChessBoard, Piece>> _pieces = new()
        {
            {
                "Queen",
                (horizontal, vertical, color, chessboard) =>
                 new Queen(horizontal, vertical, color, chessboard)
            },
            {
                "King",
                (horizontal, vertical, color, chessboard) =>
                 new King(horizontal, vertical, color, chessboard)
            },
            {
                "Rook",
                (horizontal, vertical, color, chessboard) =>
                 new Rook(horizontal, vertical, color, chessboard)
            },
            {
                "Knight",
                (horizontal, vertical, color, chessboard) =>
                 new Knight(horizontal, vertical, color, chessboard)
            },

        };

        public int PieceOwnerValidator(string playerId, int horizontal, int vertical)
        {
            if (PlayerWhoMadeGame != null)
            {
                if (PlayerWhoMadeGame.Id == playerId)
                {
                    if (GameBoard.Game[vertical - 1, horizontal - 1] == null)
                    {
                        return 0;
                    }
                    if (PlayerWhoMadeGame.Color == GameBoard.Game[vertical - 1, horizontal - 1].Color)
                    {
                        return 1;
                    }
                    return 0;
                }
            }

            if (PlayerWhoJoined != null)
            {
                if (PlayerWhoJoined.Id == playerId)
                {
                    if (GameBoard.Game[vertical - 1, horizontal - 1] == null)
                    {
                        return 0;
                    }
                    if (PlayerWhoJoined.Color == GameBoard.Game[vertical - 1, horizontal - 1].Color)
                    {
                        return 1;
                    }
                    return 0;
                }
            }
            return 0; // this line never executes
        }
        public static Player MakeNewPlayer(string playerWhoMadeGame, PieceColor choosenColor)
        {
            return new Player(playerWhoMadeGame, choosenColor);
        }
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public void JoinPlayer(string playerWhoJoined)
        {
            IsStarted = true;
            GameVersion++;

            if (PlayerWhoMadeGame.Color == PieceColor.White)
            {
                PlayerWhoJoined = new Player(playerWhoJoined, PieceColor.Black);
            }
            if (PlayerWhoMadeGame.Color == PieceColor.Black)
            {
                PlayerWhoJoined = new Player(playerWhoJoined, PieceColor.White);
            }

            PlayerWhoJoined.TimeForThink = TimeForThink;
            PlayerWhoMadeGame.TimeForThink = TimeForThink;

        }

        public ChessBoard DefaultStartingPosition()
        {

            ChessBoard GameBoard = new(HorizontalMax, VerticalMax);

            for (int verticalIndex = 0; verticalIndex < VerticalMax; verticalIndex++)
            {
                for (int horizontalIndex = 0; horizontalIndex < HorizontalMax; horizontalIndex++)
                {
                    if (verticalIndex == 1)
                    {

                        GameBoard.Game[verticalIndex, horizontalIndex] = new Pawn(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                        continue;
                    }

                    if (verticalIndex == VerticalMax - 2)
                    {

                        GameBoard.Game[verticalIndex, horizontalIndex] = new Pawn(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                        continue;
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 0 || horizontalIndex == HorizontalMax - 1)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Rook(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 0 || horizontalIndex == HorizontalMax - 1)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Rook(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                        }
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 1 || horizontalIndex == HorizontalMax - 2)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Knight(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 1 || horizontalIndex == HorizontalMax - 2)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Knight(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                        }
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 2 || horizontalIndex == HorizontalMax - 3)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Bioshop(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 2 || horizontalIndex == HorizontalMax - 3)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Bioshop(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                        }
                    }

                    if (horizontalIndex == 3)
                    {
                        if (verticalIndex == 0)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Queen(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                        }
                        if (verticalIndex == VerticalMax - 1)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new Queen(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                        }
                    }

                    if (horizontalIndex == 4)
                    {
                        if (verticalIndex == 0)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new King(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, GameBoard);
                            GameBoard.WhiteKing = new King(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, null);
                        }
                        if (verticalIndex == VerticalMax - 1)
                        {

                            GameBoard.Game[verticalIndex, horizontalIndex] = new King(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, GameBoard);
                            GameBoard.BlackKing = new King(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, null);
                        }
                    }

                }
            }
            return GameBoard;
        }


        public List<DefaultPiece> DefaultJsonData()
        {

            List<DefaultPiece> GameBoard = new();

            for (int verticalIndex = 0; verticalIndex < VerticalMax; verticalIndex++)
            {
                for (int horizontalIndex = 0; horizontalIndex < HorizontalMax; horizontalIndex++)
                {
                    if (verticalIndex == 1)
                    {

                        GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.Pawn ));
                        continue;
                    }

                    if (verticalIndex == VerticalMax - 2)
                    {

                        GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.Pawn ));
                        continue;
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 0 || horizontalIndex == HorizontalMax - 1)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.Rook ));
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 0 || horizontalIndex == HorizontalMax - 1)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.Rook ));
                        }
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 1 || horizontalIndex == HorizontalMax - 2)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.Knight ));
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 1 || horizontalIndex == HorizontalMax - 2)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.Knight ));
                        }
                    }

                    if (verticalIndex == 0)
                    {
                        if (horizontalIndex == 2 || horizontalIndex == HorizontalMax - 3)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.Bioshop ));
                        }
                    }

                    if (verticalIndex == VerticalMax - 1)
                    {
                        if (horizontalIndex == 2 || horizontalIndex == HorizontalMax - 3)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.Bioshop ));
                        }
                    }

                    if (horizontalIndex == 3)
                    {
                        if (verticalIndex == 0)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.Queen ));
                        }
                        if (verticalIndex == VerticalMax - 1)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.Queen ));
                        }
                    }

                    if (horizontalIndex == 4)
                    {
                        if (verticalIndex == 0)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.White, PieceName.King ));
                        }
                        if (verticalIndex == VerticalMax - 1)
                        {

                            GameBoard.Add(new DefaultPiece(horizontalIndex + 1, verticalIndex + 1, PieceColor.Black, PieceName.King ));
                        }
                    }

                }
            }
            return GameBoard;
        }

        public List<Cell> GetMovements(int horizontal, int vertical)
        {
            if (GameBoard.Game[vertical - 1, horizontal - 1] != null)
            {
                return GameBoard.Game[vertical - 1, horizontal - 1].ValidateMoves();
            }
            else
            {
                return new List<Cell>();
            }

        }


        public void RevivePiece(int horizontal, int vertical, string pieceToBeRevived, string Color)
        {
            GameVersion++;

            PieceName pieceName = (PieceName)Enum.Parse(typeof(PieceName), pieceToBeRevived);
            PieceColor pieceColor = (PieceColor)Enum.Parse(typeof(PieceColor), Color);

            if (_pieces.ContainsKey(pieceToBeRevived))
            {
                GameBoard.Game[vertical - 1, horizontal - 1] =  _pieces[pieceToBeRevived](horizontal, vertical, pieceColor, GameBoard);
            }
            else
            {
                GameBoard.Game[vertical - 1, horizontal - 1] = null;
            }

            int index1 = DefaultPieces.FindIndex(element => element.HorizontalPosition == horizontal
                                    && element.VerticalPosition == vertical);

            if (index1 > -1)
            {
                DefaultPieces.RemoveAt(index1);
            }

            DefaultPieces.Add(new DefaultPiece(horizontal, vertical, pieceColor, pieceName));
        }

        public int[] ChangeSituation(int fromHorizontal, int fromVertical, int toHorizontal, int toVertical)
        {
            GameVersion++;

            Piece addressPiece = GameBoard.Game[fromVertical - 1, fromHorizontal - 1];

            GameBoard.Game[fromVertical - 1, fromHorizontal - 1].IsMoved = true;

            if (addressPiece.Name == PieceName.King)
            {
                if (addressPiece.Color == PieceColor.White)
                {
                    GameBoard.WhiteKing.HorizontalPosition = toHorizontal;
                    GameBoard.WhiteKing.VerticalPosition = toVertical;
                }

                if (addressPiece.Color == PieceColor.Black)
                {
                    GameBoard.BlackKing.HorizontalPosition = toHorizontal;
                    GameBoard.BlackKing.VerticalPosition = toVertical;
                }
            }

            if (addressPiece.Name == PieceName.King
                        && Math.Abs(toHorizontal - addressPiece.HorizontalPosition) > 1)
            {

                int measure = toHorizontal - addressPiece.HorizontalPosition;

                addressPiece.HorizontalPosition = toHorizontal;
                addressPiece.VerticalPosition = toVertical;
                GameBoard.Game[fromVertical - 1, fromHorizontal - 1] = null;
                GameBoard.Game[toVertical - 1, toHorizontal - 1] = addressPiece;

                int index2 = DefaultPieces.FindIndex(element => element.HorizontalPosition == fromHorizontal
                                    && element.VerticalPosition == fromVertical);
                DefaultPieces[index2].HorizontalPosition = toHorizontal;
                DefaultPieces[index2].VerticalPosition = toVertical;

                if (measure > 0)
                {
                    GameVersion--;
                    ChangeSituation(toHorizontal + 1, toVertical, toHorizontal - 1, toVertical);
                    return new int[4] { toHorizontal + 1, toVertical, toHorizontal - 1, toVertical };
                }
                if (measure < 0)
                {
                    GameVersion--;
                    ChangeSituation(toHorizontal - 2, toVertical, toHorizontal + 1, toVertical);
                    return new int[4] { toHorizontal - 2, toVertical, toHorizontal + 1, toVertical };
                }

            }

            if (addressPiece.Name == PieceName.Pawn && (toVertical == 1 || toVertical == GameBoard.VerticalMax))
            {

                addressPiece.HorizontalPosition = toHorizontal;
                addressPiece.VerticalPosition = toVertical;
                addressPiece.VerticalPosition = toVertical;
                GameBoard.Game[fromVertical - 1, fromHorizontal - 1] = null;
                GameBoard.Game[toVertical - 1, toHorizontal - 1] = addressPiece;
             
                int index3 = DefaultPieces.FindIndex(element => element.HorizontalPosition == toHorizontal
                                    && element.VerticalPosition == toVertical);

                if (index3 > -1)
                {
                    DefaultPieces.RemoveAt(index3);
                }

                int index4 = DefaultPieces.FindIndex(element => element.HorizontalPosition == fromHorizontal
                                        && element.VerticalPosition == fromVertical);
                DefaultPieces[index4].HorizontalPosition = toHorizontal;
                DefaultPieces[index4].VerticalPosition = toVertical;

                return new int[2] { toHorizontal, toVertical };
            }

            addressPiece.HorizontalPosition = toHorizontal;
            addressPiece.VerticalPosition = toVertical;
            GameBoard.Game[fromVertical - 1, fromHorizontal - 1] = null;
            GameBoard.Game[toVertical - 1, toHorizontal - 1] = addressPiece;

            int index1 = DefaultPieces.FindIndex(element => element.HorizontalPosition == toHorizontal
                                    && element.VerticalPosition == toVertical);

            if (index1 > -1)
            {
                DefaultPieces.RemoveAt(index1);
            }

            int index = DefaultPieces.FindIndex(element => element.HorizontalPosition == fromHorizontal
                                    && element.VerticalPosition == fromVertical);
            DefaultPieces[index].HorizontalPosition = toHorizontal;
            DefaultPieces[index].VerticalPosition = toVertical;

            return Array.Empty<int>();

        }
    }
}

