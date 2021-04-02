using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Queen : FastPiece
    {
        protected static Position[] moveSet = new Position[] {
                new Position(-1, 1),
                new Position(1, 1),
                new Position(1, -1),
                new Position(-1, -1),

                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };

        protected static Position[] killSet = new Position[] {
                new Position(-1, 1),
                new Position(1, 1),
                new Position(1, -1),
                new Position(-1, -1),

                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };

        public Queen(PieceColor color, Position position) :
            base(color, PieceType.Queen, position){}

        public override Position[] MoveSet => moveSet;

        public override Position[] KillSet => killSet;
    }
}
