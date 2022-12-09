using Common;

namespace ConsoleApp2022;

public class Day10 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        return 0;
    }

    public long Part2()
    {
        PuzzleContext.Answer1 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        return 0;
    }

    public long Parse(string line)
    {
        return long.Parse(line);
    }

}