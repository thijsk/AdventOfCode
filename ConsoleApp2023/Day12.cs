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
            sum += CountOptions(line.springs, line.groupinput);
        }

        return sum;
    }
    public long Part2()
    {
        PuzzleContext.Answer2 = 7025;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        long sum = 0L;
        foreach (var line in input)
        {
            var multiplesprings = $"{line.springs}?{line.springs}?{line.springs}?{line.springs}?{line.springs}";
            var multiplegroups = $"{line.groupinput},{line.groupinput},{line.groupinput},{line.groupinput},{line.groupinput}";
            var amount = CalculateOptions(multiplesprings + '.', multiplegroups);

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

    public long CalculateOptions(string springs, string groups, int brokenCount = 0)
    {
        var cacheKey = $"{springs}|{groups}|{brokenCount}";
        if (cache.ContainsKey(cacheKey))
        {
            return cache[cacheKey];
        }

        if (string.IsNullOrEmpty(springs))
        {
            var result = string.IsNullOrEmpty(groups) ? 1 : 0;
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
                    if (string.IsNullOrEmpty(groups))
                    {
                        options = 0;
                        break;
                    }

                    var intGroups = groups.Split<int>(',');
                    var firstGroup = intGroups[0];
                    if (brokenCount == firstGroup)
                    {
                        options = CalculateOptions(springs[1..], string.Join(',', intGroups[1..]), 0);
                        break;
                    } else
                    {
                        options = 0;
                        break;
                    }

                    if (springs[1..].Count(c => c is '#' or '?') < (firstGroup - brokenCount))
                    {
                        options = 0;
                        break;
                    }
                }
                options = CalculateOptions(springs[1..], groups);
                break;
            case '?':
                options = CalculateOptions('.' + springs[1..], groups, brokenCount) +
                          CalculateOptions(springs[1..], groups, brokenCount + 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(first));
        }

        cache.Add(cacheKey, options);
        //if (options > 0)
        //    Console.WriteLine($"{cacheKey} = {options}");

        //string validationSprings = new string('#', brokenCount) + springs;
        //var validation = CountOptions(validationSprings, groups);
        //if (options != validation)
        //{
        //    Debugger.Break();
        //}
        
        return options;
    }

    private (string springs, string groupinput) Parse(string line)
    {
        var splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return (splits[0], splits[1]);
    }

}
