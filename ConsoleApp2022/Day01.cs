using Common;

namespace ConsoleApp2022;

public class Day01 : IDay
{
    public long Part1()
    {
        
        var input = PuzzleContext.Input.ToArray();

        var sums = Sums(input);

        return sums.Max();
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.ToArray();
        var sums = Sums(input);
        return sums.OrderByDescending(v => v).Take(3).Sum();
    }

    private static List<long> Sums(string[] input)
    {
        var sum = 0l;
        var max = new List<long>();
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                max.Add(sum);
                sum = 0;
                continue;
            }

            sum += long.Parse(line);
        }

        return max;
    }




}