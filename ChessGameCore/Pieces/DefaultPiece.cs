using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameCore.ContentManager;
using ChessGameCore.Board;

namespace ChessGameCore.Pieces
{
    public class DefaultPiece
    {
        public DefaultPiece(int horizontalPosition, int verticalPosition, PieceColor color, PieceName name)
        {
            HorizontalPosition = horizontalPosition;
            VerticalPosition = verticalPosition;
            Name = name;
            Color = color;
        }

        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get; set; }
        public PieceName Name { get; set; }
        public PieceColor Color { get; set; }

    }
}
