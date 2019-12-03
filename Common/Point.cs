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

    public struct Point<T>
    {
        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly T x;
        public readonly T y;
    }
}
