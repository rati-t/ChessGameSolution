using System;
using System.Collections.Generic;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public class Knight : Piece
    {
        public int[,] Moves = new int[,] { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 } };

        public Knight(int horizontalPosition, int verticalPosition, PieceColor color, ChessBoard chessBoard)
            : base(horizontalPosition, verticalPosition, color, chessBoard)
        {
            Name = PieceName.Knight;
        }


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

                    if (IsBounded(Color, horizontal, vertical))
                    {
                        continue;
                    }

                    if (IsEnemy(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard) 
                        || IsEmpty(horizontal, vertical, ChessBoard))
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

                    if (IsEnemy(horizontal, vertical, HorizontalPosition, VerticalPosition, ChessBoard))
                    {
                        if(ChessBoard.Game[vertical - 1, horizontal - 1].Name != PieceName.King)
                        {
                            continue;
                        }
                        Cell Move = new(horizontal, vertical);
                        squareArray.Add(Move);
                    }
                }
            }
            return squareArray;
        }
    }

}