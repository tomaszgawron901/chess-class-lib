using ChessClassLibrary.Pieces;

namespace ChessClassLibrary
{
    public class PieceBackup
    {
        public IPiece piece;
        public Position position;

        public PieceBackup(IPiece piece, Position position)
        {
            this.piece = piece;
            this.position = position;
        }
    }
}
