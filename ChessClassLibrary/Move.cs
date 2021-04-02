using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    public struct Move
    {
        public readonly Position current;
        public readonly Position destination;

        public Move(Position current, Position destination)
        {
            this.current = current;
            this.destination = destination;
        }
    }
}
