using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic
{
    public interface IBasePieceDecorator: IPiece
    {
        IPiece Piece { get; }
        PieceMove MoveModifier(PieceMove move);
        bool ValidateNewMove(PieceMove move);
    }

    /// <summary>
    /// Base class container for Piece to be decorated with new behavior.
    /// </summary>
    public abstract class BasePieceDecorator : IBasePieceDecorator
    {
        public BasePieceDecorator(IPiece Piece)
        {
            this.Piece = Piece;
            this.Color = Piece.Color;
            this.Type = Piece.Type;
        }

        /// <summary>
        /// Base Piece witch is being decorated.
        /// </summary>
        public IPiece Piece { get; }

        /// <summary>
        /// Available PieceMoves.
        /// </summary>
        public virtual IEnumerable<PieceMove> MoveSet { get => Piece.MoveSet.Select(MoveModifier).Where(x => x.MoveTypes.Any()); }

        public PieceColor Color { get; }
        public PieceType Type { get; }

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
        public virtual PieceMove GetShiftMove(Position shift) => MoveSet.FirstOrDefault(x => x.Shift == shift);
    }
}
