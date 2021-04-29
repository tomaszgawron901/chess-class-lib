using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;

namespace ChessClassLibrary.Games
{
    public interface IGame
    {
        PieceColor CurrentPlayerColor { get;}
        GameState GameState { get;}
        bool CanPerformMove(BoardMove move);
        void TryPerformMove(BoardMove move);
        void PerformMove(BoardMove move);
        IEnumerable<PieceMove> GetPieceMoveSetAtPosition(Position position);
    }
}
