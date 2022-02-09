using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule that disables moves all after which given Piece could be killed except moves after which attacked Piece could be killed.
    /// </summary>
    public class ProtectAttackRule : BasePieceRule
    {
        public IPiece ProtectedPiece { get; set; }
        public virtual IPiece AtackedPiece { get; set; }
        public ProtectAttackRule(BasePieceDecorator piece, IPiece protectedPiece, IPiece atackedPiece=null)
            : base(piece)
        {
            this.ProtectedPiece = protectedPiece;
            this.AtackedPiece = atackedPiece;
        }

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Where(isProtectedPieceSafeAfterMove);

        /// <summary>
        /// Checks if protected Piece couldn't be killed or attacked Piece could be killed after performing PieceMove.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        protected bool isProtectedPieceSafeAfterMove(PieceMove move)
        {
            var destinationPostion = Position + move.Shift;
            IPiece pieceAtDestinationPosition = Board.GetPiece(destinationPostion);
            IPiece currentPiece = Board.GetPiece(Position);
            if (pieceAtDestinationPosition != null && pieceAtDestinationPosition == AtackedPiece)
            {
                return true;
            }
            else
            {
                var backup = new Stack<PieceBackup>();
                backup.Push(new PieceBackup(currentPiece, Position));
                backup.Push(new PieceBackup(pieceAtDestinationPosition, destinationPostion));


                Board.SetPiece(null, Position);
                currentPiece.Position = destinationPostion;
                Board.SetPiece(currentPiece);

                bool KingIsChecked = Board
                    .Where(piece => piece != null && piece.Color != this.Color)
                    .Select(piece => piece.GetMoveTo(ProtectedPiece.Position))
                    .Any(m => m != null && m.MoveTypes.Contains(MoveType.Kill));

                while (backup.Count > 0)
                {
                    var pieceBackup = backup.Pop();
                    if (pieceBackup.piece != null)
                    {
                        pieceBackup.piece.Position = pieceBackup.position;
                    }
                    Board.SetPiece(pieceBackup.piece, pieceBackup.position);
                }
                return !KingIsChecked;
            }
        }

        public override PieceMove MoveModifier(PieceMove move)
        {
            if (isProtectedPieceSafeAfterMove(move))
            {
                return move;
            }
            return null;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            return InnerPieceDecorator.ValidateNewMove(move) && isProtectedPieceSafeAfterMove(move);
        }
    }
}
