using ChessClassLib.Pieces;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Models;

namespace ChessClassLib.Logic.PieceRules
{
    public interface IPieceRule : IPiece
    {
        IBoard Board { get; }
        PieceMove ConstrainMove(PieceMove move);
        bool ValidateMove(PieceMove move);
    }
}
