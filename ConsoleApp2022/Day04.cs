using Common;

namespace ConsoleApp2022;

public class Day04 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var count = 0L;

        foreach (var (first, second) in input)
        {
            if (first.Contains(second) || second.Contains(first))
            {
                count++;
            }
        }

        return count;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var count = 0L;

        foreach (var (first, second) in input)
        {
            if (first.Overlaps(second) || second.Overlaps(first))
            {
                count++;
            }
        }

        return count;
    }

    public  (Range first, Range second) Parse(string line)
    {
        var ranges = line.Split(',').Select(i =>
        {
            var items = i.Split('-').Select(j => int.Parse(j)).ToArray();
            return new Range(items[0], items[1]);
        }).ToArray();

        return (ranges[0], ranges[1]);
    }

}