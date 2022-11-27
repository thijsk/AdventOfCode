using Common;

namespace ConsoleApp2021;

public class Day13 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        var instruction = input.instructions.First();

        var result = input.points.Select(point => Fold(point, instruction)).Distinct();

        return result.Count();
    }

    private (int x, int y) Fold((int x, int y) point, (char xy, int value) instruction)
    {
        if (instruction.xy == 'x')
        {
            if (point.x > instruction.value)
            {
                return (point.x - ((point.x - instruction.value) * 2), point.y);
            }
        }
        else
        {
            if (point.y > instruction.value)
            {
                return (point.x, point.y - ((point.y - instruction.value) * 2));
            }
        }

        return point;
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        var result = input.points.ToList();
        foreach (var instruction in input.instructions)
        {
            result = result.Select(point => Fold(point, instruction)).Distinct().ToList();
        }

        var maxx = result.Max(r => r.x);
        var maxy = result.Max(r => r.y);

        for (int y = 0; y <= maxy; y++)
        {
            for (int x = 0; x <= maxx; x++)
            {

                if (result.Contains((x, y)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
        }

        return 0;
    }

    public (List<(int x, int y)> points, List<(char xy, int value)> instructions) Parse(string[] lines)
    {
        bool first = true;
        var points = new List<(int x, int y)>();
        var instructions = new List<(char xy, int value)>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                first = false;
                continue;
            }
            if (first)
            {
                var split = line.Split(',');
                points.Add((int.Parse(split[0]), int.Parse(split[1])));
            }
            else
            {
                var split = line.Split('=');
                instructions.Add((split[0].Last(), int.Parse(split[1])));
            }
        }

        return (points, instructions);
    }

}