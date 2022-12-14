using System.Collections.Generic;
using System.Numerics;

namespace Common
{
    public static class LineExtensions
    {
        public static bool IsHorizontal<T>(this Line<T> line)
        {
            return line.start.x.Equals(line.end.x);
        }

        public static bool IsVertical<T>(this Line<T> line)
        {
            return line.start.y.Equals(line.end.y);
        }

        public static IEnumerable<Point<T>> ToPoints<T>(this Line<T> line) where T : INumber<T>
        {
            var xIncrement = line.start.x <= line.end.x ? T.One : -T.One;
            var yIncrement = line.start.y <= line.end.y ? T.One : -T.One;

            for (T x = line.start.x; x != line.end.x + xIncrement; x += xIncrement)
            for (T y = line.start.y; y != line.end.y + yIncrement; y += yIncrement)
            {
                yield return new Point<T>(x, y);
            }
        }
    }
}
