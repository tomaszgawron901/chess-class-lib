﻿using ChessClassLibrary.enums;
using ChessClassLibrary.Extensions;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Rules
{
    /// <summary>
    /// Rule that allows moving Piece two fields straight backward if and only if it was not yet moved.
    /// </summary>
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

        public override IEnumerable<PieceMove> MoveSet {
            get
            {
                if (InnerPieceDecorator.ValidateNewMove(LongMove) && CanLongMove())
                {
                    return Piece.MoveSet.AddOrUpdatePieceMove(LongMove);
                }
                return Piece.MoveSet;
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

        /// <summary>
        /// Checks if Piece can move two fields straight backward.
        /// </summary>
        /// <returns></returns>
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
