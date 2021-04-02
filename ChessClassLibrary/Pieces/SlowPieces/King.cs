using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class King : SlowPiece
    {
        protected new static Position[] moveSet = new Position[] {
                new Position(-1, 1), new Position(0, 1), new Position(1, 1),
                new Position(-1, 0), new Position(1, 0),
                new Position(-1, -1), new Position(0, -1), new Position(1, -1),
            };

        protected new static Position[] killSet = new Position[] {
                new Position(-1, 1), new Position(0, 1), new Position(1, 1),
                new Position(-1, 0), new Position(1, 0),
                new Position(-1, -1), new Position(0, -1), new Position(1, -1),
            };

        public King(PieceColor color, Position position) :
            base(color, PieceType.King, position) {}
    }
}
