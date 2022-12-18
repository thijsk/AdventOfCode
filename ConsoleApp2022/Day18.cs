using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Common;

namespace ConsoleApp2022;

public class Day18 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 4302;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToHashSet();

        var total = 0L;

        foreach (var point in input)
        {
            var exposed = point.GetNeighbors().Count(n => !input.Contains(n));
            total += exposed;
        }


        return total;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToHashSet();

        var minx = input.Min(p => p.x) - 1;
        var miny = input.Min(p => p.y) - 1;
        var minz = input.Min(p => p.z) - 1;
        var maxx = input.Max(p => p.x) + 1;
        var maxy = input.Max(p => p.y) + 1;
        var maxz = input.Max(p => p.z) + 1;

        // flood fill the outside, the world is not big

        var start = new Point3<int>(minx, miny, minz);
        var queue = new Queue<Point3<int>>();
        queue.Enqueue(start);

        var visited = new HashSet<Point3<int>>();
        var total = 0;

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();

            ConsoleX.WriteLine($"{point}");

            var neighbors = point.GetNeighbors().Where(n => (n.x >= minx && n.x <= maxx) && (n.y >= miny && n.y <= maxy) && (n.z >= minz && n.z <= maxz) ).ToList();
            var emptyNeighbors = neighbors.Where(n => !input.Contains(n)).ToList();

            total += neighbors.Count(n => input.Contains(n));

            var notVisited = emptyNeighbors.Where(n => !visited.Contains(n) && !queue.Contains(n)).ToList();

            foreach (var neighbor in notVisited)
            {
                queue.Enqueue(neighbor);
            }

            visited.Add(point);
            
        }
        
        return total;
    }

    public Point3<int> Parse(string line)
    {
        (int x, int y, int z) =  line.Split(',').Select(int.Parse).ToArray();
        return new Point3<int>(x, y, z);
    }

}