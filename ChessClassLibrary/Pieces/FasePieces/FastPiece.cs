using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public abstract class FastPiece : Piece
    {
        protected FastPiece(PieceColor color, PieceType type, Point position) :
            base(color, type, position)
        { }

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public override Point? CanMoveAchieve(Point position)
        {
            if (this.Position == position)
                return null;
            foreach (Point move in moveSet)
            {
                if (move == new Point(0, 0))
                {
                    continue;
                }
                if (isInLine(position, move))
                {
                    return move;
                }
            }
            return null;
        }

        public override Point? CanKillAchieve(Point position)
        {
            if (this.Position == position)
                return null;
            foreach (Point move in killSet)
            {
                if (move == new Point(0, 0))
                {
                    continue;
                }
                if (isInLine(position, move))
                {
                    return move;
                }
            }
            return null;
        }


        private bool isInLine(Point destination, Point move)
        {
            Point destinationMove = destination - this.position;
            if (Math.Sign(destinationMove.X) != Math.Sign(move.X))
                return false;
            if (Math.Sign(destinationMove.Y) != Math.Sign(move.Y))
                return false;
            if (move == new Point(0, 0))
            {
                if (move == destinationMove)
                    return true;
                else return false;
            }

            if (move.X == 0)
            {
                if (destinationMove.X != 0)
                    return false;
                if (destinationMove.Y % move.Y != 0)
                    return false;
            }
            else if (move.Y == 0)
            {
                if (destinationMove.Y != 0)
                    return false;
                if (destinationMove.X % move.X != 0)
                    return false;
            }
            else
            {
                if (destinationMove.X == 0 || destinationMove.Y == 0)
                    return false;
                if (destinationMove.X % move.X != 0 || destinationMove.Y % move.Y != 0)
                    return false;
                if (destinationMove.X / move.X != destinationMove.Y / move.Y)
                    return false;

            }
            return true;
        }
    }
}
