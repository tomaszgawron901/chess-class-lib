using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public abstract class Pawn : SlowPiece
    {
        public Pawn(PieceColor color, Position position) :
            base(color, PieceType.Pawn, position)
        { }

        protected static Position[] firstMoveSet;
        public override Position[] MoveSet
        {
            get
            {
                if (this.WasMoved)
                {
                    return moveSet;
                }
                else
                {
                    return firstMoveSet;
                }
            }
        }
    }

    public class WhitePawn : Pawn
    {
        protected new static Position[] firstMoveSet = new Position[] { new Position(0, 1), new Position(0, 2) };
        protected new static Position[] moveSet = new Position[] { new Position(0, 1) };
        protected new static Position[] killSet = new Position[] { new Position(-1, 1), new Position(1, 1) };


        public WhitePawn(Position position) :
            base(PieceColor.White, position)
        { }
    }

    public class BlackPawn : Pawn
    {
        protected new static Position[] firstMoveSet = new Position[] { new Position(0, -1), new Position(0, -2) };
        protected new static Position[] moveSet = new Position[] { new Position(0, -1) };
        protected new static Position[] killSet = new Position[] { new Position(-1, -1), new Position(1, -1) };

        public BlackPawn(Position position) :
            base(PieceColor.Black, position)
        { }
    }
}
