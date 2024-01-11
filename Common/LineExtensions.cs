using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Common
{
    public static class LineExtensions
    {
        public static bool IsHorizontal<T>(this Line<T> line) where T : INumber<T>
        {
            return line.start.x.Equals(line.end.x);
        }

        public static bool IsVertical<T>(this Line<T> line) where T : INumber<T>
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

        public static long Length(this Line<int> line)
        {
            return (line.end - line.start).Magnitude();
        }

        /// <summary>
        /// Use the shoelace formula to calculate the area of a polygon.
        /// https://en.wikipedia.org/wiki/Shoelace_formula
        /// </summary>
        /// <param name="lines">Clockwise or counterclockwise list of lines that form the polygon</param>
        /// <returns>area</returns>
        public static long AreaOfPolygon(this IList<Line<int>> lines)
        {
            Debug.Assert(lines.First().start == lines.Last().end);

            var sumOfLeftPairs = 0L;
            var sumOfRightPairs = 0L;
            foreach (var line in lines)
            {
                sumOfLeftPairs += (line.start.x * (long)line.end.y);
                sumOfRightPairs += (line.start.y * (long)line.end.x);
            }

            var shoelace = Math.Abs(sumOfLeftPairs - sumOfRightPairs) / 2L;
            return shoelace;
        }

        /// <summary>
        /// This calculates the area of a (integer) grid that is enclosed by a polygon. Points on the polygon are considered to be inside the polygon.
        /// </summary>
        /// <param name="lines">Lines that form the polygon</param>
        /// <returns>area</returns>
        public static long AreaOfGridWithPicksTheorem(this IList<Line<int>> lines)
        {
            var area = lines.AreaOfPolygon();
            var length = lines.Sum(l => l.Length());
            return area + 1 + (length / 2);
        }
    }
}
