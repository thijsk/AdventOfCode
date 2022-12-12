using Common;

namespace ConsoleApp2022;

public class Day12 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 370;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var start = input.Find('S').First();
        var end = input.Find('E').First();

        var path = Dijkstra(input, start, end);


        return path.Length;
    }

    private (int x, int y)[] Dijkstra(char[,] input, (int x, int y) start, (int x, int y) goal)
    {
        var path = new List<(int x, int y)>();

        PriorityQueue<(int x, int y), long> frontier = new();

        Dictionary<(int, int), (int, int)> cameFrom = new();
        var pathLength = new long[input.GetUpperBound(0) + 1, input.GetUpperBound(1) + 1];
        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                pathLength[x, y] = long.MaxValue;
            }
        }

        pathLength[start.x, start.y] = 0;
        frontier.Enqueue(start, input[start.x, start.y]);
        var visited = new HashSet<(int, int)>();
        var movemap = new Dictionary<(int, int), (int, int)>();

        var total = input.Length;

        while (frontier.Count > 0)
        {
            //move
            var current = frontier.Dequeue();
            visited.Add(current);

            if (current == goal)
            {
                break;
            }

            //explore
            var myHeight = input[current.x, current.y];
            if (myHeight == 'S') myHeight = 'a';
            var neighbors = input.GetNeighbors(current.x, current.y)
                .Where(n =>
                {
                    var neighborHeight = input[n.x, n.y];
                    if (neighborHeight == 'E') neighborHeight = 'z';
                    return neighborHeight <= (myHeight + 1);
                });
            foreach (var neighbor in neighbors.Where(n => !visited.Contains(n)))
            {
                var currentPathLength = pathLength[current.x, current.y];
                var neighborLength = currentPathLength + 1;

                if (pathLength[neighbor.x, neighbor.y] > neighborLength)
                {
                    frontier.Enqueue(neighbor, neighborLength);

                    pathLength[neighbor.x, neighbor.y] = neighborLength;
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

        //pathLength.ToConsole(v =>
        //{
        //    if (v == long.MaxValue)
        //    {
        //        ConsoleX.Write($"   ");
        //    }
        //    else
        //    {
        //        ConsoleX.Write($" {v:00}");
        //    }
            
        //});

        var prev = goal;
        if (movemap.ContainsKey(prev))
        {
            while (prev != start)
            {
                path.Add(prev);
                prev = movemap[prev];
            }
        }

        return path.ToArray();

    }

    public long Part2()
    {
        PuzzleContext.Answer1 = 29;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var starts = input.Find('S').Concat(input.Find('a'));

        int shortest = int.MaxValue;

        foreach (var start in starts)
        {
            ConsoleX.WriteLine($"Starting at {start} where char is {input[start.x, start.y]}");
            var end = input.Find('E').First();
            var path = Dijkstra(input, start, end);
            if (path.Length < shortest && path.Length > 0)
            {
                shortest = path.Length;
            }
            ConsoleX.WriteLine($"Path length {path.Length}");
        }

        return shortest;
    }
}