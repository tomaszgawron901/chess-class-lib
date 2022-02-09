using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule which checks if Piece could be moved by PieceMove with 'Move' MoveType.
    /// </summary>
    class MoveRule: BasePieceRule
    {
        public MoveRule(BasePieceDecorator piece)
            : base(piece)
        {}

        public override PieceMove MoveModifier(PieceMove move)
        {
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);
            if (pieceAtDestination != null || move.MoveTypes.Contains(MoveType.Move))
            {
                if (pieceAtDestination == null)
                {
                    return new PieceMove(move.Shift, MoveType.Move);
                }
                var newMoveTypes = move.MoveTypes.Where(m => m != MoveType.Move).ToArray();
                if (move.MoveTypes.Count() != 0)
                {
                    return new PieceMove(move.Shift, newMoveTypes);
                }
            }
            return null;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);

            var containsMove = move.MoveTypes.Contains(MoveType.Move);
            if (pieceAtDestination == null && !containsMove) return false;
            if (pieceAtDestination != null && containsMove) return false;
            return true;

        }
    }
}
