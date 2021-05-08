namespace ChessClassLibrary.Models
{
    public struct BoardMove
    {
        public Position current;
        public Position destination;

        public BoardMove(Position current, Position destination)
        {
            this.current = current;
            this.destination = destination;
        }
    }
}
