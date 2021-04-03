using ChessClassLibrary.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games
{
    public interface IGame
    {
        bool CanPerformMove(Move move);
        void TryPerformMove(Move move);
        void PerformMove(Move move);
    }
}
