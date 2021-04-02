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
        public Pawn(PieceColor color, Point position) :
            base(color, PieceType.Pawn, position)
        { }

        protected static Point[] firstMoveSet;
        public override Point[] MoveSet
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
        protected new static Point[] firstMoveSet = new Point[] { new Point(0, 1), new Point(0, 2) };
        protected new static Point[] moveSet = new Point[] { new Point(0, 1) };
        protected new static Point[] killSet = new Point[] { new Point(-1, 1), new Point(1, 1) };


        public WhitePawn(Point position) :
            base(PieceColor.White, position)
        { }
    }

    public class BlackPawn : Pawn
    {
        protected new static Point[] firstMoveSet = new Point[] { new Point(0, -1), new Point(0, -2) };
        protected new static Point[] moveSet = new Point[] { new Point(0, -1) };
        protected new static Point[] killSet = new Point[] { new Point(-1, -1), new Point(1, -1) };

        public BlackPawn(Point position) :
            base(PieceColor.Black, position)
        { }
    }
}
