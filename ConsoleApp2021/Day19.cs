using System.Numerics;
using Common;

namespace ConsoleApp2021;

public class Day19 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Example);

        var point = new Vector3(2, 2, 2);
        Console.WriteLine(point);
        var q = new Quaternion(1, 0, 0, 0);
        var result = Vector3.Transform(point, q);
        Console.WriteLine(result);
        var result1 = Vector3.Transform(result, q);
        Console.WriteLine(result1);
        var result2 = Vector3.Transform(result1, q);
        Console.WriteLine(result2);
        var result3 = Vector3.Transform(result2, q);
        Console.WriteLine(result3);
        var result4 = Vector3.Transform(result3, q);
        Console.WriteLine(result4);

        return 0;
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Example);

        return 0;
    }

    public List<List<Vector3>> Parse(string[] lines)
    {
        var result = new List<List<Vector3>>();
        List<Vector3> scanner = null;
        foreach (var line in lines)
        {
            if (line.StartsWith("--"))
            {
                scanner = new List<Vector3>();
                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                result.Add(scanner);
                continue;
            }

            var split = line.Split(',').Select(int.Parse).ToArray();
            var point = new Vector3(split[0], split[1], split[2]);
            scanner.Add(point);
        }
        result.Add(scanner);

        return result;
    }
}