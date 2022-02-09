using ChessClassLibrary.enums;
using ChessClassLibrary.Extensions;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule for handling castle move.
    /// </summary>
    public class CastleRule: ProtectedPieceRule, IBasePieceDecorator, IPiece
    {
        public override IPiece AtackedPiece { 
            get {
                if(this.protectedPieceRule != null)
                {
                    return this.protectedPieceRule.AtackedPiece;
                }
                return null;
            }
            set {
                if (this.protectedPieceRule !=  null)
                {
                    this.protectedPieceRule.AtackedPiece = value;
                }
            }
        }

        protected readonly static PieceMove leftCastleMove = new PieceMove(new Position(-2, 0), MoveType.Move);
        protected readonly static PieceMove rightCastleMove = new PieceMove(new Position(2, 0), MoveType.Move);

        private ProtectedPieceRule protectedPieceRule;
        public override KingState KingState { get => protectedPieceRule.KingState; set => protectedPieceRule.KingState = value; }

        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                var newMoveSet = Piece.MoveSet;
                if (InnerPieceDecorator.ValidateNewMove(leftCastleMove) && CanLeftCastle())
                {
                    newMoveSet = newMoveSet.AddOrUpdatePieceMove(leftCastleMove);
                }
                if (InnerPieceDecorator.ValidateNewMove(rightCastleMove) && CanRightCastle())
                {
                    newMoveSet = newMoveSet.AddOrUpdatePieceMove(rightCastleMove);
                }
                return newMoveSet;
            }
        }

        public CastleRule(ProtectedPieceRule pieceDecorator)
            : base(pieceDecorator)
        {
            this.protectedPieceRule = pieceDecorator;
        }

        /// <summary>
        /// Checks if left castle can be performed.
        /// </summary>
        /// <returns></returns>
        private bool CanRightCastle()
        {
            if (IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(7, Position.Y);
            IPiece rightRook = Board.GetPiece(rookPosition);
            if (rightRook != null && rightRook.Type == PieceType.Rook && !rightRook.WasMoved && rightRook.Color == this.Color)
            {
                foreach (var checkedPosition in new Position[] { new Position(5, this.Position.Y), new Position(6, this.Position.Y) })
                {
                    if (Board.GetPiece(checkedPosition) != null) return false;

                    if (!isProtectedPieceSafeAfterMove(new PieceMove(checkedPosition - Position, MoveType.Move))) return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if right castle can be performed.
        /// </summary>
        /// <returns></returns>
        private bool CanLeftCastle()
        {
            if (IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(0, this.Position.Y);
            IPiece leftRook = Board.GetPiece(rookPosition);
            if (leftRook != null && leftRook.Type == PieceType.Rook && !leftRook.WasMoved && leftRook.Color == this.Color && Board.GetPiece(new Position(1, this.Position.Y)) == null)
            {
                foreach (var checkedPosition in new Position[] { new Position(2, this.Position.Y), new Position(3, this.Position.Y) })
                {
                    if (Board.GetPiece(checkedPosition) != null) return false;

                    if (!isProtectedPieceSafeAfterMove(new PieceMove(checkedPosition - Position, MoveType.Move))) return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performs left castle.
        /// </summary>
        private void DoLeftCastle()
        {
            Piece.MoveToPosition(new Position(2, Position.Y));
            Board.GetPiece(new Position(0, Position.Y)).MoveToPosition(new Position(3, Position.Y));
        }

        /// <summary>
        /// Performs right castle.
        /// </summary>
        private void DoRightCastle()
        {
            Piece.MoveToPosition(new Position(6, Position.Y));
            Board.GetPiece(new Position(7, Position.Y)).MoveToPosition(new Position(5, Position.Y));
        }

        public override PieceMove MoveModifier(PieceMove move)
        {
            return move;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;

            if (move.MoveTypes.Contains(MoveType.Move) && (move.Shift == leftCastleMove.Shift || move.Shift == rightCastleMove.Shift))
            {
                if (move.Shift == leftCastleMove.Shift)
                {
                    return CanLeftCastle();
                }
                else if (move.Shift == rightCastleMove.Shift)
                {
                    return CanRightCastle();
                }
            }
            return true;
        }

        public override void MoveToPosition(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == leftCastleMove.Shift)
            {
                DoLeftCastle();
            }
            else if(moveShift == rightCastleMove.Shift)
            {
                DoRightCastle();
            }
            else
            {
                Piece.MoveToPosition(position);
            }
        }

        public override PieceMove GetMoveTo(Position position)
        {
            var moveShift = position - Position;

            if (moveShift == leftCastleMove.Shift)
            {
                if (InnerPieceDecorator.ValidateNewMove(leftCastleMove) && CanLeftCastle())
                {
                    return leftCastleMove;
                }
                return null;
            }

            if (moveShift == rightCastleMove.Shift)
            {
                if (InnerPieceDecorator.ValidateNewMove(rightCastleMove) && CanRightCastle())
                {
                    return rightCastleMove;
                }
                return null;
            }

            return Piece.GetMoveTo(position);
        }
    }
}
