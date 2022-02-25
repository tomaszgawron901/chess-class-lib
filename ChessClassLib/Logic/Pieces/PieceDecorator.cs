using ChessClassLib.Pieces;
using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Logic.PieceRules
{
    public abstract class PieceDecorator : IPiece
    {
        protected abstract IPiece InnerPiece { get; }

        public virtual IEnumerable<PieceMove> MoveSet { get => InnerPiece.MoveSet; }
        public virtual bool WasMoved { get => InnerPiece.WasMoved; set => InnerPiece.WasMoved = value; }
        public virtual Position Position { get => InnerPiece.Position; set => InnerPiece.Position = value; }
        public virtual PieceColor Color { get => InnerPiece.Color; set => InnerPiece.Color = value; }
        public virtual PieceType Type { get => InnerPiece.Type; set => InnerPiece.Type = value; }
        public virtual PieceMove GetMoveTo(Position position) => InnerPiece.GetMoveTo(position);
        public virtual void MoveToPosition(Position position) => InnerPiece.MoveToPosition(position);
    }
}
