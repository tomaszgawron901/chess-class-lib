using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.SlowPieces
{
    public class Centaur: Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Shift(-1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-2, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(2, 1), MoveType.Move, MoveType.Kill ),
        };

        public Centaur(PieceColor color, Position position) :
            base(color, PieceType.Centaur, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
