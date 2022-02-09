using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;

namespace ChessClassLibrary.Logic
{
    public interface IBasePieceDecorator: IPiece
    {
        PieceMove MoveModifier(PieceMove move);
        bool ValidateNewMove(PieceMove move);
        IBoard Board { get; }
    }

    /// <summary>
    /// Base class container for Piece to be decorated with new behavior.
    /// </summary>
    public abstract class BasePieceDecorator : IBasePieceDecorator
    {
        public BasePieceDecorator(){}

        /// <summary>
        /// Base Piece witch is being decorated.
        /// </summary>
        public abstract IPiece Piece { get; }

        /// <summary>
        /// Board on witch Piece is located.
        /// </summary>
        public abstract IBoard Board { get; }

        /// <summary>
        /// Available PieceMoves.
        /// </summary>
        public abstract IEnumerable<PieceMove> MoveSet { get; }

        /// <summary>
        /// Determines if piece was moved.
        /// </summary>
        public bool WasMoved => Piece.WasMoved;

        public Position Position { get => Piece.Position; set => Piece.Position = value; }
        public PieceColor Color { get => Piece.Color; set => Piece.Color = value; }
        public PieceType Type { get => Piece.Type; set => Piece.Type = value; }

        /// <summary>
        /// Applies new behaviors and constrains to the PieceMove.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public abstract PieceMove MoveModifier(PieceMove move);

        /// <summary>
        /// Checks if new PieceMove is valid.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public abstract bool ValidateNewMove(PieceMove move);

        /// <summary>
        /// Gets available PieceMove to given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract PieceMove GetMoveTo(Position position);

        /// <summary>
        /// Moves piece to given position.
        /// </summary>
        /// <param name="position"></param>
        public abstract void MoveToPosition(Position position);
    }
}
