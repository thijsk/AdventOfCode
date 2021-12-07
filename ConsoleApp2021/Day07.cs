using Common;

namespace ConsoleApp2021;

public class Day07 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.SelectMany(Parse).ToArray();

        var min = input.Min();
        var max = input.Max();

       //var result = new List<(int, int)>();

       // for (var height = min; height< max; height++)
       // {
       //     var fuel = input.Sum(i => Math.Abs(i - height));

       //     result.Add((height, fuel));
       //    // Console.WriteLine($"{height} {fuel}");
       // }

        var answer = Enumerable.Range(min, max - min)
            .Select(height => (height, fuel: input.Sum(i => Math.Abs(i - height)))).Min(t => t.fuel);

        return answer;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.SelectMany(Parse).ToArray();

        var min = input.Min();
        var max = input.Max();

        var answer = Enumerable.Range(min, max - min)
            .Select(height => (height, fuel: input.Sum(i => Calc(Math.Abs(i - height))))).Min(t => t.fuel);

        return answer;
    }

    private int Calc(int abs)
    {
        if (abs == 0) return 0;
        return Enumerable.Range(1, abs).Sum();
    }

    public IEnumerable<int> Parse(string line)
    {
        return line.Split(',').Select(int.Parse);
    }

}