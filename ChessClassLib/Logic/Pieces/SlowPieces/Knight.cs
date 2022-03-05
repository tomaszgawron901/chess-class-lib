using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.SlowPieces
{
    public class Knight : Piece
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

        public Knight(PieceColor color, Position position) :
            base(color, PieceType.Knight, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
