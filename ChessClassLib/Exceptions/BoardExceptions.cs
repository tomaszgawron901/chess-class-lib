using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLib.Exceptions
{
    public class PositionOutsideBoardRangeException : Exception
    {
        public override string Message => "A piece could not beset at the given position.";

        public override string ToString() => Message;
    }
}
