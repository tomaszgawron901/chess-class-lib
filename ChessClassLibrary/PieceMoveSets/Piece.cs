using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces
{
    public interface IPiece
    {
        PieceColor Color { get; }
        PieceType Type { get; }
        IEnumerable<PieceMove> MoveSet { get; }
        PieceMove GetShiftMove(Position shift);
    }

    public abstract class Piece : IPiece
    {
        public PieceColor Color { get; set; }
        public PieceType Type { get; set; }

        /// <summary>
        /// Piece available moves.
        /// </summary>
        public abstract IEnumerable<PieceMove> MoveSet { get; }

        protected Piece(PieceColor color, PieceType type)
        {
            this.Color = color;
            this.Type = type;
        }

        /// <summary>
        /// Gets shift move.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual PieceMove GetShiftMove(Position shift) => MoveSet.FirstOrDefault(x => x.Shift == shift);
    }
}
