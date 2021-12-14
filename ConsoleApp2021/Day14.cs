using System.Reflection;
using System.Text;
using Common;

namespace ConsoleApp2021;

public class Day14 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        var polymer = input.start;
        var resultPolymer = new StringBuilder();
        for (var step = 1; step <= 10; step++)
        {
            var first = polymer[0];
            resultPolymer.Append(first);
            for (int c = 1; c < polymer.Length; c++)
            {
                var second = polymer[c];
                var key = $"{first}{second}";
                if (input.rules.ContainsKey(key))
                {
                    resultPolymer.Append(input.rules[key]);
                }
                else
                {
                    Console.WriteLine("nothing");
                }

                resultPolymer.Append(second);
                first = second;
            }

            polymer = resultPolymer.ToString();
            //Console.WriteLine($"step {step} : {polymer}");
            resultPolymer.Clear();
        }

        var oc = polymer.GroupBy(c => c).Select(g => (g.Key, g.Count()));

        var min = oc.Min(o => o.Item2);
        var max = oc.Max(o => o.Item2);

        return max - min;
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        var polymer = input.start;
        var buckets = new Dictionary<string, long>();

        for (int c = 1; c < polymer.Length; c++)
        {
            var pair = new String(new[] { polymer[c - 1], polymer[c] });
            buckets.AddOrIncrement(pair, 1);
        }

        for (var step = 1; step <= 40; step++)
        {
            var resultBuckets = new Dictionary<string, long>();
            foreach (var key in buckets.Keys)
            {
                var amount = buckets[key];
                if (input.rules.ContainsKey(key))
                {
                    var insert = input.rules[key];

                    var firstpair = new string(new[] { key[0], insert[0] });
                    resultBuckets.AddOrIncrement(firstpair, amount);

                    var secondpair =new string(new[] { insert[0], key[1] });
                    resultBuckets.AddOrIncrement(secondpair, amount);
                }
            }

            buckets = resultBuckets;
        }

        var letters = new Dictionary<char, long>();
        foreach (var pair in buckets)
        {
            var firstLetter = pair.Key[0];
            var value = pair.Value;
            letters.AddOrIncrement(firstLetter, value);
        }
        letters[input.start.Last()] += 1;

        var min = letters.Values.Min();
        var max = letters.Values.Max();

        return max - min;
    }

    public (string start, Dictionary<string, string> rules) Parse(string[] lines)
    {
        var start = string.Empty;
        var rules = new Dictionary<string, string>();
        bool first = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                first = false;
                continue;
            }

            if (first)
            {
                start = line;
            }
            else
            {
                var split = line.Split(" -> ");
                rules.Add(split[0], split[1]);
            }
        }

        return (start, rules);
    }
}