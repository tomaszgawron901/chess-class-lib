namespace ChessClassLibrary.Games
{
    public interface IGame
    {
        bool CanPerformMove(Move move);
        void TryPerformMove(Move move);
        void PerformMove(Move move);
    }
}
