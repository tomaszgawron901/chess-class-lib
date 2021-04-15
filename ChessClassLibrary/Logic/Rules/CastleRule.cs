﻿using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{

    public class CastleRule: ProtectedPieceRule, IBasePieceDecorator, IPiece
    {
        protected static PieceMove leftCastleMove = new PieceMove(new Position(-2, 0), MoveType.Move);
        protected static PieceMove rightCastleMove = new PieceMove(new Position(2, 0), MoveType.Move);

        public PieceMove LeftCastleMove { get => new PieceMove(leftCastleMove.Shift, leftCastleMove.MoveTypes.Select(x => x).ToArray()); }
        public PieceMove RightCastleMove { get => new PieceMove(rightCastleMove.Shift, rightCastleMove.MoveTypes.Select(x => x).ToArray()); }

        private ProtectedPieceRule protectedPieceRule;
        public override KingState KingState { get => protectedPieceRule.KingState; set => protectedPieceRule.KingState = value; }

        IEnumerable<PieceMove> IPiece.MoveSet
        {
            get
            {
                var newMoveSet = Piece.MoveSet;
                if (CanLeftCastle() && InnerPieceDecorator.ValidateNewMove(leftCastleMove))
                {
                    var existingMove = newMoveSet.FirstOrDefault(x => x.Shift == LeftCastleMove.Shift);
                    if (existingMove == null)
                    {
                        newMoveSet = newMoveSet.Append(leftCastleMove);
                    }
                    else
                    {
                        if (!existingMove.MoveTypes.Contains(MoveType.Move))
                        {
                            existingMove.MoveTypes = existingMove.MoveTypes.Append(MoveType.Move).ToArray();
                        }
                    }
                }
                if (CanRightCastle())
                {
                    var existingMove = newMoveSet.FirstOrDefault(x => x.Shift == RightCastleMove.Shift);
                    if (existingMove == null)
                    {
                        newMoveSet = newMoveSet.Append(rightCastleMove);
                    }
                    else
                    {
                        if (!existingMove.MoveTypes.Contains(MoveType.Move))
                        {
                            existingMove.MoveTypes = existingMove.MoveTypes.Append(MoveType.Move).ToArray();
                        }
                    }
                }
                return newMoveSet;
            }
        }

        public CastleRule(ProtectedPieceRule pieceDecorator)
            : base(pieceDecorator)
        {
            this.protectedPieceRule = pieceDecorator;
        }

        private bool CanRightCastle()
        {
            if (IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(7, Position.y);
            IPiece rightRook = Board.GetPiece(rookPosition);
            if (rightRook != null && rightRook.Type == PieceType.Rook && !rightRook.WasMoved && rightRook.Color == this.Color)
            {
                foreach (var checkedPosition in new Position[] { new Position(5, this.Position.y), new Position(6, this.Position.y) })
                {
                    if (Board.GetPiece(checkedPosition) != null) return false;

                    if (!isProtectedPieceSafeAfterMove(new PieceMove(checkedPosition - Position, MoveType.Move))) return false;
                }
                return true;
            }
            return false;
        }
        private bool CanLeftCastle()
        {
            if (IsChecked) return false;
            if (WasMoved) return false;
            var rookPosition = new Position(0, this.Position.y);
            IPiece leftRook = Board.GetPiece(rookPosition);
            if (leftRook != null && leftRook.Type == PieceType.Rook && !leftRook.WasMoved && leftRook.Color == this.Color && Board.GetPiece(new Position(1, this.Position.y)) == null)
            {
                foreach (var checkedPosition in new Position[] { new Position(2, this.Position.y), new Position(3, this.Position.y) })
                {
                    if (Board.GetPiece(checkedPosition) != null) return false;

                    if (!isProtectedPieceSafeAfterMove(new PieceMove(checkedPosition - Position, MoveType.Move))) return false;
                }
                return true;
            }
            return false;
        }

        private void DoLeftCastle()
        {
            Piece.MoveToPosition(new Position(2, Position.y));
            Board.GetPiece(new Position(0, Position.y)).MoveToPosition(new Position(3, Position.y));
        }

        private void DoRightCastle()
        {
            Piece.MoveToPosition(new Position(6, Position.y));
            Board.GetPiece(new Position(7, Position.y)).MoveToPosition(new Position(5, Position.y));
        }

        public override PieceMove MoveModifier(PieceMove move)
        {
            return move;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;

            if (move.MoveTypes.Contains(MoveType.Move) && (move.Shift == this.LeftCastleMove.Shift || move.Shift == this.RightCastleMove.Shift))
            {
                if (move.Shift == this.LeftCastleMove.Shift)
                {
                    return CanLeftCastle();
                }
                else if (move.Shift == this.RightCastleMove.Shift)
                {
                    return CanRightCastle();
                }
            }
            return true;
        }

        public override void MoveToPosition(Position position)
        {
            var moveShift = position - Position;
            if (moveShift == this.LeftCastleMove.Shift)
            {
                DoLeftCastle();
            }
            else if(moveShift == this.RightCastleMove.Shift)
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
            var baseMove = Piece.GetMoveTo(position);
            if (moveShift == this.LeftCastleMove.Shift)
            {
                if (baseMove == null)
                {
                    if (InnerPieceDecorator.ValidateNewMove(LeftCastleMove) && CanLeftCastle())
                    {
                        return LeftCastleMove;
                    }
                }
                else
                {
                    if (!baseMove.MoveTypes.Contains(MoveType.Move) && CanLeftCastle())
                    {
                        baseMove.MoveTypes = baseMove.MoveTypes.Append(MoveType.Move).ToArray();
                    }
                }
                return baseMove;

            }
            else if (moveShift == this.RightCastleMove.Shift)
            {
                if (baseMove == null)
                {
                    if (InnerPieceDecorator.ValidateNewMove(RightCastleMove) && CanRightCastle())
                    {
                        return RightCastleMove;
                    }
                }
                else
                {
                    if (!baseMove.MoveTypes.Contains(MoveType.Move) && CanRightCastle())
                    {
                        baseMove.MoveTypes = baseMove.MoveTypes.Append(MoveType.Move).ToArray();
                    }
                }
                return baseMove;
            }
            return baseMove;
        }
    }
}
