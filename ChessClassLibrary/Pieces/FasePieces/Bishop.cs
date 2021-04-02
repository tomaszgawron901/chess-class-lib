using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Bishop : FastPiece
    {
        protected new static Point[] moveSet = new Point[] {
                new Point(-1, 1),
                new Point(1, 1),
                new Point(1, -1),
                new Point(-1, -1)
            };

        protected new static Point[] killSet = new Point[] {
                new Point(-1, 1),
                new Point(1, 1),
                new Point(1, -1),
                new Point(-1, -1)
            };

        public Bishop(PieceColor color, Point position) :
            base(color, PieceType.Bishop, position) {}
    }
}
