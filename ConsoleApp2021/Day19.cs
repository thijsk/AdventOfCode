using System.Numerics;
using System.Runtime.Intrinsics;
using Common;

namespace ConsoleApp2021;

public class Day19 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Example);

        var point = new Vector3(2, 2, 2);
        var m = Matrix4x4.CreateRotationX(MathF.PI / 2);


        Console.WriteLine(m);

        //Vector<int> v = new(new[] { 2, 2, 2 });

        Console.WriteLine(point);
        Console.WriteLine(point.Length());
        var result = Vector3.Transform(point, m).RoundToInt();
        Console.WriteLine(result);
        Console.WriteLine(result.Length());
        var result1 = Vector3.Transform(result, m).RoundToInt();
        Console.WriteLine(result1);
        Console.WriteLine(result1.Length());
        var result2 = Vector3.Transform(result1, m).RoundToInt();
        Console.WriteLine(result2);
        Console.WriteLine(result2.Length());
        var result3 = Vector3.Transform(result2, m).RoundToInt();
        Console.WriteLine(result3);
        Console.WriteLine(result3.Length());
        var result4 = Vector3.Transform(result3, m).RoundToInt();
        Console.WriteLine(result4);
        Console.WriteLine(result4.Length());

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

public static class Vector3Extensions
{

    public static Vector3 RoundToInt(this Vector3 vector)
    {
        return new Vector3(MathF.Round(vector.X), MathF.Round(vector.Y), MathF.Round(vector.Z));
    }
}