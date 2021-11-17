namespace Common
{
    public struct Point3<T>
    {
        public Point3(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public readonly T x;
        public readonly T y;
        public readonly T z;
    }

}
