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
        protected new static Position[] moveSet = new Position[] {
                    new Position(-1, 2), new Position(1, 2),
                    new Position(-1, -2), new Position(1, -2),
                    new Position(-2, -1), new Position(-2, 1),
                    new Position(2, -1), new Position(2, 1),
            };

        protected new static Position[] killSet = new Position[] {
                    new Position(-1, 2), new Position(1, 2),
                    new Position(-1, -2), new Position(1, -2),
                    new Position(-2, -1), new Position(-2, 1),
                    new Position(2, -1), new Position(2, 1),
            };

        public Knight(PieceColor color, Position position) :
            base(color, PieceType.Knight, position)
        { }

    }
}
