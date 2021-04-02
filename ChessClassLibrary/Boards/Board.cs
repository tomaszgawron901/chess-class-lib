using ChessClassLibrary.Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Boards
{
    public abstract class Board : IEnumerable<Piece>
    {
        public abstract bool IsInRange(Position position);
        public abstract Piece GetPiece(Position position);

        public abstract IEnumerator<Piece> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
