using ChessClassLibrary.Boards;
using ChessClassLibrary.Pieces;

namespace ChessClassLib.Logic.PieceRules
{
    public abstract class PieceRuleDecorator : PieceRule
    {
        protected IPieceRule InnerPieceRule { get; }

        public PieceRuleDecorator(IPieceRule innerPieceRule)
        {
            InnerPieceRule = innerPieceRule;
        }

        protected override IPiece InnerPiece { get => InnerPieceRule; }

        public override IBoard Board { get => InnerPieceRule.Board; }
    }
}
