using System;
using System.Collections.Generic;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public class Queen : Piece
    {
       
        public int[,] Moves = new int[,] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 1 }, { -1, 0 } };

        public Queen(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
            : base(horizontalPosition, verticalPosition, color, chessBoard)
        {
            Name = PieceName.Queen;
        }


        public override List<Cell> ValidateMoves()
        {

            List<Cell> squareArray = new();

            for (int index = 0; index < Moves.GetLength(0); index++)
            {

                int multiplier = 1;

                while (HorizontalPosition + Moves[index, 0] * multiplier > 0 && HorizontalPosition + Moves[index, 0] * multiplier <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] * multiplier > 0 && VerticalPosition + Moves[index, 1] * multiplier <= ChessBoard.VerticalMax)
                {

                    var horizontal = HorizontalPosition + Moves[index, 0] * multiplier;
                    var vertical = VerticalPosition + Moves[index, 1] * multiplier;

                    if (IsBounded(Color, horizontal, vertical))
                    {
                        multiplier++;
                        continue;
                    }

                    if (IsAlly(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard))
                    {

                        break;
                    }

                    if (IsEmpty(horizontal, vertical, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                        multiplier += 1;
                        continue;
                    }

                    if (IsEnemy(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                        break;
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

                int multiplier = 1;

                while (HorizontalPosition + Moves[index, 0] * multiplier > 0 && HorizontalPosition + Moves[index, 0] * multiplier <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] * multiplier > 0 && VerticalPosition + Moves[index, 1] * multiplier <= ChessBoard.VerticalMax)
                {

                    var horizontal = HorizontalPosition + Moves[index, 0] * multiplier;
                    var vertical = VerticalPosition + Moves[index, 1] * multiplier;

                    if (IsAlly(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);

                        break;
                    }

                    if (IsEmpty(horizontal, vertical, ChessBoard))
                    {
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                        multiplier += 1;
                        continue;
                    }

                    if (IsEnemy(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard))
                    {
                        if (ChessBoard.Game[vertical - 1, horizontal - 1].Name != PieceName.King)
                        {
                            break;
                        }
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                        multiplier++;
                    }
                }
            }
            return squareArray;
        }
    }

}