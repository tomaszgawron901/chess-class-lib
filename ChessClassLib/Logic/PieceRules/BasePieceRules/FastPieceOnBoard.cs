using ChessClassLib.Pieces;
using ChessClassLibrary.Logic.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.BasePieceRules
{
    public static partial class IPieceExtensions
    {
        public static FastPieceOnBoard AddFastPieceOnBoard(this IPiece innerPiece, IBoard board)
        {
            return new FastPieceOnBoard(innerPiece, board);
        }
    }

    /// <summary>
    /// Container class for fast pieces.
    /// </summary>
    public class FastPieceOnBoard: PieceOnBoard
    {
        public FastPieceOnBoard(IPiece innerPiece, IBoard board)
            : base(innerPiece, board)
        {}

        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                var newMoveSet = new List<PieceMove>();
                foreach (PieceMove move in InnerPiece.MoveSet)
                {
                    for (Position nextShift = move.Shift; true; nextShift += move.Shift)
                    {
                        var checkingPosition = Position + nextShift;
                        if (!Board.IsInRange(checkingPosition)) break;

                        newMoveSet.Add(new PieceMove(nextShift, move.MoveTypes.ToArray()));

                        if (Board.GetPiece(checkingPosition) != null) break;
                    }
                }
                return newMoveSet;
            }
        }

        public override PieceMove GetMoveTo(Position position)
        {
            if (!Board.IsInRange(position)) return null;

            var slowMove = InnerPiece.MoveSet.FirstOrDefault(move => isInLine(position, move));
            if (slowMove == null || !IsPathClear(position, slowMove)) return null;

            return new PieceMove(position - Position, slowMove.MoveTypes.ToArray());
        }


        /// <summary>
        /// Check if there is no Piece between current Piece Position and PieceMove destination.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool IsPathClear(Position destination, PieceMove move)
        {
            for (
                Position checkedPosition = Position + move.Shift;
                checkedPosition != destination;
                checkedPosition += move.Shift)
            {
                if (Board.GetPiece(checkedPosition) != null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if destination is in reach of multiplicity of given PieceMove.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool isInLine(Position destination, PieceMove move)
        {
            Position destinationMove = destination - this.Position;
            if (Math.Sign(destinationMove.X) != Math.Sign(move.Shift.X))
                return false;
            if (Math.Sign(destinationMove.Y) != Math.Sign(move.Shift.Y))
                return false;
            if (move.Shift == new Position(0, 0))
            {
                if (move.Shift == destinationMove)
                    return true;
                else return false;
            }

            if (move.Shift.X == 0)
            {
                if (destinationMove.X != 0)
                    return false;
                if (destinationMove.Y % move.Shift.Y != 0)
                    return false;
            }
            else if (move.Shift.Y == 0)
            {
                if (destinationMove.Y != 0)
                    return false;
                if (destinationMove.X % move.Shift.X != 0)
                    return false;
            }
            else
            {
                if (destinationMove.X == 0 || destinationMove.Y == 0)
                    return false;
                if (destinationMove.X % move.Shift.X != 0 || destinationMove.Y % move.Shift.Y != 0)
                    return false;
                if (destinationMove.X / move.Shift.X != destinationMove.Y / move.Shift.Y)
                    return false;

            }
            return true;
        }
    }
}
