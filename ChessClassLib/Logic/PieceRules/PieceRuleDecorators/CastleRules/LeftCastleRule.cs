using ChessClassLib.Helpers;
using ChessClassLib.Enums;
using ChessClassLib.Models;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.CastleRules
{
    public static partial class IPieceRuleExtensions
    {
        public static LeftCastleRule AddLeftCastleRule(this IPieceRule innerPieceRule, IKingStateProvider kingStateProvider)
        {
            return new LeftCastleRule(innerPieceRule, kingStateProvider);
        }
    }

    public class LeftCastleRule : NewMoveRule
    {
        protected readonly static PieceMove leftCastleMove = new PieceMove(new Shift(-2, 0), MoveType.Move);
        private IKingStateProvider KingStateProvider { get; }

        public LeftCastleRule(IPieceRule innerPieceRule, IKingStateProvider kingStateProvider) : base(innerPieceRule)
        {
            KingStateProvider = kingStateProvider;
        }

        protected override PieceMove NewMove => leftCastleMove;

        protected override bool CanPerformNewMove()
        {
            if (KingStateProvider.IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(0, Position.Y);
            var leftRook = Board.GetPiece(rookPosition);
            if (leftRook != null && leftRook.Type == PieceType.Rook && !leftRook.WasMoved && leftRook.Color == Color && Board.GetPiece(new Position(1, Position.Y)) == null)
            {
                return InnerPieceRule.ValidateMove(new PieceMove(new Shift(-1, 0), MoveType.Move)) &&
                    InnerPieceRule.ValidateMove(new PieceMove(new Shift(-2, 0), MoveType.Move));
            }
            return false;
        }

        public override void MoveToPosition(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == leftCastleMove.Shift)
            {
                InnerPiece.MoveToPosition(new Position(2, Position.Y));
                Board.GetPiece(new Position(0, Position.Y)).MoveToPosition(new Position(3, Position.Y));
            }
            else
            {
                InnerPiece.MoveToPosition(position);
            }
        }
    }
}
