using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Rook : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Position(0, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(0, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, 0), MoveType.Move, MoveType.Kill ),
        };

        public Rook(PieceColor color, Position position) :
            base(color, PieceType.Rook, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
