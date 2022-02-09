using ChessClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Extensions
{
    public static class PieceMoveSetExtensions
    {
        public static PieceMove GetPieceMoveByShift(this IEnumerable<PieceMove> moveSet, Position shift)
        {
            return moveSet.FirstOrDefault(move => move.Shift == shift);
        }

        public static IEnumerable<PieceMove> AddOrUpdatePieceMove(this IEnumerable<PieceMove> moveSet, PieceMove move)
        {
            foreach (var m in moveSet)
            {
                if(m.Shift == move.Shift)
                {
                    return moveSet.SwapFirst(m, move);
                }
            }
            return moveSet.Add(move);
        }
    }
}
