using System;
using System.Collections.Generic;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public class Pawn : Piece
    {
        public int[,] Moves = new int[,] { { 0, 1 }, { 0, 2 }, { -1, 1 }, { 1, 1 } };

        public Pawn(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
        : base(horizontalPosition, verticalPosition, color, chessBoard)
        {
            Name = PieceName.Pawn;
        }

        public override List<Cell> ValidateMoves()
        {

            int multiplier = 1;

            if (Color == PieceColor.Black)
            {
                multiplier = -1;
            }

            List<Cell> squareArray = new();

            for (int index = 0; index < Moves.GetLength(0); index++)
            {

                if (HorizontalPosition + Moves[index, 0] * multiplier > 0 && HorizontalPosition + Moves[index, 0] * multiplier <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] * multiplier > 0 && VerticalPosition + Moves[index, 1] * multiplier <= ChessBoard.VerticalMax)
                {

                    var horizontal = HorizontalPosition + Moves[index, 0] * multiplier;
                    var vertical = VerticalPosition + Moves[index, 1] * multiplier;

                    if (IsBounded(Color, horizontal, vertical))
                    {
                        continue;
                    }


                    if (index == 0 && IsEmpty(horizontal, vertical, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }

                    if (index == 1 && IsEmpty(horizontal, vertical, ChessBoard) && IsEmpty(horizontal, vertical - multiplier, ChessBoard) && (VerticalPosition == 2 || VerticalPosition == ChessBoard.VerticalMax - 1))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }

                    if (index >= 2 && IsEnemy(HorizontalPosition, VerticalPosition, horizontal, vertical, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }

                }
            }

            return squareArray;
        }
        public override List<Cell> ValidateMovesForKing()
        {

            int multiplier = 1;

            if (Color == PieceColor.Black)
            {
                multiplier = -1;
            }

            List<Cell> squareArray = new();

            for (int index = 0; index < Moves.GetLength(0); index++)
            {

                if (HorizontalPosition + Moves[index, 0] * multiplier > 0 && HorizontalPosition + Moves[index, 0] * multiplier <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] * multiplier > 0 && VerticalPosition + Moves[index, 1] * multiplier <= ChessBoard.VerticalMax)
                {

                    var horizontal = HorizontalPosition + Moves[index, 0] * multiplier;
                    var vertical = VerticalPosition + Moves[index, 1] * multiplier;


                    if (index >= 2)
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }

                }
            }

            return squareArray;

        }

    }

}