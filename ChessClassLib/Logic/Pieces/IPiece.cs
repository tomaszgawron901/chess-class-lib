using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Pieces
{
    public interface IPiece
    {
        Position Position { get; set; }
        PieceColor Color { get; }
        PieceType Type { get; }
        bool WasMoved { get; set; }
        IEnumerable<PieceMove> MoveSet { get; }
        void MoveToPosition(Position position);
        PieceMove GetMoveTo(Position position);
    }
}
