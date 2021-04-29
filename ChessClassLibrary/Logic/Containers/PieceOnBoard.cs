using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Containers
{
    public class PieceOnBoard : BasePieceContainer
    {
        public PieceOnBoard(IPiece piece, Board board)
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
