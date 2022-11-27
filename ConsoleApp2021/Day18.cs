using System.Text.Json.Nodes;
using Common;

namespace ConsoleApp2021;

public class Day18 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var first = input.First();
        var result = first;

        foreach (var line in input.Skip(1))
        {
            ConsoleX.WriteLine($"   {result.ToJsonString()}");
            ConsoleX.WriteLine($" + {line.ToJsonString()}");
            result = Add(result, line);
            ConsoleX.WriteLine($" = {result.ToJsonString()}");
            ConsoleX.WriteLine();
        }

        return result.Magnitude();
    }

    private JsonArray Add(JsonArray first, JsonArray second)
    {
        //Reduce(first);
        //Reduce(second);
        var result = new JsonArray(new[] { first, second });
        Reduce(result);
        return result;
    }

    private void Reduce(JsonArray n)
    {
        bool action = true;
        while (action)
        {
            action = n.Explode();
            if (!action)
            {
               action = n.Split();
            }
        }
    }

    public long Part2()
    {
        var input = PuzzleContext.Input;
        var permutations = input.GetPermutations(2);

        long max = 0;
        foreach (var p in permutations)
        {
            var first = Parse(p.First());
            var second = Parse(p.Last());
            var magnitude = Add(first, second).Magnitude();
            if (magnitude > max)
            {
                max = magnitude;
            }
        }

        return max;
    }

    public JsonArray Parse(string line)
    {
        return (JsonArray)JsonNode.Parse(line);
    }

}

public static class JsonExtensions
{
    public static int Depth(this JsonNode jn)
    {
        if (jn.Parent == null)
        {
            return 1;
        }

        return jn.Parent.Depth() + 1;
    }

    public static void AddToTheRight(this JsonArray ja, JsonArray child, int value)
    {
        var childIndex = ja.IndexOf(child);
        switch (childIndex)
        {
            case 0:
            {
                if (ja[1] is JsonArray rightArray)
                {
                    rightArray.AddToTheRight(null, value);
                }
                else
                {
                    ja[1] = (int)ja[1] + value;
                }

                break;
            }
            case 1:
            {
                var parent = ((JsonArray)ja.Parent);
                parent?.AddToTheRight(ja, value);
                break;
            }
            default:
            {
                if (ja[0] is JsonArray rightChild)
                {
                    rightChild.AddToTheRight(null, value);
                }
                else
                {
                    ja[0] = (int)ja[0] + value;
                }

                break;
            }
        }
    }

    public static void AddToTheLeft(this JsonArray ja, JsonArray child, int value)
    {
        var childIndex = ja.IndexOf(child);
        if (childIndex == 1)
        {
            if (ja[0] is JsonArray leftArray)
            {
                leftArray.AddToTheLeft(null, value);
            }
            else
            {
                ja[0] = (int)ja[0] + value;
            }
        }
        else if (childIndex == 0)
        {
            var parent = ((JsonArray)ja.Parent);
            parent?.AddToTheLeft(ja, value);
        }
        else
        {
            if (ja[1] is JsonArray leftChild)
            {
                leftChild.AddToTheLeft(null, value);
            }
            else
            {
                ja[1] = (int)ja[1] + value;
            }
        }
    }

    public static bool Explode(this JsonArray ja)
    {
        if (ja == null)
            return false;

        var depth = ja.Depth();
        if (depth > 4)
        {
            var parent = (JsonArray)ja.Parent;

            parent.AddToTheRight(ja, (int)ja[1]);
            parent.AddToTheLeft(ja, (int)ja[0]);

            var myindex = parent.IndexOf(ja);

            parent[myindex] = 0;

            return true;
        }

        return (ja[0] as JsonArray).Explode() || (ja[1] as JsonArray).Explode();
    }

    public static bool Split(this JsonArray ja)
    {
        return ja[0].Split(ja) || ja[1].Split(ja);
    }

    private static bool Split(this JsonNode jn, JsonArray parent)
    {
        if (jn is JsonArray array)
        {
            return array.Split();
        }
        var splitResult = Split((int)jn);
        if (splitResult != null)
        {
            parent[parent.IndexOf(jn)] = splitResult;
            return true;
        }
        return false;
    }

    private static JsonArray Split(int value)
    {
        if (value >= 10)
        {
            int left = (int)Math.Floor(value / (double)2);
            int right = (int)Math.Ceiling(value /  (double)2);
            var result = new JsonArray();
            result.Add(left);
            result.Add(right);
            return result;
        }
        return null;
    }

    public static long Magnitude(this JsonArray ja)
    {
        return (3 * ja[0].Magnitude()) + (2 * ja[1].Magnitude());
    }

    private static long Magnitude(this JsonNode jn)
    {
        if (jn is JsonArray array)
        {
            return array.Magnitude();
        }
        return (int)jn;
    }
}
