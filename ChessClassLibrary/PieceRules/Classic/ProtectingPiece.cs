using ChessClassLibrary.Boards;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{
    public class ProtectingPiece : BasePieceDecorator
    {
        public IPiece ProtectedPiece { get; protected set; }
        public Board Board { get; set; }
        public ProtectingPiece(IPiece piece, IPiece protectedPiece, Board board)
            : base(piece)
        {
            this.ProtectedPiece = protectedPiece;
        }

        public new IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Where(IsMoveValid);

        public new bool IsMoveValid(PieceMove move)
        {
            var backup = new Stack<PieceBackup>();
            IPiece pieceAtDestinationPosition = board.GetPiece(destination);

            backup.Push(new PieceBackup(Piece, Position));
            backup.Push(new PieceBackup(pieceAtDestinationPosition, destination));


            this.MoveToPosition(destination);

            bool KingIsChecked = false;
            if (Color == PieceColor.White)
            {
                KingIsChecked = board.WhiteKing.IsChecked;
            }
            else if (Color == PieceColor.Black)
            {
                KingIsChecked = board.BlackKing.IsChecked;
            }

            while (backup.Count > 0)
            {
                var pieceBackup = backup.Pop();
                pieceBackup.piece.MoveToPosition(pieceBackup.position);
            }
            return KingIsChecked;
        }
    }
}
