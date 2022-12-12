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

        var path = input.Dijkstra(start, end, GetNeighbors);

        return path.Length;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 363;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var starts = input.Find('S').Concat(input.Find('a'));

        int shortest = int.MaxValue;

      

        foreach (var start in starts)
        {
            ConsoleX.WriteLine($"Starting at {start} where char is {input[start.x, start.y]}");
            var end = input.Find('E').First();

            var path = input.Dijkstra(start, end, GetNeighbors);
            if (path.Length < shortest && path.Length > 0)
            {
                shortest = path.Length;
            }
            ConsoleX.WriteLine($"Path length {path.Length}");
        }

        return shortest;
    }

    IEnumerable<((int x, int y) point, long weight)> GetNeighbors(char[,] grid, (int x, int y) current)
    {
        var myHeight = grid[current.x, current.y];
        if (myHeight == 'S') myHeight = 'a';
        var neighbors = grid.GetNeighbors(current)
            .Where(n =>
            {
                var neighborHeight = grid[n.x, n.y];
                if (neighborHeight == 'E') neighborHeight = 'z';
                return neighborHeight <= myHeight + 1;
            });

        return neighbors.Select(n => (n, 1L));
    }
}