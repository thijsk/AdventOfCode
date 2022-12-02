using Common;

namespace ConsoleApp2022;

public class Day03 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        foreach (var i in 1..10)
        {

        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        return 0;
    }

    public long Parse(string line)
    {
        return long.Parse(line);
    }

}