using Common;

namespace ConsoleApp2021;

public class Day15 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        var start = (x: 0, y: 0);
        var goal = (x: input.GetUpperBound(0), y: input.GetUpperBound(1));

        var path = Dijkstra(input, start, goal);

        var sum = path.Sum(p => input[p.x, p.y]);

        return sum;
    }

    private (int x, int y)[] Dijkstra(int[,] input, (int x, int y) start, (int x, int y) goal)
    {
        var path = new List<(int x, int y)>();

        PriorityQueue<(int x, int y), long> frontier = new ();

        Dictionary<(int,int), (int, int)> cameFrom = new();
        var totalRisk = new long[input.GetUpperBound(0)+1,input.GetUpperBound(1)+1];
        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                totalRisk[x,y] = long.MaxValue;
            }
        }

        totalRisk[start.x, start.y] = input[start.x, start.y];
        frontier.Enqueue(start, input[start.x,start.y]);
        var visited = new HashSet<(int, int)>();
        var movemap = new Dictionary<(int, int), (int, int)>();

        var total = input.Length;

        while (frontier.Count > 0)
        {
            if (frontier.Count % 100000 == 0)
            {
                Console.WriteLine($"{frontier.Count} - {visited.Count}/{total} - {100.0 * (visited.Count / (float)total)}");
            }

            //move
            var current = frontier.Dequeue();
            visited.Add(current);

            if (current == goal)
            {
                break;
            }

            //explore
            var neighbors = GetNeighbors(input, current.x, current.y);
            foreach (var neighbor in neighbors.Where(n => !visited.Contains(n)))
            {
                var currentCost = totalRisk[current.x, current.y];
                var neighborCost = currentCost + input[neighbor.x, neighbor.y];

                if (totalRisk[neighbor.x, neighbor.y] > neighborCost)
                {
                    frontier.Enqueue(neighbor, neighborCost);

                    totalRisk[neighbor.x, neighbor.y] = neighborCost;
                    if (movemap.ContainsKey(neighbor))
                    {
                        movemap[neighbor] = current;
                    }
                    else
                    {
                        movemap.Add(neighbor, current);
                    }
                }

            }


        }

        var prev = goal;
        while (prev != start)
        {
            path.Add(prev);
            prev = movemap[prev];
        }

        //PrintGrid(input, path.ToHashSet());

        return path.ToArray();

    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        input = Multiply(input, 5);

        var start = (x: 0, y: 0);
        var goal = (x: input.GetUpperBound(0), y: input.GetUpperBound(1));

        var path = Dijkstra(input, start, goal);

        var sum = path.Sum(p => input[p.x, p.y]);

        return sum;
    }

    private int[,] Multiply(int[,] input, int m)
    {
        var result = new int[(input.GetUpperBound(0) + 1) * m, (input.GetUpperBound(1) + 1) * m];
        var maxx = input.GetUpperBound(0) +1;
        var maxy = input.GetUpperBound(1) +1;

        for (int tilex = 1; tilex <= m; tilex++)
        {
            for (int tiley = 1; tiley <= m; tiley++)
            {
                for (int x = 0; x <= input.GetUpperBound(0); x++)
                {
                    for (int y = 0; y <= input.GetUpperBound(1); y++)
                    {
                        var resultx = ((tilex-1)*maxx) + x;
                        var resulty = ((tiley-1)*maxy) + y;

                        var orignal = input[x, y];
                        var increment = ((tilex -1)+ (tiley-1));
                        var value = orignal + increment;

                        value = (value - 1) % 9 + 1;
                        result[resultx, resulty] = value;
                    }
                }
            }
        }

        return result;
    }

    public void PrintGrid(int[,] grid, HashSet<(int x, int y)> highlight)
    {
        for (int x = 0; x <= grid.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= grid.GetUpperBound(1); y++)
            {
                var olderColor = Console.ForegroundColor;

                if (highlight.Contains((x, y)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (grid[x,y] == int.MaxValue)
                    Console.Write('.');
                else
                    Console.Write(grid[x, y]);

                Console.ForegroundColor = olderColor;

            }

            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private (int x, int y)[] GetNeighbors(int[,] grid, int ix, int iy)
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