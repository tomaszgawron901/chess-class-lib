using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule which checks if Piece could be moved by PieceMove with 'Kill' MoveType.
    /// </summary>
    class KillRule : BasePieceRule
    {
        public KillRule(BasePieceDecorator piece)
            : base(piece)
        {}

        public override PieceMove MoveModifier(PieceMove move)
        {
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);
            if (pieceAtDestination == null || pieceAtDestination.Color == Color || move.MoveTypes.Contains(MoveType.Kill))
            {
                if (pieceAtDestination != null && pieceAtDestination.Color != Color)
                {
                    move.MoveTypes = new MoveType[] { MoveType.Kill };
                }
                else
                {
                    move.MoveTypes = move.MoveTypes.Where(m => m != MoveType.Kill).ToArray();
                    if (move.MoveTypes.Length == 0)
                    {
                        return null;
                    }
                }
                return move;
            }
            return null;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);

            var containsMove = move.MoveTypes.Contains(MoveType.Kill);
            if (pieceAtDestination != null && pieceAtDestination.Color != Color && !containsMove) return false;
            if ((pieceAtDestination == null || pieceAtDestination.Color == Color) && containsMove) return false;
            return true;
        }
    }
}
