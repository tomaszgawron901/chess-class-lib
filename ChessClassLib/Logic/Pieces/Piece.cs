using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Pieces
{
    public abstract class Piece : IPiece
    {
        public PieceColor Color { get; }
        public PieceType Type { get; }
        public Position Position { get; set; }
        public bool WasMoved { get; set; }

        public abstract IEnumerable<PieceMove> MoveSet { get; }

        public Piece(PieceColor color, PieceType type, Position position)
        {
            Color = color;
            Type = type;
            WasMoved = false;
            Position = position;
        }

        protected void firstMove()
        {
            WasMoved = true;
        }

        public void MoveToPosition(Position position)
        {
            WasMoved = true;
            Position = position;
        }

        public PieceMove GetMoveTo(Position position) => 
            MoveSet.FirstOrDefault(x => Position + x.Shift == position);
    }
}
