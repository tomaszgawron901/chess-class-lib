using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.FasePieces
{
    public class Rook : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Shift(0, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, 0), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(0, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-1, 0), MoveType.Move, MoveType.Kill ),
        };

        public Rook(PieceColor color, Position position) :
            base(color, PieceType.Rook, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
