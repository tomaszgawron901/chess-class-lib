﻿
namespace ChessClassLib.Models
{
    public struct BoardMove
    {
        public Position Current { get; set; }
        public Position Destination { get; set; }

        public BoardMove(Position current, Position destination)
        {
            Current = current;
            Destination = destination;
        }

        public override string ToString()
        {
            return $"{Current} to {Destination}";
        }
    }
}
