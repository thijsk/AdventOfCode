using System.Collections.Generic;
using System.Numerics;

namespace Common;

public static class Point3Extensions
{
    public static T ManhattanDistanceTo<T>(this Point3<T> point, Point3<T> other) where T : INumber<T>
    {
        return Math2.Abs(point.x - other.x) + Math2.Abs(point.y - other.y) + Math2.Abs(point.z - other.z);
    }

    public static IEnumerable<Point3<T>> GetNeighbors<T>(this Point3<T> point) where T : INumber<T>
    {
        yield return point + (T.One, T.Zero, T.Zero);
        yield return point + (T.Zero, T.One, T.Zero);
        yield return point + (T.Zero, T.Zero, T.One);
        yield return point - (T.One, T.Zero, T.Zero);
        yield return point - (T.Zero, T.One, T.Zero);
        yield return point - (T.Zero, T.Zero, T.One);
    }
}