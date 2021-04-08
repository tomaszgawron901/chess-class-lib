using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules
{
    public abstract class BasePieceDecorator : IPiece
    {
        protected readonly IPiece piece;
        public BasePieceDecorator(IPiece piece)
        {
            this.piece = piece;
        }

        public  IPiece Piece { get => this.piece; }

        public virtual IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Where(IsMoveValid);
        public Position Position { get => Piece.Position; }

        public PieceColor Color => Piece.Color;

        public PieceType Type => Piece.Type;

        public bool WasMoved => Piece.WasMoved;

        public bool CanMoveAnywhere() => Piece.CanMoveAnywhere();

        public virtual PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (this.IsMoveValid(baseMove))
            {
                return baseMove;
            }
            return null;
        }

        public virtual bool IsMoveValid(PieceMove move) => piece.IsMoveValid(move);

        public virtual void MoveToPosition(Position position) => Piece.MoveToPosition(position);
    }
}
