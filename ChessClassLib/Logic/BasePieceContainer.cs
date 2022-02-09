using ChessClassLibrary.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;

namespace ChessClassLibrary.Logic
{
    /// <summary>
    /// Base class responsible for handling actions on the given Board.
    /// </summary>
    public abstract class BasePieceContainer : BasePieceDecorator
    {
        protected IPiece piece;
        protected IBoard board;
        public override IBoard Board => board;

        public BasePieceContainer(IPiece piece, IBoard board)
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

        /// <summary>
        /// Checks if new PieceMove is in range of the board.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public override bool ValidateNewMove(PieceMove move)
        {
            return Board.IsInRange(Position + move.Shift);
        }

        /// <summary>
        /// Moves piece to given position on the board.
        /// </summary>
        /// <param name="position"></param>
        public override void MoveToPosition(Position position)
        {
            Board.SetPiece(Board.GetPiece(Position), position);
            Board.SetPiece(null, Position);
            this.Piece.MoveToPosition(position);
        }
    }
}
