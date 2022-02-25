using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces.SlowPieces
{

    public class WhitePawn : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[] 
        { 
            new PieceMove(new Shift(0, 1), MoveType.Move ),
            new PieceMove(new Shift(-1, 1), MoveType.Kill ),
            new PieceMove(new Shift(1, 1), MoveType.Kill ),
        };


        public WhitePawn(Position position) :
            base(PieceColor.White, PieceType.Pawn, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }

    public class BlackPawn : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[]
        {
            new PieceMove(new Shift(0, -1), MoveType.Move ),
            new PieceMove(new Shift(-1, -1), MoveType.Kill ),
            new PieceMove(new Shift(1, -1), MoveType.Kill ),
        };


        public BlackPawn(Position position) :
            base(PieceColor.Black, PieceType.Pawn, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
