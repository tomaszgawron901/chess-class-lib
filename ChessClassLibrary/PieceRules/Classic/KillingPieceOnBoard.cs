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
    class KillingPieceOnBoard : BasePieceDecorator
    {
        protected readonly Board board;
        public KillingPieceOnBoard(IPiece piece, Board board)
            : base(piece)
        {
            this.board = board;
        }

        public new IEnumerable<PieceMove> MoveSet
        {
            get
            {
                var newMoveSet = new List<PieceMove>();
                foreach (PieceMove move in Piece.MoveSet)
                {
                    var pieceAtDestination = board.GetPiece(Position + move.Shift);
                    if (IsMoveValid(move))
                    {
                        if (pieceAtDestination != null && pieceAtDestination.Color != Color)
                        {
                            move.MoveTypes = new MoveType[] {MoveType.Kill };
                        }
                        newMoveSet.Add(move);
                    }
                }
                return newMoveSet;
            }
        }

        public new bool IsMoveValid(PieceMove move)
        {
            var pieceAtDestination = board.GetPiece(Position + move.Shift);
            return pieceAtDestination == null || pieceAtDestination.Color == Color || move.MoveTypes.Contains(MoveType.Kill);
        }

        public new PieceMove GetMoveTo(Position position)
        {
            var baseMove = Piece.GetMoveTo(position);
            if (this.IsMoveValid(baseMove))
            {
                var pieceAtDestination = board.GetPiece(Position + baseMove.Shift);
                if (pieceAtDestination != null && pieceAtDestination.Color != Color)
                {
                    baseMove.MoveTypes = new MoveType[] { MoveType.Kill };
                }
                return baseMove;
            }
            return null;
        }
    }
}
