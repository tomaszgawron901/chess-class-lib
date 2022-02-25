using ChessClassLib.Pieces;
using ChessClassLibrary.Logic.Boards;
using ChessClassLibrary.Pieces;

namespace ChessClassLib.Logic.PieceRules
{
    public abstract class BasePieceRule : PieceRule
    {
        protected override IPiece InnerPiece { get; }
        public override IBoard Board { get; }
        public BasePieceRule(IPiece innerPiece, IBoard board)
        {
            InnerPiece = innerPiece;
            Board = board;
        }
    }
}
