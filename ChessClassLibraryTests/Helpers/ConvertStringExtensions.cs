using ChessClassLib.Models;

namespace ChessClassLibraryTests.Helpers
{
    public static class ConvertStringExtensions
    {
        public static Position ToPosition(this string str)
        {
            int X = str[0] - 'a';
            int Y = int.Parse(str.Substring(1)) - 1;
            return new Position(X, Y);
        }

        public static BoardMove ToBoardMove(this string str)
        {
            string[] strArray = str.Split(" to ");
            Position p1 = strArray[0].ToPosition();
            Position p2 = strArray[1].ToPosition();
            return new BoardMove(p1, p2);
        }
    }
}
