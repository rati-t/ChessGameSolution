using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameCore.Pieces;

namespace ChessGameCore.Board
{
    public class ChessBoard
    {
        public ChessBoard(int horizontalMax, int verticalMax)
        {
            HorizontalMax = horizontalMax;
            VerticalMax = verticalMax;
            Game = MakeBoard();
        }
        public King WhiteKing { get; set; }
        public King BlackKing { get; set; }
        public int HorizontalMax { get; set; }
        public int VerticalMax { get; set; }

        public Piece[,] Game { get; set; }

        public Piece[,] MakeBoard()
        {
            Piece[,] gameArray = new Piece[VerticalMax, HorizontalMax];

            for (int verticalIndex = 0; verticalIndex < VerticalMax; verticalIndex++)
            {
                for (int horizontalIndex = 0; horizontalIndex < HorizontalMax; horizontalIndex++)
                {
                    gameArray[verticalIndex, horizontalIndex] = null;
                }
            }
            return gameArray;
        }

    }
}