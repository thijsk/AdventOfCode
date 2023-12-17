using Common;

namespace ConsoleApp2023;

public class Day17 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 722;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => int.Parse(c.ToString()));

        var start = (x: 0, y: 0, d: Directions.Down);
        var goal = (x: input.GetLength(0) - 1, y: input.GetLength(1) - 1);

        var result = input.Dijkstra(start, GetNeighbors,
            s => s.x == goal.x && s.y == goal.y);

        PrintGrid(input, result.path);

        return result.weight;
    }

    private IEnumerable<((int x, int y, (int x, int y) d) point, long weight)> GetNeighbors(int[,] grid,
        (int x, int y, (int x, int y) d) current)
    {
        var directions = Directions.AllCardinal.Where(d => d != (current.d) && d != Directions.Reverse(current.d));

        foreach (var d in directions)
        {
            var weight = 0L;
            for (var step = 1; step <= 3; step++)
            {
                var neighbor = (x: current.x + d.x * step, y: current.y + d.y * step);
                if (grid.IsInGrid(neighbor))
                {
                    weight += grid[neighbor.x, neighbor.y];
                    yield return ((neighbor.x, neighbor.y, d), weight);
                }
            }
        }
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 894;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => int.Parse(c.ToString()));

        var start = (x: 0, y: 0, d: Directions.Down);
        var goal = (x: input.GetLength(0) - 1, y: input.GetLength(1) - 1);

        var result = input.Dijkstra(start, GetNeighbors2,
            s => s.x == goal.x && s.y == goal.y);

       PrintGrid(input, result.path);

        return result.weight;
    }


    private IEnumerable<((int x, int y, (int x, int y) d) point, long weight)> GetNeighbors2(int[,] grid,
        (int x, int y, (int x, int y) d) current)
    {
        var directions = Directions.AllCardinal.Where(d => d != (current.d) && d != Directions.Reverse(current.d));

        foreach (var d in directions)
        {
            var weight = 0L;
            for (var step = 1; step <= 10; step++)
            {
                var neighbor = (x: current.x + d.x * step, y: current.y + d.y * step);
                if (grid.IsInGrid(neighbor))
                {
                    weight += grid[neighbor.x, neighbor.y];
                    if (step > 3)
                        yield return ((neighbor.x, neighbor.y, d), weight);
                }
                else
                {
                    break;
                }
            }
        }
    }

    private static void PrintGrid(int[,] input, (int x, int y, (int x, int y) d)[] path)
    {
        input.ToConsole((w, c) =>
        {
            if (path.Any(p => p.x == w.x && p.y == w.y))
            {
                ConsoleX.ForegroundColor = ConsoleColor.Red;
                char x;
                var d = path.First(p => p.x == w.x && p.y == w.y).d;
                if (d == Directions.Right)
                    x = '>';
                else if (d == Directions.Left)
                    x = '<';
                else if (d == Directions.Down)
                    x = 'v';
                else if (d == Directions.Up)
                    x = '^';
                else
                    x = '?';
                ConsoleX.Write(x);
            }
            else
            {
                ConsoleX.ForegroundColor = ConsoleColor.White;
                ConsoleX.Write(c);
            }
        });
    }

}
