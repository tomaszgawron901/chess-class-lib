﻿using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    class KillingPieceOnBoard : BasePieceRule
    {
        protected readonly Board board;
        public KillingPieceOnBoard(BasePieceDecorator piece, Board board)
            : base(piece)
        {
            this.board = board;
        }

        protected override PieceMove MoveModifier(PieceMove move)
        {
            var pieceAtDestination = board.GetPiece(Position + move.Shift);
            if (pieceAtDestination == null || pieceAtDestination.Color == Color || move.MoveTypes.Contains(MoveType.Kill))
            {
                if (pieceAtDestination != null && pieceAtDestination.Color != Color)
                {
                    move.MoveTypes = new MoveType[] { MoveType.Kill };
                }
                else
                {
                    move.MoveTypes = move.MoveTypes.Where(m => m != MoveType.Kill).ToArray();
                }
                return move;
            }
            return null;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            if (!InnerPieceDecorator.ValidateNewMove(move)) return false;
            var pieceAtDestination = board.GetPiece(Position + move.Shift);

            var containsMove = move.MoveTypes.Contains(MoveType.Kill);
            if (pieceAtDestination != null && pieceAtDestination.Color != Color && !containsMove) return false;
            if ((pieceAtDestination == null || pieceAtDestination.Color == Color) && containsMove) return false;
            return true;
        }
    }
}
