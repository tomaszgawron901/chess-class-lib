using ChessClassLibrary.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic
{
    /// <summary>
    /// Base class responsible for chaining piece behaviors and constrains.
    /// </summary>
    public abstract class BasePieceRule : BasePieceDecorator
    {
        /// <summary>
        /// Base PieceDecorator that is being decorated.
        /// </summary>
        protected IBasePieceDecorator pieceDecorator;
        public BasePieceRule(IBasePieceDecorator pieceDecorator)
        {
            this.pieceDecorator = pieceDecorator;
        }

        public override IBoard Board => this.pieceDecorator.Board;

        public override IPiece Piece => pieceDecorator;
        public IBasePieceDecorator InnerPieceDecorator => pieceDecorator;

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Select(MoveModifier).Where(move => move != null);

        public override void MoveToPosition(Position position)
        {
            Piece.MoveToPosition(position);
        }

        public override PieceMove GetShiftMove(Position position)
        {
            var baseMove = Piece.GetShiftMove(position);
            if (baseMove == null) return null;

            return MoveModifier(baseMove);
        }
    }
}
