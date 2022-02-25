using ChessClassLibrary.Enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class Commoner: Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Position(-1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(0, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(0, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Move, MoveType.Kill ),
        };

        public Commoner(PieceColor color, Position position) :
            base(color, PieceType.Commoner, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
