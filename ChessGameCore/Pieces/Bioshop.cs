using System;
using System.Collections.Generic;
using ChessGameCore.Board;
using ChessGameCore;

namespace ChessGameCore.Pieces
{
    public class Bioshop : Piece
    {
        public int[,] Moves = new int[,] { { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 }, };

        public Bioshop(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
            : base(horizontalPosition, verticalPosition, color, chessBoard)
        {
            Name = PieceName.Bioshop;
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

                int Multiplier = 1;

                while (HorizontalPosition + Moves[index, 0] * Multiplier > 0 && HorizontalPosition + Moves[index, 0] * Multiplier <= ChessBoard.HorizontalMax
                    && VerticalPosition + Moves[index, 1] * Multiplier > 0 && VerticalPosition + Moves[index, 1] * Multiplier <= ChessBoard.VerticalMax)
                {

                    var horizontal = HorizontalPosition + Moves[index, 0] * Multiplier;
                    var vertical = VerticalPosition + Moves[index, 1] * Multiplier;


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
                        Multiplier += 1;
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
                        Multiplier++;
                    }
                }
            }
            return squareArray;
        }

    }

}