using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Rook : FastPiece
    {
        protected new static Position[] moveSet = new Position[] {
                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };
        protected new static Position[] killSet = new Position[] {
                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };
        public Rook(PieceColor color, Position position) :
            base(color, PieceType.Rook, position){}
    }
}
