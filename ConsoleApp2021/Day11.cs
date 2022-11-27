using Common;

namespace ConsoleApp2021;

public class Day11 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);
//        PrintGrid(input);
        long totalflashes = 0;
        for (int step = 1; step <= 100; step++)
        {
            if (step % 10 == 0)
                Console.WriteLine($"Step: {step}");
            long flashes = 0;

            for (int x = 0; x <= input.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= input.GetUpperBound(1); y++)
                {
                    input[x, y]++;
                }
            }

            for (int x = 0; x <= input.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= input.GetUpperBound(1); y++)
                {
                    if (input[x, y] > 9)
                    {
                        input[x, y] = 0;
                        FlashNeighbors(input, x, y);


                    }
                }
            }

            flashes = input.GetRows().SelectMany(v => v).Count(v => v == 0);
            if (step % 10 == 0)
            {
                Console.WriteLine($"Flashes: {flashes}");
                PrintGrid(input);
            }

            totalflashes += flashes;
        }

        return totalflashes;

    }

    private void FlashNeighbors(int[,] grid, int ix, int iy)
    {
        var minx = Math.Max(ix - 1, 0);
        var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
        var miny = Math.Max(iy - 1, 0);
        var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

        int count = 0;

        for (int x = minx; x <= maxx; x++)
        {
            for (int y = miny; y <= maxy; y++)
            {
                if (x != ix || y != iy)
                {
                    if (grid[x, y] > 0)
                    {
                        grid[x, y]++;

                        if (grid[x, y] > 9)
                        {
                            grid[x, y] = 0;
                            FlashNeighbors(grid, x, y);
                        }
                    }
                }
            }
        }
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);
        for (int step = 1;; step++)
        {
            long flashes = 0;

            for (int x = 0; x <= input.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= input.GetUpperBound(1); y++)
                {
                    input[x, y]++;
                }
            }

            for (int x = 0; x <= input.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= input.GetUpperBound(1); y++)
                {
                    if (input[x, y] > 9)
                    {
                        input[x, y] = 0;
                        FlashNeighbors(input, x, y);
                    }
                }
            }

            flashes = input.GetRows().SelectMany(v => v).Count(v => v == 0);

            if (flashes == 100)
                return step;
        }
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

    public void PrintGrid(int[,] grid)
    {
        for (int x = 0; x <= grid.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= grid.GetUpperBound(1); y++)
            {
                var olderColor = Console.ForegroundColor;

                if (grid[x,y] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(grid[x, y]);

                Console.ForegroundColor = olderColor;

            }

            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();
    }

}