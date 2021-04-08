using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{
    class MovablePieceOnBoard: BasePieceDecorator
    {
        protected readonly Board board;
        public MovablePieceOnBoard(IPiece piece, Board board)
            : base(piece)
        {
            this.board = board;
        }

        public new IEnumerable<PieceMove> MoveSet
        {
            get
            {
                foreach (PieceMove move in Piece.MoveSet)
                {
                    if (board.GetPiece(Position + move.Shift) != null)
                    {
                        move.MoveTypes = move.MoveTypes.Where(x => x != MoveType.Move).ToArray();
                    }
                }
                return Piece.MoveSet.Where(move => move.MoveTypes.Length != 0);
            }
        }

        public new bool IsMoveValid(PieceMove move)
        {
            return board.GetPiece(Position + move.Shift) != null || move.MoveTypes.Contains(MoveType.Move);
        }

        public new PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (this.IsMoveValid(baseMove))
            {
                if (board.GetPiece(Position + baseMove.Shift) == null)
                {
                    baseMove.MoveTypes = new MoveType[] { MoveType.Move };
                }
                else
                {
                    baseMove.MoveTypes = baseMove.MoveTypes.Where(move => move != MoveType.Move).ToArray();
                }
                return baseMove;
            }
            return null;
        }
    }
}
