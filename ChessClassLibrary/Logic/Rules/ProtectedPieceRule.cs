using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule that disables moves after which given Piece could be killed.
    /// </summary>
    public class ProtectedPieceRule : ProtectAttackRule
    {
        public virtual KingState KingState { get; set; }
        public bool IsChecked { get => KingState == KingState.Checked;}
        public bool IsCheckmated { get => KingState == KingState.Checkmated; }
        public bool IsStalemated { get => KingState == KingState.Stalemated; }

        public ProtectedPieceRule(BasePieceDecorator pieceDecorator)
            :base(pieceDecorator, pieceDecorator)
        {}

        /// <summary>
        /// Updates ProtectedPiece state.
        /// </summary>
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
            else if (!Board.Any(piece => piece != null && piece.Color == Color && piece.MoveSet.Any()))
            {
                KingState = KingState.Stalemated;
            }
        }

        /// <summary>
        /// Check if given PieceMove contains 'Kill' MoveType.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool moveContainsKill(PieceMove move) => move != null && move.MoveTypes.Contains(MoveType.Kill);
    }
}
