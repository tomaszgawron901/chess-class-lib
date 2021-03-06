using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class Knight : Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(-1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-2, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(2, 1), MoveType.Move, MoveType.Kill ),
        };

        public Knight(PieceColor color, Position position) :
            base(color, PieceType.Knight, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
    }
}
