using ChessClassLibrary.enums;
using System;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public abstract class FastPiece : Piece
    {
        protected FastPiece(PieceColor color, PieceType type, Position position) :
            base(color, type, position)
        { }

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public override Position? CanMoveAchieve(Position position)
        {
            if (this.Position == position)
                return null;
            foreach (Position move in MoveSet)
            {
                if (move == new Position(0, 0))
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

        public override Position? CanKillAchieve(Position position)
        {
            if (this.Position == position)
                return null;
            foreach (Position move in KillSet)
            {
                if (move == new Position(0, 0))
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


        private bool isInLine(Position destination, Position move)
        {
            Position destinationMove = destination - this.position;
            if (Math.Sign(destinationMove.x) != Math.Sign(move.x))
                return false;
            if (Math.Sign(destinationMove.y) != Math.Sign(move.y))
                return false;
            if (move == new Position(0, 0))
            {
                if (move == destinationMove)
                    return true;
                else return false;
            }

            if (move.x == 0)
            {
                if (destinationMove.x != 0)
                    return false;
                if (destinationMove.y % move.y != 0)
                    return false;
            }
            else if (move.y == 0)
            {
                if (destinationMove.y != 0)
                    return false;
                if (destinationMove.x % move.x != 0)
                    return false;
            }
            else
            {
                if (destinationMove.x == 0 || destinationMove.y == 0)
                    return false;
                if (destinationMove.x % move.x != 0 || destinationMove.y % move.y != 0)
                    return false;
                if (destinationMove.x / move.x != destinationMove.y / move.y)
                    return false;

            }
            return true;
        }
    }
}
