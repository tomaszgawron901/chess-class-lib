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

        public bool WasMoved => Piece.WasMoved;

        public Position Position { get => Piece.Position; set => Piece.Position = value; }
        public PieceColor Color { get => Piece.Color; set => Piece.Color = value; }
        public PieceType Type { get => Piece.Type; set => Piece.Type = value; }

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
