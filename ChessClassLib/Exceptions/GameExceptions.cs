using System;

namespace ChessClassLib.Exceptions
{
    public class CouldNotPerformMoveException: Exception
    {
        public override string Message => "A move could not be performed.";

        public override string ToString() => Message;
    }
}
