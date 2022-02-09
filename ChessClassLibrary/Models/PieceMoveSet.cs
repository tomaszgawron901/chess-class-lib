using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Models
{
    internal class PieceMoveSet : IEnumerable<PieceMove>
    {
        private readonly IEnumerable<PieceMove> _moves;

        public PieceMoveSet(params PieceMove[] moves)
        {
            this._moves = moves;
        }

        public PieceMove GetPieceMoveByShift(Position shift) => _moves.FirstOrDefault(x => x.Shift == shift);

        public IEnumerator<PieceMove> GetEnumerator()
        {
            return _moves.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
