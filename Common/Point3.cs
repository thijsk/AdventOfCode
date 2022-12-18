using System;
using System.Numerics;

namespace Common
{
    public struct Point3<T> where T:  INumber<T>
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


        public override bool Equals(object other)
        {
            if (!(other is Point3<T> otherPoint))
            {
                return false;
            }

            return this.x.Equals(otherPoint.x) && this.y.Equals(otherPoint.y) && this.z.Equals(otherPoint.z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }


        public override string ToString()
        {
            return $"{x},{y},{z}";
        }

        public static implicit operator Point3<T>((T x, T y, T z) p) => new(p.x, p.y, p.z);

        public static bool operator ==(Point3<T> me, Point3<T> other) => me.Equals(other);
        public static bool operator !=(Point3<T> me, Point3<T> other) => !(me == other);

        public static Point3<T> operator +(Point3<T> me, Point3<T> other) => new(me.x + other.x, me.y + other.y, me.z + other.z);
        public static Point3<T> operator -(Point3<T> me, Point3<T> other) => new(me.x - other.x, me.y - other.y, me.z - other.z);
    }

}
