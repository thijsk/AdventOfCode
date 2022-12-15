using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Common;

namespace ConsoleApp2022;

public class Day15 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 5240818;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToList();

        var minx = input.Min(i => i.sensor.x - i.distance);
        var maxx = input.Max(i => i.sensor.x + i.distance);

        var y = 2000000;
        var counter = 0;
        for (int x = minx; x < maxx; x++)
        {
            var point = new Point<int>(x, y);
            var something = false;
            foreach (var i in input)
            {
                if (point == i.sensor)
                {
                    ConsoleX.Write('S');
                    something = true;
                    break;
                }
                if (point == i.beacon)
                {
                    ConsoleX.Write('B');
                    something = true;
                    break;
                }
                var distance = point.ManhattanDistanceTo(i.sensor);
                if (distance <= i.distance)
                {
                    ConsoleX.Write('#');
                    something = true;
                    counter++;
                    break;
                }
            }
            if (!something)
            {
                ConsoleX.Write('.');
            }
        }

        return counter;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 13213086906101;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToList();

        var minx = 0;
        var maxx = 4000000;
        var miny = 0;
        var maxy = 4000000;

        Point<int> result = new();

        Parallel.ForEach(Partitioner.Create(miny, maxy+1), (range, state) =>
        {
            var found = false;
            var (miny, maxy) = range;
            Console.WriteLine($"Range {miny} {maxy}");
            for (var y = miny; y < maxy; y++)
            {
                var xRanges = input.Select(i => GetXRange(i, y))
                    .Where(r => r.minx <= r.maxx).ToList();

                var x = 0;
                while (x <= maxx)
                {
                    var inRanges = xRanges.Where(r => r.minx <= x && x <= r.maxx).ToList();
                    if (!inRanges.Any())
                    {
                        result = (x, y);
                        found = true;
                        state.Stop();
                        break;
                    }

                    x = inRanges.Max(r => r.maxx) + 1;
                }

                if (found)
                {
                    break;
                }
            }
        });


        Console.WriteLine($"{result}");
        return (result.x * 4000000L) + result.y;
    }
    
    private static (int minx, int maxx) GetXRange(((int x, int y) sensor, (int x, int y) beacon, int distance) i, int y)
    {
        var ydistance = Math.Abs(y - i.sensor.y);
        var minx = i.sensor.x - (i.distance - ydistance);
        var maxx = i.sensor.x + (i.distance - ydistance);
        return (minx, maxx);
    }

    public ((int x, int y) sensor, (int x, int y) beacon, int distance) Parse(string line)
    {
        var (sensorstr, beaconstr) = line.Split(':');

        var (sensorxstr, sensorystr) = sensorstr.Split(',');
        var (beaconxstr, beaconystr) = beaconstr.Split(",");
        var sensorx = int.Parse(sensorxstr.Split('=')[1]);
        var sensory = int.Parse(sensorystr.Split('=')[1]);
        var beaconx = int.Parse(beaconxstr.Split('=')[1]);
        var beacony = int.Parse(beaconystr.Split('=')[1]);

        var distance = ((Point<int>)(sensorx, sensory)).ManhattanDistanceTo((beaconx, beacony));
        return ((sensorx, sensory), (beaconx, beacony), distance);
    }

}