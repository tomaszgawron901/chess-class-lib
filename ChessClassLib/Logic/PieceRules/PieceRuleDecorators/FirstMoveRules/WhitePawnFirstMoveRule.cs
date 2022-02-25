using ChessClassLib.Enums;
using ChessClassLib.Models;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.NewMoveRules
{
    public static partial class IPieceRuleExtensions
    {
        public static WhitePawnFirstMoveRule AddWhitePawnFirstMoveRule(this IPieceRule innerPieceRule)
        {
            return new WhitePawnFirstMoveRule(innerPieceRule);
        }
    }

    /// <summary>
    /// Rule that allows moving Piece two fields straight forward if and only if it was not yet moved.
    /// </summary>
    public class WhitePawnFirstMoveRule: NewMoveRule
    {
        protected readonly static PieceMove longMove = new PieceMove(new Shift(0, 2), MoveType.Move);

        public WhitePawnFirstMoveRule(IPieceRule pieceDecorator)
            : base(pieceDecorator)
        { }

        protected override PieceMove NewMove { get => longMove; }

        protected override bool CanPerformNewMove()
        {
            return !this.WasMoved && Board.GetPiece(Position + new Shift(0, 1)) == null && Board.GetPiece(Position + new Shift(0, 2)) == null;
        }
    }
}
