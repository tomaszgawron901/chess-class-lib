using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.SlowPieces
{

    public class WhitePawn : Piece
    {
        protected readonly static IEnumerable<PieceMove> moveSet = new PieceMove[] 
        { 
            new PieceMove(new Position(0, 1), MoveType.Move ),
            new PieceMove(new Position(-1, 1), MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Kill ),
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
            new PieceMove(new Position(0, -1), MoveType.Move ),
            new PieceMove(new Position(-1, -1), MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Kill ),
        };


        public BlackPawn(Position position) :
            base(PieceColor.Black, PieceType.Pawn, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet;
    }
}
