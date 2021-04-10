using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic
{
    public abstract class BasePieceDecorator : IPiece
    {
        public BasePieceDecorator(){}

        public abstract IPiece Piece { get; }
        public abstract Board Board { get; }

        public abstract IEnumerable<PieceMove> MoveSet { get; }

        public Position Position { get => Piece.Position; }

        public PieceColor Color => Piece.Color;

        public PieceType Type => Piece.Type;

        public bool WasMoved => Piece.WasMoved;
        protected abstract PieceMove MoveModifier(PieceMove move);

        public abstract bool ValidateNewMove(PieceMove move);

        public virtual PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (baseMove == null) return null;

            return MoveModifier(baseMove);
        }

        public virtual void MoveToPosition(Position position)
        {
            Piece.MoveToPosition(position);
        }
    }
}
