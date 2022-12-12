using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringArrayExtensions
    {
        public static string[][] SplitByEmptyLines(this string[] lines)
        {
            var result = new List<string[]>();
            var group = new List<string> { };
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    result.Add(group.ToArray());
                    group.Clear();
                }
                else
                {
                    group.Add(line);
                }
            }
            result.Add(group.ToArray());
            return result.ToArray();
        }

        public static T[,] GetGrid<T>(this string[] lines, Func<char, T> parse)
        {
            var width = lines.First().Length;
            var height = lines.Length;

            var grid = new T[height, width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    grid[h, w] = parse(lines[h][w]);
                }
            }
            return grid;
        }

      
    }
}
