using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
