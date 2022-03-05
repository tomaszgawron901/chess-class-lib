using ChessClassLib.Pieces;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace ChessClassLib.Logic.Boards
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
