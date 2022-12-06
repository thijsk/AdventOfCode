using Common;

namespace ConsoleApp2022;

public class Day06 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.First();

        foreach (var index in 4..input.Length)
        {
            if (input[(index - 4)..index].Distinct().Count() == 4)
            {
                return index;
            }
        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.First();
        
        const int messageLength = 14;

        foreach (var index in messageLength..input.Length)
        {
            if (input[(index - messageLength)..index].Distinct().Count() == messageLength)
            {
                return index;
            }
        }
        return 0;
    }
}