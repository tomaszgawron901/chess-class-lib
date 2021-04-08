using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{
    public class ProtectorAtackerPiece : BasePieceDecorator
    {
        public IPiece ProtectedPiece { get; protected set; }
        public IPiece AtackedPiece { get; protected set; }
        public Board Board { get; set; }
        public ProtectorAtackerPiece(IPiece piece, IPiece protectedPiece, IPiece atackedPiece, Board board)
            : base(piece)
        {
            this.ProtectedPiece = protectedPiece;
            this.AtackedPiece = atackedPiece;
            this.Board = board;
        }

        public new IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Where(IsMoveValid);

        public new bool IsMoveValid(PieceMove move)
        {
            var destinationPostion = Position + move.Shift;
            IPiece pieceAtDestinationPosition = Board.GetPiece(destinationPostion);
            if (pieceAtDestinationPosition == AtackedPiece)
            {
                return true;
            }
            else
            {
                var backup = new Stack<PieceBackup>();
                backup.Push(new PieceBackup(Piece, Position));
                backup.Push(new PieceBackup(pieceAtDestinationPosition, destinationPostion));

                this.MoveToPosition(destinationPostion);

                bool KingIsChecked = Board
                    .Where(piece => piece != null && piece.Color != this.Color)
                    .Select(piece => piece.GetMoveTo(ProtectedPiece.Position))
                    .Any(m => m != null && m.MoveTypes.Contains(MoveType.Kill));

                while (backup.Count > 0)
                {
                    var pieceBackup = backup.Pop();
                    pieceBackup.piece.MoveToPosition(pieceBackup.position);
                }
                return !KingIsChecked;
            }
        }
    }
}
