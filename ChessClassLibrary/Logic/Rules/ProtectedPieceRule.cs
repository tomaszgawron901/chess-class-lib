using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    public class ProtectedPieceRule : ProtectAttackRule
    {
        public virtual KingState KingState { get; set; }
        public bool IsChecked { get => KingState == KingState.Checked;}
        public bool IsCheckmated { get => KingState == KingState.Checkmated; }
        public bool IsStalemated { get => KingState == KingState.Stalemated; }

        public ProtectedPieceRule(BasePieceDecorator pieceDecorator)
            :base(pieceDecorator, pieceDecorator)
        {}

        public void UpdateState()
        {
            KingState = KingState.None;
            if (Board.Any(piece => piece != null && piece.Color != Color && moveContainsKill(piece.GetMoveTo(Position))))
            {
                KingState = KingState.Checked;
                if (!Board.Any(piece => piece != null && piece.Color == Color && piece.MoveSet.Any()))
                {
                    KingState = KingState.Checkmated;
                }

            }
            else //if (!Board.Any(piece => piece != null && piece.Color == Color && piece.MoveSet.Any()))
            {
                var aha = Board.Where(piece => piece != null && piece.Color == Color).Select(piece => (piece, piece.MoveSet));
                if (!Board.Any(piece => piece != null && piece.Color == Color && piece.MoveSet.Any()))
                {
                    KingState = KingState.Stalemated;
                }
            }
        }

        private bool moveContainsKill(PieceMove move) => move != null && move.MoveTypes.Contains(MoveType.Kill);
    }
}
