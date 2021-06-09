using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class Centaur: Piece
    {
        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(-1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, 2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(1, -2), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(-2, 1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(2, -1), MoveType.Move, MoveType.Kill ),
            new PieceMove(new Position(2, 1), MoveType.Move, MoveType.Kill ),
        };

        public Centaur(PieceColor color, Position position) :
            base(color, PieceType.Centaur, position)
        { }

        public override IEnumerable<PieceMove> MoveSet => moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
    }
}
