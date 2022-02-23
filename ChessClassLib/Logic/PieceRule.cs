using ChessClassLibrary.Boards;
using ChessClassLibrary.Logic;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic
{
    public interface IPieceRule: IPiece
    {
        IBoard Board { get; }
        PieceMove ConstrainMove(PieceMove move);
        bool ValidateMove(PieceMove move);
    }

    public abstract class PieceRule : PieceDecorator, IPieceRule
    {
        public abstract IBoard Board { get; }

        public override IEnumerable<PieceMove> MoveSet => InnerPiece.MoveSet.Select(ConstrainMove).Where(x => x != null);

        public virtual PieceMove ConstrainMove(PieceMove move)
        {
            return move;
        }

        public virtual bool ValidateMove(PieceMove move)
        {
            return true;
        }

        public override PieceMove GetMoveTo(Position position)
        {
            var baseMove = InnerPiece.GetMoveTo(position);
            if (baseMove == null) return null;

            return ConstrainMove(baseMove);
        }
    }
}
