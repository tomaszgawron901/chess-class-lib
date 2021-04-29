namespace ChessClassLibrary.Models
{
    public struct BoardMove
    {
        public readonly Position current;
        public readonly Position destination;

        public BoardMove(Position current, Position destination)
        {
            this.current = current;
            this.destination = destination;
        }
    }
}
