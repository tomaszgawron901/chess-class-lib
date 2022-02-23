using ChessClassLibrary.enums;
using ChessClassLibrary.Models;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.NewMoveRules
{
    public static partial class IPieceRuleExtensions
    {
        public static BlackPawnFirstMoveRule AddBlackPawnFirstMoveRule(this IPieceRule innerPieceRule)
        {
            return new BlackPawnFirstMoveRule(innerPieceRule);
        }
    }

    /// <summary>
    /// Rule that allows moving Piece two fields straight backward if and only if it was not yet moved.
    /// </summary>
    public class BlackPawnFirstMoveRule: NewMoveRule
    {
        private static readonly PieceMove longMove = new PieceMove(new Position(0, -2), MoveType.Move);

        public BlackPawnFirstMoveRule(IPieceRule innerPieceRule)
            : base(innerPieceRule)
        { }

        protected override PieceMove NewMove { get => longMove; }

        protected override bool CanPerformNewMove()
        {
            return !this.WasMoved && Board.GetPiece(Position + new Position(0, -1)) == null && Board.GetPiece(Position + new Position(0, -2)) == null;
        }
    }
}
