using ChessClassLibrary.Boards;
using ChessClassLibrary.Models;
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
        protected Board board;
        public override Board Board => board;

        public BasePieceContainer(IPiece piece, Board board)
        {
            this.piece = piece;
            this.board = board;
        }

        public override IPiece Piece => this.piece;

        public override PieceMove MoveModifier(PieceMove move)
        {
            if (Board.IsInRange(Position + move.Shift))
            {
                return move;
            }
            return null;
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            return Board.IsInRange(Position + move.Shift);
        }

        public override void MoveToPosition(Position position)
        {
            Board.SetPiece(Board.GetPiece(Position), position);
            Board.SetPiece(null, Position);
            this.Piece.MoveToPosition(position);
        }
    }
}
