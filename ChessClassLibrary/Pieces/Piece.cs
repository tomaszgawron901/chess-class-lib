using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces
{
    public interface IPiece
    {
        Position Position { get;}
        PieceColor Color { get; }
        PieceType Type { get; }
        bool WasMoved { get; }
        IEnumerable<PieceMove> MoveSet { get; }
        void MoveToPosition(Position position);
        bool CanMoveAnywhere();
        bool IsMoveValid(PieceMove move);
    }

    public abstract class Piece : IPiece
    {
        public PieceColor Color { get; protected set; }
        public PieceType Type { get; protected set; }
        public Position Position { get; protected set; }
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
        public bool CanMoveAnywhere()
        {
            return this.MoveSet.Any();
        }

        public virtual bool IsMoveValid(PieceMove move) => true;
    }
}
