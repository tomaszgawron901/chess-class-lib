using ChessClassLib.Helpers;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.CastleRules
{
    public static partial class IPieceRuleExtensions
    {
        public static RightCastleRule AddRightCastleRule(this IPieceRule innerPieceRule, IKingStateProvider kingStateProvider)
        {
            return new RightCastleRule(innerPieceRule, kingStateProvider);
        }
    }

    public class RightCastleRule: NewMoveRule
    {
        protected readonly static PieceMove rightCastleMove = new PieceMove(new Position(2, 0), MoveType.Move);
        private IKingStateProvider KingStateProvider { get; }

        public RightCastleRule(IPieceRule innerPieceRule, IKingStateProvider kingStateProvider) : base(innerPieceRule)
        {
            KingStateProvider = kingStateProvider;
        }

        protected override PieceMove NewMove => rightCastleMove;

        protected override bool CanPerformNewMove()
        {
            if (KingStateProvider.IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(7, Position.Y);
            var rightRook = Board.GetPiece(rookPosition);
            if (rightRook != null && rightRook.Type == PieceType.Rook && !rightRook.WasMoved && rightRook.Color == this.Color)
            {
                return InnerPieceRule.ValidateMove(new PieceMove(new Position(1, 0), MoveType.Move)) &&
                    InnerPieceRule.ValidateMove(new PieceMove(new Position(2, 0), MoveType.Move));
            }
            return false;
        }

        public override void MoveToPosition(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == rightCastleMove.Shift)
            {
                InnerPiece.MoveToPosition(new Position(6, Position.Y));
                Board.GetPiece(new Position(7, Position.Y)).MoveToPosition(new Position(5, Position.Y));
            }
            else
            {
                InnerPiece.MoveToPosition(position);
            }
        }
    }
}
