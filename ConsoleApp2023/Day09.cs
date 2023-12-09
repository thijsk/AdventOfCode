using Common;

namespace ConsoleApp2023;

public class Day09 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 2008960228;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var result = new List<List<List<long>>>();

        foreach (var inp in input)
        {
            var history = new List<List<long>>();
            history.Add(inp);

            while (history.Last().Any(i => i != 0))
            {
                var next = history.Last().Zip(history.Last().Skip(1)).Select(z => z.Second - z.First).ToList();
                history.Add(next);
            }

            long last = 0;
            foreach (var sequence in history.Reverse<List<long>>())
            {
                sequence.Add(sequence.Last() + last);
                last = sequence.Last();
            }

            result.Add(history);
        }

        return result.Sum(r => r.First().Last());
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 1097;
        PuzzleContext.UseExample = false;


        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var result = new List<List<List<long>>>();

        foreach (var inp in input)
        {
            var history = new List<List<long>>();
            history.Add(inp);

            while (history.Last().Any(i => i != 0))
            {
                var next = history.Last().Zip(history.Last().Skip(1)).Select(z => z.Second - z.First).ToList();
                history.Add(next);
            }

            long first = 0;
            foreach (var sequence in history.Reverse<List<long>>())
            {
                sequence.Insert(0, sequence.First() - first);
                first = sequence.First();
            }

            result.Add(history);
        }

        return result.Sum(r => r.First().First());
    }

    private List<long> Parse(string line)
    {
        return line.Split<long>(' ').ToList();
    }

}