using ChessClassLib.Extensions;
using ChessClassLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    public abstract class NewMoveRule : PieceRuleDecorator
    {
        protected abstract PieceMove NewMove { get; }

        protected NewMoveRule(IPieceRule innerPieceRule) : base(innerPieceRule)
        { }

        public override PieceMove ConstrainMove(PieceMove move)
        {
            return move;
        }

        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                var newMoveSet = InnerPiece.MoveSet;
                if (InnerPieceRule.ValidateMove(NewMove) && CanPerformNewMove())
                {
                    return InnerPiece.MoveSet.AddOrUpdatePieceMove(NewMove);
                }
                return newMoveSet;
            }
        }

        public override bool ValidateMove(PieceMove move)
        {
            if (!InnerPieceRule.ValidateMove(move)) return false;

            if ( NewMove.MoveTypes.Any(mt => move.HasType(mt) && move.Shift == NewMove.Shift))
            {
                return CanPerformNewMove();
            }
            return true;
        }

        /// <summary>
        /// Checks if Piece can move two fields straight forward.
        /// </summary>
        /// <returns></returns>
        protected abstract bool CanPerformNewMove();

        public override PieceMove GetMoveTo(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == NewMove.Shift && InnerPieceRule.ValidateMove(NewMove) && CanPerformNewMove())
            {
                return NewMove;
            }
            return InnerPiece.GetMoveTo(position);
        }
    }
}
