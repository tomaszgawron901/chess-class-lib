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
        public abstract void SetPiece(Piece piece, Position position);
        public abstract void Clear();
        public abstract IEnumerable<PieceBackup> GetBoardBackup();
        public abstract void RestoreBoard(IEnumerable<PieceBackup> backup);

        public abstract IEnumerator<Piece> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
