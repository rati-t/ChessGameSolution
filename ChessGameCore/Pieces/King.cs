using System;
using System.Collections.Generic;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public class King : Piece
    {
        public int[,] Moves = new int[,] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 1 }, { -1, 0 } };
        public int[,] RookingMoves = { { 2, 0 }, { -2, 0 } };

        public King(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
            : base(horizontalPosition, verticalPosition, color,chessBoard)
        {
            Name = PieceName.King;
            IsMoved = false;
        }

        public override bool IsMoved { get; set; }
        public override List<Cell> ValidateMoves()
        {


            List<Cell> squareArray = new();

            for (int index = 0; index < Moves.GetLength(0); index++)
            {

                if (HorizontalPosition + Moves[index, 0] > 0 && HorizontalPosition + Moves[index, 0] <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] > 0 && VerticalPosition + Moves[index, 1] <= ChessBoard.VerticalMax)
                {

                    int horizontal = HorizontalPosition + Moves[index, 0];
                    int vertical = VerticalPosition + Moves[index, 1];

                    if (IsEnemy(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard) || IsEmpty(horizontal, vertical, ChessBoard))
                    {
                        if (CheckAvailable(horizontal, vertical, Color, ChessBoard))
                        {
                            Cell Move = new(horizontal, vertical);
                            squareArray.Add(Move);
                        }
                    }
                }
            }

            if (CheckAvailable(HorizontalPosition, VerticalPosition, Color, ChessBoard))
            {
                return squareArray;
            }

            for (var index = 0; index < RookingMoves.GetLength(0); index++)
            {

                if (!IsMoved)
                {
                    return squareArray;
                }

                if ((VerticalPosition == 1 || VerticalPosition == ChessBoard.VerticalMax)
                    && HorizontalPosition == 5)
                {

                    int horizontal = HorizontalPosition + RookingMoves[index, 0];
                    int vertical = VerticalPosition + RookingMoves[index, 1];

                    if (index == 0)
                    {
                        if (IsEmpty(horizontal - 1, vertical, ChessBoard) && IsEmpty(horizontal, vertical, ChessBoard)
                            && ChessBoard.Game[VerticalPosition - 1, ChessBoard.HorizontalMax - 1].Name == PieceName.Rook
                            && ChessBoard.Game[VerticalPosition - 1, ChessBoard.HorizontalMax - 1].Color == Color)
                        {
                            
                            if (CheckAvailable(horizontal, vertical, Color, ChessBoard) 
                                && !ChessBoard.Game[VerticalPosition - 1, ChessBoard.HorizontalMax - 1].IsMoved)
                            {
                                Cell Move = new(horizontal, vertical);
                                squareArray.Add(Move);
                            }
                        }
                    }
                    if (index == 1)
                    {
                        if (IsEmpty(horizontal - 1, vertical, ChessBoard)
                            && IsEmpty(horizontal + 1, vertical, ChessBoard)
                            && IsEmpty(horizontal, vertical, ChessBoard)
                            && ChessBoard.Game[VerticalPosition - 1, 0].Name == PieceName.Rook
                            && ChessBoard.Game[VerticalPosition - 1, 0].Color == Color)
                        {
                            if (CheckAvailable(horizontal, vertical, Color, ChessBoard)
                                && !ChessBoard.Game[VerticalPosition - 1, ChessBoard.HorizontalMax - 1].IsMoved)
                            {
                                Cell Move = new(horizontal, vertical);
                                squareArray.Add(Move);
                            }
                        }
                    }
                }

            }
            return squareArray;
        }

        public override List<Cell> ValidateMovesForKing()
        {

            List<Cell> squareArray = new();

            for (int index = 0; index < Moves.GetLength(0); index++)
            {

                if (HorizontalPosition + Moves[index, 0] > 0 && HorizontalPosition + Moves[index, 0] <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] > 0 && VerticalPosition + Moves[index, 1] <= ChessBoard.VerticalMax)
                {

                    int horizontal = HorizontalPosition + Moves[index, 0];
                    int vertical = VerticalPosition + Moves[index, 1];

                    if (IsAlly(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard) || IsEmpty(horizontal, vertical, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }
                }
            }
            return squareArray;
        }

        public static bool CheckAvailable(int horizontal, int vertical, PieceColor color, ChessBoard chessBoard)
        {
            for (int elem = 0; elem < chessBoard.Game.GetLength(0); elem++)
            {

                for (int index = 0; index < chessBoard.Game.GetLength(1); index++)
                {

                    if (chessBoard.Game[elem, index] != null && chessBoard.Game[elem, index].Color != color)
                    {
                        if (chessBoard.Game[elem, index].Name == PieceName.Pawn)
                        {
                            for (int counter = 0; counter < chessBoard.Game[elem, index].ValidateMovesForKing().Count; counter++)
                            {
                                if (chessBoard.Game[elem, index].ValidateMovesForKing()[counter].Horizontal == horizontal && chessBoard.Game[elem, index].ValidateMovesForKing()[counter].Vertical == vertical)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            for (int counter = 0; counter < chessBoard.Game[elem, index].ValidateMovesForKing().Count; counter++)
                            {
                                if (chessBoard.Game[elem, index].ValidateMovesForKing()[counter].Horizontal == horizontal && chessBoard.Game[elem, index].ValidateMovesForKing()[counter].Vertical == vertical)
                                {
                                    return false;
                                }
                            }
                        }
                    }

                }
            }
            return true;
        }
    }

}