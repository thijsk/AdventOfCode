using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Common;

namespace ConsoleApp2021;

public class Day22 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var result = new Dictionary<(int x, int y, int z), bool>();

        foreach (var cuboids in input)
        {
            foreach (var cube in cuboids.cuboids)
            {
                if (cuboids.state)
                {
                    result.AddOrSet(cube, cuboids.state);
                }
                else
                {
                   result.Remove(cube);
                }
            }
        }

        Console.WriteLine(result.Keys.Count);
        return result.Keys.Count(k => result[k]);
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse2).ToArray();

        var xIntervals = GetIntervals(input, x => new [] { x.xs, x.xe+1 });
        var yIntervals = GetIntervals(input, y => new [] { y.ys, y.ye+1 });
        var zIntervals = GetIntervals(input, z => new [] { z.zs, z.ze+1 });

        var result = new HashSet<(int x, int y, int z)>();

        long line = 0;
        foreach (var (state, cuboids) in input)
        {
            line++;
            Console.Write($"{line} {state} ");
            var cx = ToIntervals(xIntervals, cuboids.xs, cuboids.xe);
            var cy = ToIntervals(yIntervals, cuboids.ys, cuboids.ye);
            var cz = ToIntervals(zIntervals, cuboids.zs, cuboids.ze);

            Console.Write(":");

            foreach (var ix in cx)
            {
                foreach (var iy in cy)
                {
                    foreach (var iz in cz)
                    {
                        if (state)
                        {
                            result.Add((ix, iy, iz));
                        }
                        else
                        {
                            result.Remove((ix, iy, iz));
                        }
                    }
                }
            }

            Console.WriteLine(result.Count);
        }

        return result.Sum(v =>
        {
            var x = xIntervals[v.x];
            var y = yIntervals[v.y];
            var z = zIntervals[v.z];
            return (long)(1 + x.e - x.s) * (1+ y.e - y.s) * (1 + z.e - z.s);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<int> ToIntervals(ImmutableList<(int s, int e)> intervals, int cuboidsXs, int cuboidsXe)
    {
        return intervals
            .Where(i => i.s.Between(cuboidsXs, cuboidsXe) || i.e.Between(cuboidsXs, cuboidsXe))
            .Select(i => intervals.IndexOf(i)).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ImmutableList<(int s,int e)> GetIntervals((bool state, (int xs, int xe, int ys, int ye, int zs, int ze) cuboids)[] input,
        Func<(int xs, int xe, int ys, int ye, int zs, int ze), IEnumerable<int>> selector)
    {
        return input
            .Select(i => i.cuboids)
            .SelectMany(selector)
            .Distinct()
            .OrderBy(x => x)
            .SlidingWindow(int.MinValue)
            .Skip(1)
            .Select(window => (window.PrevItem, window.CurrentItem - 1)).ToImmutableList();

    }

    public (bool state, (int x, int y, int z)[] cuboids) Parse(string line)
    {
        //on x = 10..10, y = 10..10, z = 10..10
        var splitstatevalues = line.Split(' ');
        var state = splitstatevalues[0] == "on";

        var xyz = splitstatevalues[1].Split(',');

        var xs = xyz[0];
        var ys = xyz[1];
        var zs = xyz[2];

        var cuboids = new List<(int x, int y, int z)>();

        var xsplit = xs.Replace("x=","").Split("..").Select(int.Parse).ToArray();
        var ysplit = ys.Replace("y=", "").Split("..").Select(int.Parse).ToArray();
        var zsplit = zs.Replace("z=", "").Split("..").Select(int.Parse).ToArray();

        for (var x = Math.Max(xsplit[0], -50); x <= Math.Min(xsplit[1], 50); x++)
        {
            for (var y = Math.Max(ysplit[0], -50); y <= Math.Min(ysplit[1], 50); y++)
            {
                for (var z = Math.Max(zsplit[0], -50); z <= Math.Min(zsplit[1], 50); z++)
                {
                    cuboids.Add((x, y, z));
                }
            }
        }
        return (state, cuboids.ToArray());
    }

    public (bool state, (int xs, int xe, int ys, int ye, int zs, int ze) cuboids) Parse2(string line)
    {
        //on x = 10..10, y = 10..10, z = 10..10
        var splitstatevalues = line.Split(' ');
        var state = splitstatevalues[0] == "on";

        var xyz = splitstatevalues[1].Split(',');

        var xs = xyz[0];
        var ys = xyz[1];
        var zs = xyz[2];

        var xsplit = xs.Replace("x=", "").Split("..").Select(int.Parse).ToArray();
        var ysplit = ys.Replace("y=", "").Split("..").Select(int.Parse).ToArray();
        var zsplit = zs.Replace("z=", "").Split("..").Select(int.Parse).ToArray();

        (int xs, int xe, int ys, int ye, int zs, int ze) cuboids =
            (xsplit.Min(), xsplit.Max(), ysplit.Min(), ysplit.Max(), zsplit.Min(), zsplit.Max());
        return (state, cuboids);
    }

}