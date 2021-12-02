using Common;

namespace ConsoleApp2021;

public class Day01 : IDay
{
    public long Part1()
    {
        var input = File.ReadAllLines("Day01.txt").Select(Int32.Parse).ToArray();

        bool first = true;
        var prevValue = 0;
        var increase = 0;
        foreach (var value in input)
        {
            if (first)
            {

                first = false;
            }
            else
            {
                if (value > prevValue)
                {
                    increase++;
                }
            }

            prevValue = value;
        }

        return increase;
    }

    public long Part2()
    {
        var input = File.ReadAllLines("Day01.txt").Select(Int32.Parse).ToArray();
        bool first = true;
        var prevValue = 0;
        var increase = 0;

        foreach (var (v1, v2, v3) in input.SlidingWindow().Skip(1).SkipLast(1))
        {
            var value = v1 + v2 + v3;
            Console.WriteLine(value);
            if (first)
            {
                first = false;
            }
            else
            {
                if (value > prevValue)
                {
                    increase++;
                }
            }

            prevValue = value;
        }

        return increase;
    }
}