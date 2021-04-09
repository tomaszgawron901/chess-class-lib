using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
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
        public Board Board { get; private set; }
        public PieceOnBoard(Piece piece, Board board)
            : base(piece)
        {
            this.Board = board;
        }

        public new void MoveToPosition(Position position)
        {
            Board.SetPiece(null, Position);
            Board.SetPiece(this, position);
            this.Piece.MoveToPosition(position);
        }

        protected override PieceMove MoveModifier(PieceMove move)
        {
            if (Board.IsInRange(Position + move.Shift))
            {
                return move;
            }
            return null;
        }
    }
}
