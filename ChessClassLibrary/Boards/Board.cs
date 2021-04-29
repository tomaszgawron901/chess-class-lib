using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections;
using System.Collections.Generic;

namespace ChessClassLibrary.Boards
{
    public abstract class Board : IEnumerable<IPiece>
    {
        public abstract bool IsInRange(Position position);
        public abstract IPiece GetPiece(Position position);
        public abstract void SetPiece(IPiece piece, Position position);
        public abstract void SetPiece(IPiece piece);
        public abstract void Clear();

        public abstract IEnumerator<IPiece> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
