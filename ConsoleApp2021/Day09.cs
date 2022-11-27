using Common;

namespace ConsoleApp2021;

public class Day09 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        var lows = new List<int>();

        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                if (input[x, y] < LowestNeigbor(input, x, y))
                {
                    lows.Add(input[x, y]);
                }
            }
        }

        return lows.Sum(l => l + 1); ;
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        var lows = new List<(int,int)>();

        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                if (input[x, y] < LowestNeigbor(input, x, y))
                {
                    lows.Add((x,y));
                }
            }
        }

        var basins = new List<int>();

        foreach (var low in lows)
        {
            var size = FindBasinSize(input, low.Item1, low.Item2);
            basins.Add(size);
        }

        return basins.OrderBy(b => b).TakeLast(3).Aggregate((a, b) => a * b);
    }

    private int FindBasinSize(int[,] grid, int ix, int iy, List<(int, int)> seen = null)
    {
        if (grid[ix, iy] == 9) return 0;
        if (seen == null)
            seen = new List<(int, int)>();

        seen.Add((ix,iy));

        var minx = Math.Max(ix - 1, 0);
        var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
        var miny = Math.Max(iy - 1, 0);
        var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

        int size = 1;

        for (int x = minx; x <= maxx; x++)
        {
            for (int y = miny; y <= maxy; y++)
            {
                if (seen.Contains((x, y)) || (x != ix && y != iy))
                {
                }
                else
                {
                    size += FindBasinSize(grid, x, y, seen);
                }
            }
        }

        return size;
    }

    private int LowestNeigbor(int[,] grid, int ix, int iy)
    {

        var minx = Math.Max(ix - 1, 0);
        var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
        var miny = Math.Max(iy - 1, 0);
        var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

        int min = int.MaxValue;

        for (int x = minx; x <= maxx; x++)
        {
            if (x != ix && grid[x, iy] < min)
                min = grid[x, iy];
        }

        for (int y = miny; y <= maxy; y++)
        {
            if (y != iy && grid[ix, y] < min)
                min = grid[ix, y];
        }

        return min;
    }

    public int[,] Parse(string[] lines)
    {
        var width = lines.First().Length;
        var height = lines.Count();

        var grid = new int[height, width];

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                grid[h, w] = int.Parse(lines[h][w].ToString());
            }
        }
        return grid;
    }

}