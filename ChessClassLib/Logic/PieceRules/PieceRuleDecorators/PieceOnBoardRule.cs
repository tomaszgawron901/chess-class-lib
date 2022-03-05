using ChessClassLib.Models;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    public static partial class IPieceRuleExtensions
    {
        public static PieceOnBoardRule AddPieceOnBoardRule
            (this IPieceRule innerPieceRule)
        {
            return new PieceOnBoardRule(innerPieceRule);
        }
    }

    public class PieceOnBoardRule : PieceRuleDecorator
    {
        public PieceOnBoardRule(IPieceRule innerPieceRule)
            : base(innerPieceRule)
        {}

        public override PieceMove ConstrainMove(PieceMove move)
        {
            if (Board.IsInRange(Position + move.Shift))
            {
                return move;
            }
            return null;
        }

        public override bool ValidateMove(PieceMove move)
        {
            return Board.IsInRange(Position + move.Shift);
        }

        public override void MoveToPosition(Position position)
        {
            Board.SetPiece(Board.GetPiece(Position), position);
            Board.SetPiece(null, Position);
            InnerPiece.MoveToPosition(position);
        }
    }
}
