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

        public static T[][] GetColumns<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1)
                .Select(col => Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1).Select(row => input[row, col]).ToArray()).ToArray();
        }

        public static void Deconstruct<T>(this T[] array, out T s1, out T s2, out T s3)
        {
            s1 = array[0];
            s2 = array[1];
            s3 = array[2];
        }

    }
}
