using ChessClassLibrary.Enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class King : Piece
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

        public King(PieceColor color, Position position) :
            base(color, PieceType.King, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
