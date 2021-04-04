using ChessClassLibrary.enums;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public class Knight : SlowPiece
    {
        public static Position[] moveSet = new Position[] {
                    new Position(-1, 2), new Position(1, 2),
                    new Position(-1, -2), new Position(1, -2),
                    new Position(-2, -1), new Position(-2, 1),
                    new Position(2, -1), new Position(2, 1),
            };

        public static Position[] killSet = new Position[] {
                    new Position(-1, 2), new Position(1, 2),
                    new Position(-1, -2), new Position(1, -2),
                    new Position(-2, -1), new Position(-2, 1),
                    new Position(2, -1), new Position(2, 1),
            };

        public Knight(PieceColor color, Position position) :
            base(color, PieceType.Knight, position)
        { }

        public override Position[] MoveSet => moveSet;

        public override Position[] KillSet => killSet;

    }
}
