using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    public struct Position: IEquatable<Position>
    {
        private int x;
        private int y;

        /// <summary>
        /// Returns horizontal coordinate.
        /// </summary>
        public int X
        {
            get { return x; }
        }
        /// <summary>
        /// Returns vertical coordinate.
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Check whether other Point equals this Point.
        /// </summary>
        /// <param name="other">Point to compare.</param>
        /// <returns>True when Points are equal, otherwise false.</returns>
        public bool Equals(Position other)
        {
            if (X == other.X && Y == other.Y)
                return true;
            return false;
        }

        /// <summary>
        /// Check whether other object equals this Point.
        /// </summary>
        /// <param name="other">Object to compare.</param>
        /// <returns>True when given object equals this Point, otherwise false.</returns>
        public override bool Equals(object other)
        {
            if (other is Position)
            {
                return Equals((Position)other);
            }
            return false;
        }

        /// <summary>
        /// Calculate and returns a hashcode.
        /// </summary>
        /// <returns>The intiger hashcode.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Adds other Point to this Point and returns the outcome.
        /// </summary>
        /// <param name="other">Point to add.</param>
        /// <returns>New Point which is the sum of two Points.</returns>
        public Position Plus(Position other)
        {
            return new Position(X + other.X, Y + other.Y);
        }

        /// <summary>
        /// Subtracts other Point from this Point and returns the outcome.
        /// </summary>
        /// <param name="other">Point to subtract.</param>
        /// <returns>New Point which is the subtraction of this Point and given other Point..</returns>
        public Position Minus(Position other)
        {
            return new Position(X - other.X, Y - other.Y);
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }

        public static Position operator +(Position left, Position right)
        {
            return left.Plus(right);
        }

        public static Position operator -(Position left, Position right)
        {
            return left.Minus(right);
        }
    }
}
