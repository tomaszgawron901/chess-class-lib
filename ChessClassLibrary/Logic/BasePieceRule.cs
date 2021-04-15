using ChessClassLibrary.Boards;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic
{
    public abstract class BasePieceRule : BasePieceDecorator
    {
        protected IBasePieceDecorator pieceDecorator;
        public BasePieceRule(IBasePieceDecorator pieceDecorator)
        {
            this.pieceDecorator = pieceDecorator;
        }

        public override Board Board => this.pieceDecorator.Board;

        public override IPiece Piece => pieceDecorator;
        public IBasePieceDecorator InnerPieceDecorator => pieceDecorator;

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Select(MoveModifier).Where(move => move != null);

        public override void MoveToPosition(Position position)
        {
            Piece.MoveToPosition(position);
        }

        public override PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (baseMove == null) return null;

            return MoveModifier(baseMove);
        }
    }
}
