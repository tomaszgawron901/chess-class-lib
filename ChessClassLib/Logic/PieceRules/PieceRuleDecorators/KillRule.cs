using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    public static partial class IPieceRuleExtensions
    {
        public static KillRule AddKillRule(this IPieceRule innerPieceRule)
        {
            return new KillRule(innerPieceRule);
        }
    }

    /// <summary>
    /// Rule which checks if Piece could be moved by PieceMove with 'Kill' MoveType.
    /// </summary>
    public class KillRule: PieceRuleDecorator
    {
        public KillRule(IPieceRule innerPieceRule)
            : base(innerPieceRule)
        {}

        public override PieceMove ConstrainMove(PieceMove move)
        {
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);
            if (pieceAtDestination == null || pieceAtDestination.Color == Color || move.MoveTypes.Contains(MoveType.Kill))
            {
                if (pieceAtDestination != null && pieceAtDestination.Color != Color)
                {
                    return new PieceMove(move.Shift, MoveType.Kill);
                }
                var newMoveTypes = move.MoveTypes.Where(m => m != MoveType.Kill).ToArray();
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

            var containsMove = move.MoveTypes.Contains(MoveType.Kill);
            if (pieceAtDestination != null && pieceAtDestination.Color != Color && !containsMove) return false;
            if ((pieceAtDestination == null || pieceAtDestination.Color == Color) && containsMove) return false;
            return true;
        }
    }
}
