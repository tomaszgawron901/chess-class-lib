using ChessClassLibrary.enums;
using ChessClassLibrary.Extensions;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule that allows moving Piece two fields straight forward if and only if it was not yet moved.
    /// </summary>
    public class WhitePawnFirstMoveRule: BasePieceRule, IBasePieceDecorator, IPiece
    {
        protected readonly static PieceMove longMove = new PieceMove(new Position(0, 2), MoveType.Move);

        public WhitePawnFirstMoveRule(BasePieceDecorator pieceDecorator)
            : base(pieceDecorator)
        { }

        public override PieceMove MoveModifier(PieceMove move)
        {
            return move;
        }

        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                var newMoveSet = Piece.MoveSet;
                if (InnerPieceDecorator.ValidateNewMove(longMove) && CanLongMove())
                {
                    return Piece.MoveSet.AddOrUpdatePieceMove(longMove);
                }
                return newMoveSet;
            }
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;

            if (move.MoveTypes.Contains(MoveType.Move) && move.Shift == longMove.Shift)
            {
                return CanLongMove();
            }
            return true;
        }


        /// <summary>
        /// Checks if Piece can move two fields straight forward.
        /// </summary>
        /// <returns></returns>
        private bool CanLongMove()
        {
            return !this.WasMoved && Board.GetPiece(Position + new Position(0, 1)) == null && Board.GetPiece(Position + new Position(0, 2)) == null;
        }

        public override PieceMove GetMoveTo(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == longMove.Shift && InnerPieceDecorator.ValidateNewMove(longMove) && CanLongMove())
            {
                return longMove;
            }
            return Piece.GetMoveTo(position);
        }
    }
}
