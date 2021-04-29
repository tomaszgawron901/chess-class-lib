using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    class BlackPawnFirstMoveRule : BasePieceRule, IBasePieceDecorator, IPiece
    {
        private static PieceMove longMove = new PieceMove(new Position(0, -2), MoveType.Move);

        public BlackPawnFirstMoveRule(BasePieceDecorator pieceDecorator)
            : base(pieceDecorator)
        { }

        public override PieceMove MoveModifier(PieceMove move)
        {
            return move;
        }

        protected PieceMove LongMove => new PieceMove(longMove.Shift, longMove.MoveTypes.Select(x => x).ToArray());

        IEnumerable<PieceMove> IPiece.MoveSet {
            get
            {
                var newMoveSet = Piece.MoveSet;
                if (InnerPieceDecorator.ValidateNewMove(LongMove) && CanLongMove())
                {
                    var existingMove = newMoveSet.FirstOrDefault(x => x.Shift == LongMove.Shift);
                    if (existingMove == null)
                    {
                        newMoveSet = newMoveSet.Append(LongMove);
                    }
                    else
                    {
                        existingMove.MoveTypes = new MoveType[] { MoveType.Move };
                    }
                }
                return newMoveSet;
            }
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;

            if (move.MoveTypes.Contains(MoveType.Move) && move.Shift == LongMove.Shift)
            {
                return CanLongMove();
            }
            return true;
        }

        private bool CanLongMove()
        {
            return !this.WasMoved && Board.GetPiece(Position + new Position(0, -1)) == null && Board.GetPiece(Position + new Position(0, -2)) == null;
        }

        public override PieceMove GetMoveTo(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == this.LongMove.Shift && InnerPieceDecorator.ValidateNewMove(LongMove) && CanLongMove())
            {
                return longMove;
            }
            return Piece.GetMoveTo(position);
        }
    }
}
