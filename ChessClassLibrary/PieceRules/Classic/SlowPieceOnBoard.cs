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

namespace ChessClassLibrary.Games.ClassicGame
{
    public class SlowPieceOnClassicBoard : BasePieceDecorator
    {
        protected readonly Board board;
        public SlowPieceOnClassicBoard(IPiece piece, Board board)
            : base(piece)
        {
            this.board = board;
        }

        public new IEnumerable<PieceMove> MoveSet => Piece.MoveSet.Where(IsMoveValid);

        public new void MoveToPosition(Position position)
        {
            board.SetPiece(null, Position);
            board.SetPiece(this, position);
            this.Piece.MoveToPosition(position);
        }

        public new bool IsMoveValid(PieceMove move)
        {
            var movePosition = Position + move.Shift;
            if (board.IsInRange(movePosition) && piece.IsMoveValid(move))
            {
                if (move.MoveTypes.Contains(MoveType.Move))
                {
                    return board.GetPiece(movePosition) == null;
                }
                else if (move.MoveTypes.Contains(MoveType.Kill))
                {
                    var pieceAtMovePosition = board.GetPiece(movePosition);
                    return pieceAtMovePosition != null && pieceAtMovePosition.Color != this.Color;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
