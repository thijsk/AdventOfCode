using Common;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp2023;

public class Day12 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 7025;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var sum = 0L;
        foreach(var line in input)
        {
            sum += CalculateOptions(line.springs + '.', line.groupinput.Split<int>(','));
        }

        return sum;
    }
    public long Part2()
    {
        PuzzleContext.Answer2 = 11461095383315;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        long sum = 0L;
        foreach (var line in input)
        {
            var multiplesprings = $"{line.springs}?{line.springs}?{line.springs}?{line.springs}?{line.springs}";
            var multiplegroups = $"{line.groupinput},{line.groupinput},{line.groupinput},{line.groupinput},{line.groupinput}";
            var amount = CalculateOptions(multiplesprings + '.', multiplegroups.Split<int>(','));

            sum += amount;
        }

        return sum;
    }


    private long CountOptions(string lineSprings, string lineGroupinput)
    {
        if (lineSprings.Contains('?'))
        {
            var index = lineSprings.IndexOf('?');

            var sb = new StringBuilder(lineSprings)
            {
                [index] = '.'
            };
            var option1 = sb.ToString();
            var options = CountOptions(option1, lineGroupinput);

            var sb2 = new StringBuilder(lineSprings)
            {
                [index] = '#'
            };
            var option2 = sb2.ToString();

            return options + CountOptions(option2, lineGroupinput);
        }
        
        var isValid = IsValid(lineSprings, lineGroupinput);
        return isValid ? 1 : 0;
    }

    private bool IsValid(string lineSprings, string lineGroupinput)
    {
        var springs = lineSprings.Split(".", StringSplitOptions.RemoveEmptyEntries);
        var springGroups = string.Join(',', springs.Select(s => s.Length));
        return springGroups == lineGroupinput;
    }

    private readonly Dictionary<string, long> cache = new();

    public long CalculateOptions(string springs, int[] groups, int brokenCount = 0)
    {
        //var cacheKey = HashCode.Combine(springs, groups.GetHashCodeOfList(), brokenCount);
        var cacheKey = $"{springs}|{string.Join(',',groups)}|{brokenCount}";
        if (cache.TryGetValue(cacheKey, out var cachedValue))
        {
            return cachedValue;
        }

        if (string.IsNullOrEmpty(springs))
        {
            var result = groups.Length == 0 ? 1 : 0;
            cache[cacheKey] = result;
            return result;
        }
        
        var options = 0l;
        char first = springs[0];

        switch (first)
        {
            case '#':
                options = CalculateOptions(springs[1..], groups, brokenCount + 1);
                break;
            case '.':
                if (brokenCount > 0)
                {
                    if (groups.Length == 0)
                    {
                        options = 0;
                        break;
                    }

                    var firstGroup = groups[0];
                    if (brokenCount == firstGroup)
                    {
                        options = CalculateOptions(springs[1..], groups[1..], 0);
                        break;
                    }

                    options = 0;
                    break;
                }
                options = CalculateOptions(springs[1..], groups);
                break;
            case '?':
                options = CalculateOptions('.' + springs[1..], groups, brokenCount) +
                          CalculateOptions('#' + springs[1..], groups, brokenCount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(first));
        }

        cache.Add(cacheKey, options);
        return options;
    }

    private (string springs, string groupinput) Parse(string line)
    {
        var splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return (splits[0], splits[1]);
    }

}
