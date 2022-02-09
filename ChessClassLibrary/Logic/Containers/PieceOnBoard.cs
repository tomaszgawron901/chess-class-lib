using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Containers
{
    /// <summary>
    /// Container class for slow pieces.
    /// </summary>
    public class PieceOnBoard: BasePieceDecorator
    {
        public IBoard board { get; protected set; }

        public PieceOnBoard(IPiece piece, IBoard board)
            :base(piece)
        {
            this.board = board;
        }

        public override PieceMove GetShiftMove(Position shift)
        {
            var baseMove = Piece.GetShiftMove(shift);
            if (baseMove == null) return null;

            return MoveModifier(baseMove);
        }

        public override PieceMove MoveModifier(PieceMove move)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            throw new System.NotImplementedException();
        }
    }
}
