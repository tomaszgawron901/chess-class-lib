using System.Text.Json.Serialization;

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
    }
}
