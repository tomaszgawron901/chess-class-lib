using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.FasePieces
{
    public class Bishop : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Shift(-1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(1, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Shift(-1, -1), MoveType.Move, MoveType.Kill ),
        };

        public Bishop(PieceColor color, Position position) :
            base(color, PieceType.Bishop, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
