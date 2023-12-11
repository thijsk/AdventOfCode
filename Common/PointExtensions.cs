using System.Collections.Generic;
using System.Numerics;

namespace Common
{
    public static class PointExtensions
    {
        public static T ManhattanDistanceTo<T>(this Point<T> point, Point<T> other) where T : INumber<T>
        {
            return Math2.Abs(point.x - other.x) + Math2.Abs(point.y - other.y);
        }

        public static IEnumerable<Point<T>> GetNeighbors<T>(this Point<T> point) where T : INumber<T>
        {
            yield return point + (T.One, T.Zero);
            yield return point + (T.Zero, T.One);
            yield return point - (T.One, T.Zero);
            yield return point - (T.Zero, T.One);
        }
    }
}
