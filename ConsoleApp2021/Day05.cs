using System.Linq;
using Common;

namespace ConsoleApp2021;

public class Day05 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var horver = input.Where(l => l.IsHorizontal() || l.IsVertical()).ToArray();

        var points = horver.SelectMany(LineToPoints);

        return points.GroupBy(p => p).Count(g => g.Count() > 1);

    }

    private IEnumerable<Point<int>> LineToPoints(Line<int> line)
    {
        if (line.start.x == line.end.x)
        {
            var minmax = new int[] { line.start.y, line.end.y }.OrderBy(p => p).ToArray();
            return Enumerable.Range(minmax[0], (minmax[1] - minmax[0])+1).Select(y => new Point<int>(line.start.x, y));
        }
        else if (line.start.y == line.end.y)
        {
            var minmax = new int[] { line.start.x, line.end.x }.OrderBy(p => p).ToArray();
            return Enumerable.Range(minmax[0], (minmax[1] - minmax[0])+1).Select(x => new Point<int>(x, line.start.y));
        }
        else
        {
            var xIter = line.start.x < line.end.x ? 1 : -1;
            var yIter = line.start.y < line.end.y ? 1 : -1;

            var result = new List<Point<int>>();
            var y = line.start.y;
            var x = line.start.x;
            for (; x != (line.end.x + xIter); x += xIter, y+= yIter)
            {
                result.Add(new Point<int>(x, y));
            }
            return result;
        }
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var points = input.SelectMany(LineToPoints);

        return points.GroupBy(p => p).Where(g => g.Count() > 1).Count();
    }

    public Line<int> Parse(string line)
    {
        var points = line.Split(" -> ");
        var p1 = points[0].Split(',').Select(int.Parse).ToArray();
        var p2 = points[1].Split(',').Select(int.Parse).ToArray();
        var l = new Line<int>(new Point<int>(p1[0], p1[1]), new Point<int>(p2[0], p2[1]));
        return l;
    }

}