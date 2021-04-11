namespace ChessClassLibrary.Games
{
    public interface IGame
    {
        bool CanPerformMove(BoardMove move);
        void TryPerformMove(BoardMove move);
        void PerformMove(BoardMove move);
    }
}
