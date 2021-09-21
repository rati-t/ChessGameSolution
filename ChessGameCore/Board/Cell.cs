using System;

namespace ChessGameCore.Board
{
    public class Cell
    {
        public Cell(int horizontal, int vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
    }
}
