﻿using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces
{
    public interface IPiece
    {
        PieceColor Color { get; }
        PieceType Type { get; }
        Position[] MoveSet { get; }
        Position[] KillSet { get; }
        Position? CanKillAchieve(Position position);
        Position? CanMoveAchieve(Position position);
    }

    public abstract class Piece : IPiece
    {
        protected PieceColor color;
        protected PieceType type;
        protected Position position;

        public bool wasMoved;

        /// <summary>
        /// Returns piece color.
        /// </summary>
        public PieceColor Color
        {
            get { return color; }
        }

        /// <summary>
        /// Returns piece Name.
        /// </summary>
        public PieceType Type
        {
            get { return type; }
        }

        /// <summary>
        /// The Position of Piece.
        /// </summary>
        public Position Position
        {
            get { return position; }
            set {
                position = value;
            }
        }

        public abstract Position[] MoveSet { get; }

        public abstract Position[] KillSet  { get; }

        protected Piece(PieceColor color, PieceType type, Position position)
        {
            this.color = color;
            this.type = type;
            this.wasMoved = false;
            this.Position = position;
        }

        /// <summary>
        /// Performs the appropriate actions when the piece is moving for the first time.
        /// </summary>
        protected virtual void firstMove()
        {
            wasMoved = true;
        }

        ///// <summary>
        ///// Checks whether Piece can be moved to given position.
        ///// </summary>
        ///// <param name="position">Movement destination.</param>
        ///// <returns></returns>
        //public virtual bool canMoveTo(Point position)
        //{
        //    if (!this.board.CoordinateIsInRange(position))
        //        return false;
        //    if (pretendMoveAndCheckIfKingIsChecked(position))
        //        return false;
        //    if (CanAchieve(position, MoveSet) && board.GetPiece(position) == null)
        //        return true;
        //    if(CanAchieve(position, KillSet) && board.GetPiece(position) != null)
        //    {
        //        if(board.GetPiece(position).Color != this.Color)
        //            return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Checks if Piece can move to any position.
        ///// </summary>
        //public bool canMoveAnywhere()
        //{
        //    for(int x=0; x < board.Width; x++)
        //    {
        //        for(int y = 0; y < board.Height; y++)
        //        {
        //            if (canMoveTo(new Point(x, y)))
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        //protected bool pretendMoveAndCheckIfKingIsChecked(Point position)
        //{
        //    Piece pieceAtDestinationPosition = board.GetPiece(position);
        //    Point currentPiecePosition = Position;
        //    Position = position;

        //    board.SetPiece(null, currentPiecePosition);
        //    board.SetPiece(this, position);
        //    bool KingIsChecked;
        //    if (color == "White")
        //        KingIsChecked = board.WhiteKing.IsChecked();
        //    else
        //        KingIsChecked = board.BlackKing.IsChecked();
        //    board.SetPiece(pieceAtDestinationPosition, position);
        //    Position = currentPiecePosition;
        //    board.SetPiece(this, currentPiecePosition);
        //    return KingIsChecked;
        //}

        ///// <summary>
        ///// Moves Piece to given position.
        ///// </summary>
        ///// <param name="position">Movement destination.</param>
        //public virtual void moveTo(Point position)
        //{
        //    if (!canMoveTo(position))
        //        throw new ArgumentException("Cannot move to given position.");
        //    if (board.GetPiece(position) == board.WhiteKing)
        //        board.WhiteKing = null;
        //    if (board.GetPiece(position) == board.BlackKing)
        //        board.BlackKing = null;
        //    board.SetPiece(null, Position);
        //    Position = position;
        //    board.SetPiece(this, position);
        //    if (!wasMoved)
        //    {
        //        firstMove();
        //    }
        //}

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public abstract Position? CanMoveAchieve(Position position);
        public abstract Position? CanKillAchieve(Position position);
    }
}
