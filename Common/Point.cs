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

    public readonly struct Point<T>
    {
        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object other)
        {
            if (!(other is Point<T>))
            {
                return false;
            }
            var otherPoint = (Point<T>)other;
            return this.x.Equals(otherPoint.x) && this.y.Equals(otherPoint.y);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(x, y);
        }

        public readonly T x;
        public readonly T y;

        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}
