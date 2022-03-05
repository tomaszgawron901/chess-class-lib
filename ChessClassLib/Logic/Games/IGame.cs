using ChessClassLib.Enums;
using ChessClassLib.Models;
using System.Collections.Generic;

namespace hessClassLibrary.Logic.Games
{
    public interface IGame
    {
        PieceColor CurrentPlayerColor { get;}
        GameState GameState { get;}
        bool CanPerformMove(BoardMove move);
        bool TryPerformMove(BoardMove move);
        void PerformMove(BoardMove move);
        IEnumerable<PieceMove> GetPieceMoveSetAtPosition(Position position);
        PieceColor? GetWinner();
    }
}
