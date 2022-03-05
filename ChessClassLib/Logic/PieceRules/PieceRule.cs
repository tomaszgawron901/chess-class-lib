using ChessClassLib.Enums;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Models;
using ChessClassLib.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules
{
    public abstract class PieceRule : IPieceRule
    {
        public abstract IBoard Board { get; }
        protected abstract IPiece InnerPiece { get; }
        public virtual bool WasMoved { get => InnerPiece.WasMoved; set => InnerPiece.WasMoved = value; }
        public virtual Position Position { get => InnerPiece.Position; set => InnerPiece.Position = value; }
        public virtual PieceColor Color { get => InnerPiece.Color; }
        public virtual PieceType Type { get => InnerPiece.Type; }
        public virtual void MoveToPosition(Position position) => InnerPiece.MoveToPosition(position);
        public virtual IEnumerable<PieceMove> MoveSet => InnerPiece.MoveSet.Select(ConstrainMove).Where(x => x != null);

        public virtual PieceMove ConstrainMove(PieceMove move) => move;

        public virtual bool ValidateMove(PieceMove move) => true;

        public virtual PieceMove GetMoveTo(Position position)
        {
            var baseMove = InnerPiece.GetMoveTo(position);
            if (baseMove == null) return null;

            return ConstrainMove(baseMove);
        }
    }
}
