using Common;

namespace ConsoleApp2022;

public class Day25 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 36692419278947;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();
        var sum = input.Sum();
        ConsoleX.WriteLine(ToSnafu(sum), ConsoleColor.Green);

        return sum;
    }

    private static string ToSnafu(long decimalnumber)
    {
        var snafu = string.Empty;

        do
        {
            var digit = (decimalnumber % 5);
            var snafudigit = digit switch
            {
                0 => '0',
                1 => '1',
                2 => '2',
                3 => '=',
                4 => '-',

            };
            decimalnumber -= digit;
            if (digit > 2)
            {
                decimalnumber += 5;
            }

            decimalnumber /= 5;

            snafu += snafudigit;
        } while (decimalnumber > 0);

        return snafu.Backwards();
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        return 0;
    }

    public long Parse(string line)
    {
        var result = 0L;
        foreach (var digit in line)
        {
            result *= 5;
            var snafu = digit switch
            {
                '=' => -2,
                '-' => -1,
                '0' => 0,
                '1' => 1,
                '2' => 2,
            };

            result += snafu;
        }

        return result;
    }

}