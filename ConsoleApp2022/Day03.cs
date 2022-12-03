using Common;

namespace ConsoleApp2022;

public class Day03 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var sum = 0L;
        foreach (var (first, second) in input)
        {
            var overlap = first.Intersect(second).Single();
            Console.WriteLine((int) overlap);
            if (overlap is >= 'a' and <= 'z')
            {
                sum += (int) overlap - 96;

            }

            if (overlap is >= 'A' and <= 'Z')
            {
                sum += (int) overlap - 38;
            }
        }




        return sum;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var groups = input.Chunk(3);
        var sum = 0L;
        foreach (var group in groups)
        {
            var one = group[0].first.Concat(group[0].second).ToArray();
            var two = group[1].first.Concat(group[1].second).ToArray();
            var three = group[2].first.Concat(group[2].second).ToArray();

            var badge = one.Intersect(two).Intersect(three).Single();
            Console.WriteLine(badge);

            if (badge is >= 'a' and <= 'z')
            {
                sum += (int)badge - 96;

            }

            if (badge is >= 'A' and <= 'Z')
            {
                sum += (int)badge - 38;
            }
        }
        
        return sum;
    }

    public (char[] first, char[] second) Parse(string line)
    {
        var items = line.ToCharArray();
        var count = items.Length / 2;
        var first = items.Take(count).ToArray();
        var second = items.Skip(count).Take(count).ToArray();
        return (first, second);
    }
}