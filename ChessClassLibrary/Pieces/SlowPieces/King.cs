using ChessClassLibrary.enums;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class King : SlowPiece
    {
        protected static Position[] moveSet = new Position[] {
                new Position(-1, 1), new Position(0, 1), new Position(1, 1),
                new Position(-1, 0), new Position(1, 0),
                new Position(-1, -1), new Position(0, -1), new Position(1, -1),
            };

        protected static Position[] killSet = new Position[] {
                new Position(-1, 1), new Position(0, 1), new Position(1, 1),
                new Position(-1, 0), new Position(1, 0),
                new Position(-1, -1), new Position(0, -1), new Position(1, -1),
            };

        public King(PieceColor color, Position position) :
            base(color, PieceType.King, position)
        { }

        public override Position[] MoveSet => moveSet;

        public override Position[] KillSet => killSet;
    }
}
