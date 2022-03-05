using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    public static partial class IPieceRuleExtensions
    {
        public static MoveRule AddMoveRule(this IPieceRule innerPieceRule)
        {
            return new MoveRule(innerPieceRule);
        }
    }

    /// <summary>
    /// Rule which checks if Piece could be moved by PieceMove with 'Move' MoveType.
    /// </summary>
    public class MoveRule: PieceRuleDecorator
    {
        public MoveRule(IPieceRule innerPieceRule)
            : base(innerPieceRule)
        { }

        public override PieceMove ConstrainMove(PieceMove move)
        {
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);
            if (pieceAtDestination != null || move.MoveTypes.Contains(MoveType.Move))
            {
                if (pieceAtDestination == null)
                {
                    return new PieceMove(move.Shift, MoveType.Move);
                }
                var newMoveTypes = move.MoveTypes.Where(m => m != MoveType.Move).ToArray();
                if (newMoveTypes.Length != 0)
                {
                    return new PieceMove(move.Shift, newMoveTypes);
                }
            }
            return null;
        }

        public override bool ValidateMove(PieceMove move)
        {
            if (!InnerPieceRule.ValidateMove(move)) return false;
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);

            var containsMove = move.MoveTypes.Contains(MoveType.Move);
            if (pieceAtDestination == null && !containsMove) return false;
            if (pieceAtDestination != null && containsMove) return false;
            return true;

        }
    }
}
