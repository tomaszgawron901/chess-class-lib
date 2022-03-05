using ChessClassLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Extensions
{
    public static class PieceMoveSetExtensions
    {
        public static PieceMove GetPieceMoveByShift(this IEnumerable<PieceMove> moveSet, Shift shift)
        {
            return moveSet.FirstOrDefault(move => move.Shift == shift);
        }

        public static IEnumerable<PieceMove> AddOrUpdatePieceMove(this IEnumerable<PieceMove> moveSet, PieceMove move)
        {
            using (var enumerator = moveSet.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Shift == move.Shift)
                    {
                        yield return move;
                        while (enumerator.MoveNext())
                        {
                            yield return enumerator.Current;
                        }
                        yield break;
                    }
                    yield return enumerator.Current;
                }
                yield return move;
            }
        }
    }
}
