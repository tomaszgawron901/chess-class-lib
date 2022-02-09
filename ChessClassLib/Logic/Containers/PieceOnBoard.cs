using ChessClassLibrary.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Containers
{
    /// <summary>
    /// Container class for slow pieces.
    /// </summary>
    public class PieceOnBoard : BasePieceContainer
    {
        public PieceOnBoard(IPiece piece, IBoard board)
            : base(piece, board)
        {}

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Select(MoveModifier).Where(x => x != null);

        public override PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (baseMove == null) return null;

            return MoveModifier(baseMove);
        }
    }
}
