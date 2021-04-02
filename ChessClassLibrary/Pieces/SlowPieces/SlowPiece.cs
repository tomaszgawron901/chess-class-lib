using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public abstract class SlowPiece : Piece
    {
        protected SlowPiece(PieceColor color, PieceType type, Point position) :
            base(color, type, position)
        { }

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public override bool CanAchieve(Point position)
        {
            foreach (Point move in this.MoveSet)
            {
                Point fieldToCheck = this.position + move;
                if (position == fieldToCheck)
                    return true;
            }
            return false;
        }
    }
}
