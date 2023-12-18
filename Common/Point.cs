using System;
using System.Numerics;

namespace Common
{
    public struct Point
    {
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly double x;
        public readonly double y;
    }

    public struct Point<T> where T: INumber<T>
    {
        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object other)
        {
            if (!(other is Point<T> otherPoint))
            {
                return false;
            }

            return this.x.Equals(otherPoint.x) && this.y.Equals(otherPoint.y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public T x;
        public T y;

        public override string ToString()
        {
            return $"{x},{y}";
        }

        public static implicit operator Point<T>((T x, T y) p) => new(p.x, p.y);

        public static bool operator ==(Point<T> me, Point<T> other) => me.Equals(other);
        public static bool operator !=(Point<T> me, Point<T> other) => !(me == other);

        public static Point<T> operator +(Point<T> me, Point<T> other) => new(me.x + other.x, me.y + other.y);
        public static Point<T> operator -(Point<T> me, Point<T> other) => new(me.x - other.x, me.y - other.y);
    }
}
