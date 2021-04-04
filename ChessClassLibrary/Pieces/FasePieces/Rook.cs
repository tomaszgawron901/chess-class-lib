using ChessClassLibrary.enums;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Rook : FastPiece
    {
        protected static Position[] moveSet = new Position[] {
                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };
        protected static Position[] killSet = new Position[] {
                new Position(0, 1),
                new Position(1, 0),
                new Position(0, -1),
                new Position(-1, 0)
            };
        public Rook(PieceColor color, Position position) :
            base(color, PieceType.Rook, position)
        { }

        public override Position[] MoveSet => moveSet;

        public override Position[] KillSet => killSet;
    }
}
