using Common;
using System.Linq;

namespace ConsoleApp2023;

public class Day16 : IDay
{
    private (int x, int y) left = Directions.Left;
    private (int x, int y) right = Directions.Right;
    private (int x, int y) up = Directions.Up;
    private (int x, int y) down = Directions.Down;

    public long Part1()
    {
        PuzzleContext.Answer1 = 7472;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid();

        return CountEnergized(input, (0, 0), right);
    }

    private long CountEnergized(char[,] input, (int,int) startlocation, (int,int) startdirection)
    {
        var visited = new HashSet<((int x, int y) location, (int x, int y) direction)>();
        var queue = new Queue<((int x, int y) location, (int x, int y) direction)>();

        queue.Enqueue((startlocation, startdirection));

        while (queue.TryDequeue(out var work))
        {
            if (work.location.x < 0 || work.location.x >= input.GetLength(0))
                continue;
            if (work.location.y < 0 || work.location.y >= input.GetLength(1))
                continue;

            if (visited.Contains(work))
                continue;

            visited.Add(work);

            var c = input[work.location.x, work.location.y];

            ConsoleX.WriteLine(
                $"Location: {work.location.x},{work.location.y} Direction: {work.direction.x},{work.direction.y} Char: {c}");

            switch (c)
            {
                case '.':
                {
                    var newlocation = ((work.location.x + work.direction.x, work.location.y + work.direction.y));
                    queue.Enqueue((newlocation, work.direction));
                    break;
                }
                case '|':
                {
                    if (work.direction == up || work.direction == down)
                    {
                        var newlocation = (work.location.x + work.direction.x, work.location.y + work.direction.y);
                        queue.Enqueue((newlocation, work.direction));
                    }
                    else
                    {
                        var lup = (work.location.x + up.x, work.location.y + up.y);
                        queue.Enqueue((lup, up));
                        var ldown = (work.location.x + down.x, work.location.y + down.y);
                        queue.Enqueue((ldown, down));
                    }

                    break;
                }
                case '-':
                {
                    if (work.direction == left || work.direction == right)
                    {
                        var newlocation = (work.location.x + work.direction.x, work.location.y + work.direction.y);
                        queue.Enqueue((newlocation, work.direction));
                    }
                    else
                    {
                        var lleft = (work.location.x + left.x, work.location.y + left.y);
                        queue.Enqueue((lleft, left));
                        var lright = (work.location.x + right.x, work.location.y + right.y);
                        queue.Enqueue((lright, right));
                    }

                    break;
                }
                case '/':
                {
                    if (work.direction == up)
                    {
                        var lright = (work.location.x + right.x, work.location.y + right.y);
                        queue.Enqueue((lright, right));
                    }
                    else if (work.direction == down)
                    {
                        var lleft = (work.location.x + left.x, work.location.y + left.y);
                        queue.Enqueue((lleft, left));
                    }
                    else if (work.direction == right)
                    {
                        var lup = (work.location.x + up.x, work.location.y + up.y);
                        queue.Enqueue((lup, up));
                    }
                    else if (work.direction == left)
                    {
                        var ldown = (work.location.x + down.x, work.location.y + down.y);
                        queue.Enqueue((ldown, down));
                    }

                    break;
                }
                case '\\':
                {
                    if (work.direction == up)
                    {
                        var lleft = (work.location.x + left.x, work.location.y + left.y);
                        queue.Enqueue((lleft, left));
                    }
                    else if (work.direction == down)
                    {
                        var lright = (work.location.x + right.x, work.location.y + right.y);
                        queue.Enqueue((lright, right));
                    }
                    else if (work.direction == right)
                    {
                        var ldown = (work.location.x + down.x, work.location.y + down.y);
                        queue.Enqueue((ldown, down));
                    }
                    else if (work.direction == left)
                    {
                        var lup = (work.location.x + up.x, work.location.y + up.y);
                        queue.Enqueue((lup, up));
                    }

                    break;
                }
                default:
                    throw new Exception($"Unknown char {c}");
            }
        }

        return visited.DistinctBy(x => x.location).Count();
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 7716;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid();

        var starttop = input.GetRowIndexes(0).Select(l => (location: l, direction: down));
        var startbottom = input.GetRowIndexes(input.GetLength(0) - 1).Select(l => (location: l, direction: up));
        var startleft = input.GetColumnIndexes(0).Select(l => (location: l, direction: right));
        var startright = input.GetColumnIndexes(input.GetLength(1) - 1).Select(l => (location: l, direction: left));

        return starttop.Concat(startbottom).Concat(startleft).Concat(startright).Max(ld => CountEnergized(input, ld.location, ld.direction));
    }

}