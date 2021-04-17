using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.SlowPieces
{

    public class WhitePawn : Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[] 
        { 
            new PieceMove(new Position(0, 1), MoveType.Move ),
            new PieceMove(new Position(-1, 1), MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Kill ),
        };


        public WhitePawn(Position position) :
            base(PieceColor.White, PieceType.Pawn, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
    }

    public class BlackPawn : Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(0, -1), MoveType.Move ),
            new PieceMove(new Position(-1, -1), MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Kill ),
        };


        public BlackPawn(Position position) :
            base(PieceColor.Black, PieceType.Pawn, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
    }
}
