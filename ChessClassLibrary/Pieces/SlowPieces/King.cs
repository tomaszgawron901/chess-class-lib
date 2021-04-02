using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public abstract class King : SlowPiece
    {
        protected new static Point[] moveSet = new Point[] {
                new Point(-1, 1), new Point(0, 1), new Point(1, 1),
                new Point(-1, 0), new Point(1, 0),
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
            };

        protected new static Point[] killSet = new Point[] {
                new Point(-1, 1), new Point(0, 1), new Point(1, 1),
                new Point(-1, 0), new Point(1, 0),
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
            };

        public King(PieceColor color, Point position) :
            base(color, PieceType.King, position) {}
    }
}
