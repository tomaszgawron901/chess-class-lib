using ChessClassLibrary.Enums;
using ChessClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLib.Pieces
{
    public interface IPiece
    {
        Position Position { get; set; }
        PieceColor Color { get; set; }
        PieceType Type { get; set; }
        bool WasMoved { get; set; }
        IEnumerable<PieceMove> MoveSet { get; }
        void MoveToPosition(Position position);
        PieceMove GetMoveTo(Position position);
    }
}
