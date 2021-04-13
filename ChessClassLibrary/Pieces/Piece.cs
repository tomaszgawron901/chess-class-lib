using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces
{
    public interface IPiece
    {
        Position Position { get; set; }
        PieceColor Color { get; set; }
        PieceType Type { get; set; }
        bool WasMoved { get; }
        IEnumerable<PieceMove> MoveSet { get; }
        void MoveToPosition(Position position);
        PieceMove GetMoveTo(Position position);
    }

    public abstract class Piece : IPiece
    {
        public PieceColor Color { get; set; }
        public PieceType Type { get; set; }
        public Position Position { get; set; }
        public bool WasMoved { get; protected set; }

        public abstract IEnumerable<PieceMove> MoveSet { get; }

        protected Piece(PieceColor color, PieceType type, Position position)
        {
            this.Color = color;
            this.Type = type;
            this.WasMoved = false;
            this.Position = position;
        }

        /// <summary>
        /// Performs the appropriate actions when the piece is moving for the first time.
        /// </summary>
        protected virtual void firstMove()
        {
            WasMoved = true;
        }

        public virtual void MoveToPosition(Position position)
        {
            this.Position = position;
        }
        public virtual PieceMove GetMoveTo(Position position) => MoveSet.FirstOrDefault(x => Position + x.Shift == position);
    }
}
