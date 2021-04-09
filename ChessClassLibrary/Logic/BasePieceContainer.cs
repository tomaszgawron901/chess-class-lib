using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic
{
    public abstract class BasePieceContainer : BasePieceDecorator
    {
        protected IPiece piece;

        public BasePieceContainer(IPiece piece)
        {
            this.piece = piece;
        }

        public override IPiece Piece => this.piece;

        public override IEnumerable<PieceMove> MoveSet => Piece.MoveSet;

        public override bool ValidateNewMove(PieceMove move)
        {
            return true;
        }
    }
}
