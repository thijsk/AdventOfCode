using Common;

namespace ConsoleApp2022;

public class Day06 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.First();

        foreach (var index in 3..input.Length)
        {
            if (input.Skip(index - 3).Take(4).GroupBy(c => c).Count() == 4)
            {
                return index+1;
            }
        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.First();
        
        const int messageLength = 14;

        foreach (var index in (messageLength-1)..input.Length)
        {
            if (input.Skip(index - (messageLength - 1)).Take(messageLength).GroupBy(c => c).Count() == messageLength)
            {
                return index + 1;
            }
        }
        return 0;
    }



}