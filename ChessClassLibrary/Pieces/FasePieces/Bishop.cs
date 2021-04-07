using ChessClassLibrary.enums;
using System.Collections.Generic;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Bishop : Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(-1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, -1), MoveType.Move, MoveType.Kill ),
        };

        public Bishop(PieceColor color, Position position) :
            base(color, PieceType.Bishop, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
