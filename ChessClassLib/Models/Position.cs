using System;

namespace ChessClassLib.Models
{
    public struct Position : IEquatable<Position>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Position other)
        {
            if (X == other.X && Y == other.Y)
                return true;
            return false;
        }

        public override bool Equals(object other)
        {
            if (other is Position)
            {
                return Equals((Position)other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public Position Plus(Shift shift)
        {
            return new Position(X + shift.X, Y + shift.Y);
        }

        public Position Minus(Shift shift)
        {
            return new Position(X - shift.X, Y - shift.Y);
        }

        public Shift Minus(Position other)
        {
            return new Shift(X - other.X, Y - other.Y);
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }

        public static Position operator +(Position position, Shift shift)
        {
            return position.Plus(shift);
        }

        public static Position operator -(Position position, Shift shift)
        {
            return position.Minus(shift);
        }

        public static Shift operator -(Position left, Position right)
        {
            return left.Minus(right);
        }

        public override string ToString()
        {
            return $"{(char)(X + 'A')}{Y + 1}";
        }
    }
}
