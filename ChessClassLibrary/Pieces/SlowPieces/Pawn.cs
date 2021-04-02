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

        public abstract Position[] FirstMoveSet { get; }
    }

    public class WhitePawn : Pawn
    {
        protected readonly static Position[] firstMoveSet = new Position[] { new Position(0, 1), new Position(0, 2) };
        protected readonly static Position[] moveSet = new Position[] { new Position(0, 1) };
        protected readonly static Position[] killSet = new Position[] { new Position(-1, 1), new Position(1, 1) };


        public WhitePawn(Position position) :
            base(PieceColor.White, position)
        { }

        public override Position[] FirstMoveSet => firstMoveSet;
        public override Position[] KillSet => killSet;
        public override Position[] MoveSet
        {
            get
            {
                if (this.wasMoved)
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

    public class BlackPawn : Pawn
    {
        protected readonly static Position[] firstMoveSet = new Position[] { new Position(0, -1), new Position(0, -2) };
        protected readonly static Position[] moveSet = new Position[] { new Position(0, -1) };
        protected readonly static Position[] killSet = new Position[] { new Position(-1, -1), new Position(1, -1) };

        public BlackPawn(Position position) :
            base(PieceColor.Black, position)
        { }

        public override Position[] FirstMoveSet => firstMoveSet;
        public override Position[] KillSet => killSet;
        public override Position[] MoveSet
        {
            get
            {
                if (this.wasMoved)
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
}
