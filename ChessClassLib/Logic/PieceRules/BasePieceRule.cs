using ChessClassLib.Pieces;
using ChessClassLib.Logic.Boards;

namespace ChessClassLib.Logic.PieceRules
{
    public static partial class IPieceExtensions
    {
        public static BasePieceRule AddBasePieceRule(this IPiece innerPiece, IBoard board)
        {
            return new BasePieceRule(innerPiece, board);
        }
    }

    public class BasePieceRule : PieceRule
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
