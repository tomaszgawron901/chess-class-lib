﻿using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public abstract class SlowPiece : Piece
    {
        protected SlowPiece(PieceColor color, PieceType type, Position position) :
            base(color, type, position)
        { }

        /// <summary>
        /// Checks whether given position can by achieved by 'move' movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <returns>First move that can achieve given position or null if cannot achieve given position.</returns>
        public override Position? CanMoveAchieve(Position position)
        {
            foreach (Position move in moveSet)
            {
                Position fieldToCheck = this.position + move;
                if (position == fieldToCheck)
                    return move;
            }
            return null;
        }

        /// <summary>
        /// Checks whether given position can by achieved by 'kill' movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <returns> First move that can achieve given position or null if cannot achieve given position.</returns>
        public override Position? CanKillAchieve(Position position)
        {
            foreach (Position move in killSet)
            {
                Position fieldToCheck = this.position + move;
                if (position == fieldToCheck)
                    return move;
            }
            return null;
        }
    }
}
