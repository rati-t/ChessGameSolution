using System;
using System.Collections.Generic;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public abstract class Piece
    {
        public Piece(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
        {
            HorizontalPosition = horizontalPosition;
            VerticalPosition = verticalPosition;
            Color = color;
            ChessBoard = chessBoard;
        }
        virtual public bool IsMoved { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get; set; }
        public PieceName Name { get; set; }
        public PieceColor Color { get; set; }
        public ChessBoard ChessBoard { get; set; }

        public static bool IsEnemy(int fromHorizontal, int fromVertical, int toHorizontal, int toVertical, ChessBoard chessBoard)
        {
            if (chessBoard.Game[fromVertical - 1, fromHorizontal - 1] != null && chessBoard.Game[toVertical - 1, toHorizontal - 1] != null)
            {
                return (chessBoard.Game[fromVertical - 1, fromHorizontal - 1].Color != chessBoard.Game[toVertical - 1, toHorizontal - 1].Color);
            }
            return false;
        }

        public static bool IsEmpty(int horizontal, int vertical, ChessBoard ChessBoard)
        {
            return ChessBoard.Game[vertical - 1, horizontal - 1] == null;
        }

        public static bool IsAlly(int fromHorizontal, int fromVertical, int toHorizontal, int toVertical, ChessBoard ChessBoard)
        {
            if (ChessBoard.Game[fromVertical - 1, fromHorizontal - 1] != null && ChessBoard.Game[toVertical - 1, toHorizontal - 1] != null)
            {
                return ChessBoard.Game[fromVertical - 1, fromHorizontal - 1].Color == ChessBoard.Game[toVertical - 1, toHorizontal - 1].Color;
            }
            return false;
        }

        abstract public List<Cell> ValidateMoves();


        abstract public List<Cell> ValidateMovesForKing();

        virtual public bool IsBounded(PieceColor color, int horizontal, int vertical)
        {
            if (Color == PieceColor.White)
            {
                    Piece PieceThatMoves = ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1];
                    Piece PieceThatMightGetKilled = ChessBoard.Game[vertical - 1, horizontal - 1];
                    ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = null;
                    ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMoves;

                    if (!King.CheckAvailable(ChessBoard.WhiteKing.HorizontalPosition, ChessBoard.WhiteKing.VerticalPosition, PieceColor.White, ChessBoard))
                    {
                        ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMightGetKilled; 
                        ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = PieceThatMoves;
                        return true;
                    }

                    ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMightGetKilled;
                    ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = PieceThatMoves;
                
                    return false;
            }

            if (Color == PieceColor.Black)
            {
                    Piece PieceThatMoves = ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1];
                    Piece PieceThatMightGetKille = ChessBoard.Game[vertical - 1, horizontal - 1];
                    ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = null;
                    ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMoves;

                    if (!King.CheckAvailable(ChessBoard.BlackKing.HorizontalPosition, ChessBoard.BlackKing.VerticalPosition, PieceColor.Black, ChessBoard))
                    {
                        ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMightGetKille; 
                        ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = PieceThatMoves;
                        return true;
                    }

                    ChessBoard.Game[vertical - 1, horizontal - 1] = PieceThatMightGetKille;
                    ChessBoard.Game[VerticalPosition - 1, HorizontalPosition - 1] = PieceThatMoves;
                
                return false;
            }
            return false; // this line never executes
        }

    }
}
