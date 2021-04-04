using ChessClassLibrary.Pieces;
using System.Collections;
using System.Collections.Generic;

namespace ChessClassLibrary.Boards
{
    public abstract class Board : IEnumerable<Piece>
    {
        public abstract bool IsInRange(Position position);
        public abstract Piece GetPiece(Position position);
        public abstract void SetPiece(Piece piece, Position position);
        public abstract void Clear();

        public abstract IEnumerator<Piece> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
