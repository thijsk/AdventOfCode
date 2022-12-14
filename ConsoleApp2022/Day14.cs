using System.Data;
using Common;

namespace ConsoleApp2022;

public class Day14 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 901;
        PuzzleContext.UseExample = false;

        var map = new Dictionary<Point<int>, char>();

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        foreach (var line in input)
        {
            var start = line.First();
            foreach (var next in line.Skip(1))
            {
                DrawLine(map, start, next);

                start = next;
            }
        }

        var infinite = map.Keys.Max(p => p.y);

        var sand = 0;
        var done = false;
        while (!done)
        {
            var sp = new Point<int>(500, 0);
            var stopfalling = false;
            while (!stopfalling)
            {
                var down = new Point<int>(sp.x, sp.y + 1);
                if (down.y > infinite)
                {
                    done = true;
                    break;
                }
                var downleft = new Point<int>(down.x - 1, down.y);
                var downright = new Point<int>(down.x + 1, down.y);
                if (!map.ContainsKey(down))
                {
                    sp = down;
                }
                else if (!map.ContainsKey(downleft))
                {
                    sp = downleft;
                }
                else if (!map.ContainsKey(downright))
                {
                    sp = downright;
                }
                else
                {
                    if (map.ContainsKey(sp))
                    {
                        ConsoleX.WriteLine();
                    }
                    map.Add(sp, 'O');
                    sand++;
                    stopfalling = true;
                }
            }

            if (PuzzleContext.UseExample)
            {
                ConsoleX.WriteLine($"After sand {sand}");
                PrintMap(map);
            }
        }


        return sand;
    }

    private void PrintMap(Dictionary<Point<int>, char> map)
    {
        var minx = map.Keys.Min(p => p.x);
        var maxx = map.Keys.Max(p => p.x);
        var miny = map.Keys.Min(p => p.y);
        var maxy = map.Keys.Max(p => p.y);

        foreach (var y in miny..maxy)
        {
            foreach (var x in minx..maxx)
            {
                var point = new Point<int>(x, y);
                if (map.ContainsKey(point))
                    ConsoleX.Write(map[point]);
                else ConsoleX.Write(' ');
            }
            ConsoleX.WriteLine();
        }

    }

    private void DrawLine(Dictionary<Point<int>, char> map, Point<int> start, Point<int> end)
    {
        if (start.x == end.x)
        {
            var starty = Math.Min(start.y, end.y);
            var endy = Math.Max(start.y, end.y);
            foreach (var y in (starty..endy))
            {
                map.AddOrSet(new Point<int>(start.x, y), '#');
            }
        }
        else if (start.y == end.y)
        {
            var startx = Math.Min(start.x, end.x);
            var endx = Math.Max(start.x, end.x);
            foreach (var x in startx..endx)
            {
                map.AddOrSet(new Point<int>(x, start.y), '#');
            }
        }
        else
        {
            throw new Exception("Raar");
        }
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 24589;
        PuzzleContext.UseExample = false;

        var map = new Dictionary<Point<int>, char>();

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        foreach (var line in input)
        {
            var start = line.First();
            foreach (var next in line.Skip(1))
            {
                DrawLine(map, start, next);

                start = next;
            }
        }

        var bottom = map.Keys.Max(p => p.y) + 2;

        var sand = 0;
        var done = false;
        while (!done)
        {
            var sp = new Point<int>(500, 0);
            var stopfalling = false;
            while (!stopfalling)
            {
                if (sp.y == bottom - 1)
                {
                    map.Add(sp, 'O');
                    sand++;
                    break;
                }

                var down = new Point<int>(sp.x, sp.y + 1);
                var downleft = new Point<int>(down.x - 1, down.y);
                var downright = new Point<int>(down.x + 1, down.y);
                if (!map.ContainsKey(down))
                {
                    sp = down;
                }
                else if (!map.ContainsKey(downleft))
                {
                    sp = downleft;
                }
                else if (!map.ContainsKey(downright))
                {
                    sp = downright;
                }
                else
                {
                    map.Add(sp, 'O');
                    sand++;
                    stopfalling = true;
                    if (sp.x == 500 && sp.y == 0)
                    {
                        done = true;
                    }
                }
            }

            if (PuzzleContext.UseExample)
            {
                ConsoleX.WriteLine($"After sand {sand}");
                PrintMap(map);
            }
        }
        
        return sand;
    }

    public IEnumerable<Point<int>> Parse(string line)
    {
        var points = line.Split("->");
        return points.Select(point =>
        {
            var (x, y) = point.Trim().Split(',').Select(int.Parse).ToArray();
            return new Point<int>(x, y);
        });
    }

}