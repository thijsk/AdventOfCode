using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class ArrayExtensions
    {
        public static T[][] GetRows<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0)+1)
                .Select(row => Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1)+1).Select(col => input[row,col]).ToArray()).ToArray();
        }

        public static T[] GetRow<T>(this T[,] input, int row)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1).Select(col => input[row, col])
                .ToArray();
        }

       
        public static T[][] GetColumns<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1)
                .Select(col => Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1).Select(row => input[row, col]).ToArray()).ToArray();
        }

        public static T[] GetColumn<T>(this T[,] input, int col)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1)
                .Select(row => input[row, col]).ToArray();
        }

        public static IEnumerable<T> SubRange<T>(this T[] input, int start, int end)
        {
            if (start > end)
            {
                return input.SubRange(end, start).Reverse();
            }

            return input.Skip(start).Take((end-start)+1);
        }

        public static void ToConsole<T>(this T[,] grid)
        {
            grid.ToConsole(o => ConsoleX.Write(o));
        }

        public static void ToConsole<T>(this T[,] grid, Action<T> write)
        {
            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    var olderColor = Console.ForegroundColor;

                    write(grid[x, y]);
                }

                ConsoleX.WriteLine();
            }
        }

        public static (int x, int y)[] GetNeighbors<T>(this T[,] grid, int ix, int iy)
        {
            var minx = Math.Max(ix - 1, 0);
            var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
            var miny = Math.Max(iy - 1, 0);
            var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

            int count = 0;

            var result = new List<(int, int)>();

            for (int x = minx; x <= maxx; x++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    if (!(x == ix && y == iy) && !(x != ix && y != iy))
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result.ToArray();
        }

        public static IEnumerable<(int x, int y)> Find<T>(this T[,] grid, T search) where T : IEquatable<T>
        {
            var result = new List<(int,int)>();

            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    if (grid[x, y].Equals(search))
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result;
        }

        public static void Deconstruct<T>(this T[] array, out T s1, out T s2)
        {
            s1 = array.ElementAtOrDefault(0);
            s2 = array.ElementAtOrDefault(1);
        }

        public static void Deconstruct<T>(this T[] array, out T s1, out T s2, out T s3)
        {
            s1 = array.ElementAtOrDefault(0);
            s2 = array.ElementAtOrDefault(1);
            s3 = array.ElementAtOrDefault(2);
        }

    }
}
