using Common;

namespace ConsoleApp2022;

public class Day06 : IDay
{
    public long Part1()
    {
        var input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"; //PuzzleContext.Input.First();
        input = PuzzleContext.Input.First();

        foreach (var index in 3..input.Length)
        {
            var buffer = input.Substring(index - 3, 4);

            var chars = buffer.ToCharArray().GroupBy(c => c).Count();

            if (chars == 4)
            {
                Console.WriteLine(buffer);
                return index+1;
            }
        }

        return 0;
    }

    public long Part2()
    {
        var input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"; //PuzzleContext.Input.First();
        input = PuzzleContext.Input.First();

        const int messageLength = 14;

        foreach (var index in (messageLength-1)..input.Length)
        {
            var buffer = input.Substring(index - (messageLength - 1), messageLength);
            Console.WriteLine(index+1);
            Console.WriteLine(buffer);
            var chars = buffer.ToCharArray().GroupBy(c => c).Count();

            if (chars == messageLength)
            {
                Console.WriteLine(buffer);
                return index + 1;
            }
        }
        return 0;
    }



}