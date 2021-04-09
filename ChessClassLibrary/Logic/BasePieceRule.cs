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
        protected BasePieceDecorator pieceDecorator;
        public BasePieceRule(BasePieceDecorator pieceDecorator)
        {
            this.pieceDecorator = pieceDecorator;
        }

        public override IPiece Piece => pieceDecorator;
        public BasePieceDecorator InnerPieceDecorator => pieceDecorator;

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Select(MoveModifier).Where(move => move != null && move.MoveTypes.Length != 0);
    }
}
