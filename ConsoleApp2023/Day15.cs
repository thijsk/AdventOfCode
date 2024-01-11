using Common;

namespace ConsoleApp2023;

public class Day15 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 507666;
        PuzzleContext.UseExample = false;

        var input = Parse1(PuzzleContext.Input);

        var sum = 0L;

        HASH("HASH");

        foreach (var step in input)
        {
            sum += HASH(step);
        }

        return sum;
    }

    private int HASH(string step)
    {
        var hash = 0L;
        foreach (var c in step)
        {
            hash += (int)c;
            hash *= 17;
            hash %= 256;
        }
        return (int)hash;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 233537;
        PuzzleContext.UseExample = false;

        var input = Parse1(PuzzleContext.Input).Select(Parse2).ToArray();

        List<KeyValuePair<string, int>>[] HASHMAP = new List<KeyValuePair<string, int>>[256];

        for (var i = 0; i < 256; i++)
        {
            HASHMAP[i] = new List<KeyValuePair<string, int>>();
        }
        
        foreach (var (label, operand, value) in input)
        {
            var boxId = HASH(label);
            var box = HASHMAP[boxId];

            if (operand == '-')
            {
                var oldIndex = box.IndexOf(box.FirstOrDefault(kv => kv.Key == label));
                if (oldIndex != -1)
                    box.RemoveAt(oldIndex);
            }
            else
            {
                var kv = new KeyValuePair<string, int>(label, value);
                var oldIndex = box.IndexOf(box.FirstOrDefault(kv => kv.Key == label));
                if (oldIndex == -1)
                    box.Add(kv);
                else
                    box[oldIndex] = kv;
            }

            ConsoleX.WriteLine($"BoxId {boxId}:");
            foreach (var kv in box)
            {
                ConsoleX.WriteLine($"{kv.Key} = {kv.Value}");
            }

        }

        long focusingPower = 0L;

        for (int i = 0; i < 256; i++)
        {
            var box = HASHMAP[i];
            for (var li = 0; li < box.Count; li++)
            {
                focusingPower += (i+1) * (li+1) * box[li].Value;
            }
        }
        return focusingPower;
    }

    private string[] Parse1(string[] lines)
    {
        return lines.First().Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    private (string label, char operand, int value) Parse2(string step)
    {
        var operandIndex  = step.IndexOfAny(new[] { '=', '-' });

        var label = step[..operandIndex].Trim();
        var operand = step[operandIndex];
        if (step.Length > operandIndex+1)
        {
            var value = int.Parse(step[(operandIndex + 1)..].Trim());
            return (label, operand, value);
        }
        else
        {
            return (label, operand, 0);
        }
    }


}