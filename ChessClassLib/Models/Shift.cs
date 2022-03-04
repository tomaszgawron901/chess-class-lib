using System;

namespace ChessClassLib.Models
{
    public struct Shift : IEquatable<Shift>
    {
        public readonly static Shift Default = new Shift(0, 0);

        public int X { get; set; }

        public int Y { get; set; }

        public Shift(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Shift Plus(Shift shift)
        {
            return new Shift(X + shift.X, Y + shift.Y);
        }

        public Shift Minus(Shift shift)
        {
            return new Shift(X - shift.X, Y - shift.Y);
        }

        public bool Equals(Shift other)
        {
            if (X == other.X && Y == other.Y)
                return true;
            return false;
        }

        public override bool Equals(object other)
        {
            if (other is Shift)
            {
                return Equals((Shift)other);
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

        public static bool operator ==(Shift p1, Shift p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Shift p1, Shift p2)
        {
            return !p1.Equals(p2);
        }

        public static Shift operator +(Shift left, Shift right)
        {
            return left.Plus(right);
        }

        public static Shift operator -(Shift left, Shift right)
        {
            return left.Minus(right);
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}

