using ChessClassLibrary.Pieces;

namespace ChessClassLibrary
{
    public class PieceBackup
    {
        public Piece piece;
        public Position position;

        public PieceBackup(Piece piece, Position position)
        {
            this.piece = piece;
            this.position = position;
        }
    }
}
