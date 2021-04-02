using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class Knight : SlowPiece
    {
        protected new static Point[] moveSet = new Point[] {
                    new Point(-1, 2), new Point(1, 2),
                    new Point(-1, -2), new Point(1, -2),
                    new Point(-2, -1), new Point(-2, 1),
                    new Point(2, -1), new Point(2, 1),
            };

        protected new static Point[] killSet = new Point[] {
                    new Point(-1, 2), new Point(1, 2),
                    new Point(-1, -2), new Point(1, -2),
                    new Point(-2, -1), new Point(-2, 1),
                    new Point(2, -1), new Point(2, 1),
            };

        public Knight(PieceColor color, Point position) :
            base(color, PieceType.Knight, position)
        { }

    }
}
