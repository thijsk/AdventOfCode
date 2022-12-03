using Common;
using System;

namespace ConsoleApp2022;

public class Day03 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse1);

        var sum = 0L;
        foreach (var (first, second) in input)
        {
            var overlap = first.Intersect(second).Single();
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
        var input = PuzzleContext.Input.Select(Parse2);

        var groups = input.Chunk(3).ToArray();
        var sum = 0L;
        foreach ( var (one, two, three) in groups)
        {
            var badge = one.Intersect(two).Intersect(three).Single();

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

    public (char[] first, char[] second) Parse1(string line)
    {
        var items = line.ToCharArray();
        var count = items.Length / 2;
        var first = items.Take(count).ToArray();
        var second = items.Skip(count).Take(count).ToArray();
        return (first, second);
    }

    public char[] Parse2(string line)
    {
        var items = line.ToCharArray();
       return items;
    }
}