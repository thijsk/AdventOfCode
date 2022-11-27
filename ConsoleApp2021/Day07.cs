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

        var median = (int)input.Median();
        var answer1 = input.Sum(i => Math.Abs(i - median));

        return answer;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.SelectMany(Parse).ToArray();

        var min = input.Min();
        var max = input.Max();

        var answer = Enumerable.Range(min, max - min)
            .Select(height => (height, fuel: input.Sum(i => Triangle(Math.Abs(i - height))))).Min(t => t.fuel);

        return answer;
    }

    private int Triangle(int n)
    {
        if (n == 0) return 0;
        //return Enumerable.Range(1, n).Sum();

        return n * (n + 1) / 2;
    }

    public IEnumerable<int> Parse(string line)
    {
        return line.Split(',').Select(int.Parse);
    }

}