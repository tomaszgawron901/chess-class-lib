using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.SlowPieces
{
    public class Commoner: Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Shift(-1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(0, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(0, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, -1), MoveType.Move, MoveType.Kill ),
        };

        public Commoner(PieceColor color, Position position) :
            base(color, PieceType.Commoner, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
