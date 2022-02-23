using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;

namespace ChessClassLibrary.Boards
{
    public interface IBoard : IEnumerable<IPiece>
    {
        bool IsInRange(Position position);
        IPiece GetPiece(Position position);
        void SetPiece(IPiece piece, Position position);
        void SetPiece(IPiece piece);
        void Clear();
    }
}
