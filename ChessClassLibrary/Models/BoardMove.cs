namespace ChessClassLibrary.Models
{
    public struct BoardMove
    {
        public Position current { get; set; }
        public Position destination { get; set; }

        public BoardMove(Position current, Position destination)
        {
            this.current = current;
            this.destination = destination;
        }
    }
}
