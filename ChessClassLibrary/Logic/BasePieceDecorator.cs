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
    public interface IBasePieceDecorator: IPiece
    {
        PieceMove MoveModifier(PieceMove move);
        bool ValidateNewMove(PieceMove move);
        Board Board { get; }
    }

    public abstract class BasePieceDecorator : IBasePieceDecorator
    {
        public BasePieceDecorator(){}

        public abstract IPiece Piece { get; }
        public abstract Board Board { get; }

        public abstract IEnumerable<PieceMove> MoveSet { get; }

        public bool WasMoved => Piece.WasMoved;

        public Position Position { get => Piece.Position; set => Piece.Position = value; }
        public PieceColor Color { get => Piece.Color; set => Piece.Color = value; }
        public PieceType Type { get => Piece.Type; set => Piece.Type = value; }

        public abstract PieceMove MoveModifier(PieceMove move);

        public abstract bool ValidateNewMove(PieceMove move);
        public abstract PieceMove GetMoveTo(Position position);
        public abstract void MoveToPosition(Position position);
    }
}
