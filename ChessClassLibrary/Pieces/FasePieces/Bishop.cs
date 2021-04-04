using ChessClassLibrary.enums;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Bishop : FastPiece
    {
        protected static Position[] moveSet = new Position[] {
                new Position(-1, 1),
                new Position(1, 1),
                new Position(1, -1),
                new Position(-1, -1)
            };

        protected static Position[] killSet = new Position[] {
                new Position(-1, 1),
                new Position(1, 1),
                new Position(1, -1),
                new Position(-1, -1)
            };

        public Bishop(PieceColor color, Position position) :
            base(color, PieceType.Bishop, position)
        { }

        public override Position[] MoveSet => moveSet;

        public override Position[] KillSet => killSet;
    }
}
