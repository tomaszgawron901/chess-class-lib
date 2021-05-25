
namespace ChessClassLibrary.Models
{
    public struct BoardMove
    {
        public Position Current { get; set; }
        public Position Destination { get; set; }

        public BoardMove(Position current, Position destination)
        {
            this.Current = current;
            this.Destination = destination;
        }

        public override string ToString()
        {
            return $"{(char)(Current.X+ 'A')}{Current.Y+1} to {(char)(Destination.X + 'A')}{Destination.Y+1}";
        }
    }
}
