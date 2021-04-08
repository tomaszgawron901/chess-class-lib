using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.PieceRules;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.BoardRules
{
    public class PieceOnBoard : BasePieceDecorator
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

        public new bool IsMoveValid(PieceMove move)
        {
            return Board.IsInRange(Position + move.Shift);
        }
    }
}
