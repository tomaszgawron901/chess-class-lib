using ChessClassLib.Pieces;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.ProtectionRules
{
    public static partial class IPieceRuleExtensions
    {
        public static ProtectAttackRule AddProtectAttackRule(this IPieceRule innerPieceRule, IPiece protectedPiece, IPiece atackedPiece = null)
        {
            return new ProtectAttackRule(innerPieceRule, protectedPiece, atackedPiece);
        }
    }

    /// <summary>
    /// Rule that disables moves all after which given Piece could be killed except moves after which attacked Piece could be killed.
    /// </summary>
    public class ProtectAttackRule : ProtectionRule
    {
        public IPiece AtackedPiece { get; set; }
        public ProtectAttackRule(IPieceRule innerPieceRule, IPiece protectedPiece, IPiece atackedPiece=null)
            : base(innerPieceRule)
        {
            this.ProtectedPiece = protectedPiece;
            this.AtackedPiece = atackedPiece;
        }

        public override IEnumerable<PieceMove> MoveSet => InnerPiece.MoveSet.Where(isProtectedPieceSafeAfterMove);

        protected override bool isProtectedPieceSafeAfterMove(PieceMove move)
        {
            var destinationPostion = Position + move.Shift;
            IPiece pieceAtDestinationPosition = Board.GetPiece(destinationPostion);
            if (pieceAtDestinationPosition != null && pieceAtDestinationPosition == AtackedPiece)
            {
                return true;
            }
            else
            {
                return base.isProtectedPieceSafeAfterMove(move);
            }
        }
    }
}
