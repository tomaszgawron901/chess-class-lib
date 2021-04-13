using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Queen : Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(-1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(0, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(0, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, 0), MoveType.Move, MoveType.Kill ),
        };

        public Queen(PieceColor color, Position position) :
            base(color, PieceType.Queen, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
    }
}
