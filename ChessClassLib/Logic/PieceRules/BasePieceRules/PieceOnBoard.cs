using ChessClassLib.Pieces;
using ChessClassLibrary.Logic.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;

namespace ChessClassLib.Logic.PieceRules.BasePieceRules
{
    public static partial class IPieceExtensions
    {
        public static PieceOnBoard AddPieceOnBoard(this IPiece innerPiece, IBoard board)
        {
            return new PieceOnBoard(innerPiece, board);
        }
    }

    /// <summary>
    /// Container class for slow pieces.
    /// </summary>
    public class PieceOnBoard : BasePieceRule
    {
        public PieceOnBoard(IPiece innerPiece, IBoard board)
            : base(innerPiece, board)
        {}

        public override PieceMove ConstrainMove(PieceMove move)
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
        public override bool ValidateMove(PieceMove move)
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
            InnerPiece.MoveToPosition(position);
        }
    }
}
