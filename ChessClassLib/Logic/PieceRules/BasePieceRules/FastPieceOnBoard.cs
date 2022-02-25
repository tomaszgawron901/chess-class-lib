using ChessClassLib.Pieces;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Models;
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
                    for (Shift nextShift = move.Shift; true; nextShift += move.Shift)
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
            Shift destinationShift = destination - this.Position;
            if (Math.Sign(destinationShift.X) != Math.Sign(move.Shift.X))
                return false;
            if (Math.Sign(destinationShift.Y) != Math.Sign(move.Shift.Y))
                return false;
            if (move.Shift == new Shift(0, 0))
            {
                if (move.Shift == destinationShift)
                    return true;
                else return false;
            }

            if (move.Shift.X == 0)
            {
                if (destinationShift.X != 0)
                    return false;
                if (destinationShift.Y % move.Shift.Y != 0)
                    return false;
            }
            else if (move.Shift.Y == 0)
            {
                if (destinationShift.Y != 0)
                    return false;
                if (destinationShift.X % move.Shift.X != 0)
                    return false;
            }
            else
            {
                if (destinationShift.X == 0 || destinationShift.Y == 0)
                    return false;
                if (destinationShift.X % move.Shift.X != 0 || destinationShift.Y % move.Shift.Y != 0)
                    return false;
                if (destinationShift.X / move.Shift.X != destinationShift.Y / move.Shift.Y)
                    return false;

            }
            return true;
        }
    }
}
