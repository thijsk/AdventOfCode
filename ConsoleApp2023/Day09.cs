using Common;

namespace ConsoleApp2023;

public class Day09 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 2008960228;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var sum = 0L;

        foreach (var inp in input)
        {
            List<List<long>> history = new() { inp };

            while (history.Last().Any(i => i != 0))
            {
                var next = history.Last().Zip(history.Last().Skip(1)).Select(z => z.Second - z.First).ToList();
                history.Add(next);
            }

            long last = history.Reverse<List<long>>()
                .Aggregate<List<long>, long>(0, (current, sequence) => sequence.Last() + current);

            sum += last;
        }

        return sum;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 1097;
        PuzzleContext.UseExample = false;


        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var sum = 0L;

        foreach (var inp in input)
        {
            List<List<long>> history = new() { inp };

            while (history.Last().Any(i => i != 0))
            {
                var next = history.Last().Zip(history.Last().Skip(1)).Select(z => z.Second - z.First).ToList();
                history.Add(next);
            }

            var first = history.Reverse<List<long>>()
                .Aggregate<List<long>, long>(0, (current, sequence) => sequence.First() - current);

            sum += first;

        }

        return sum;
    }

    private List<long> Parse(string line)
    {
        return line.Split<long>(' ').ToList();
    }

}