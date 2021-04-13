using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    class MoveRule: BasePieceRule
    {
        public MoveRule(BasePieceDecorator piece)
            : base(piece)
        {}

        protected override PieceMove MoveModifier(PieceMove move)
        {
            var pieceAtDestination = Board.GetPiece(Position + move.Shift);
            if (pieceAtDestination != null || move.MoveTypes.Contains(MoveType.Move))
            {
                if (pieceAtDestination == null)
                {
                    move.MoveTypes = new MoveType[] { MoveType.Move };
                }
                else
                {
                    move.MoveTypes = move.MoveTypes.Where(m => m != MoveType.Move).ToArray();
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

            var containsMove = move.MoveTypes.Contains(MoveType.Move);
            if (pieceAtDestination == null && !containsMove) return false;
            if (pieceAtDestination != null && containsMove) return false;
            return true;

        }
    }
}
