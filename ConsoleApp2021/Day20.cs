using System.Text;
using Common;

namespace ConsoleApp2021;

public class Day20 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Example);
        var iea = input.iea;

        Print(input.ii);
        var copy1 = Enhance(iea, input.ii);
        var copy2 = Enhance(iea, copy1);
        Print(copy1);
        Print(copy2);
        return copy2.Values.Count(v => v == '#');
    }

    private Dictionary<(int x, int y), char> Enhance(char[] iea, Dictionary<(int x, int y), char> image)
    {
        var start = image;
        var minx = image.Keys.Min(xy => xy.x);
        var maxx = image.Keys.Max(xy => xy.x);
        var miny = image.Keys.Min(xy => xy.y);
        var maxy = image.Keys.Max(xy => xy.y);

        var copy = new Dictionary<(int x, int y), char>();
        for (var x = minx - 1; x <= maxx + 1; x++)
        {
            for (var y = miny - 1; y <= maxy + 1; y++)
            {
                var key = (x, y);
                var neighbors = GetNeighbors(start, key.x, key.y);
                var value = iea[GetIndex(neighbors)];
                copy.Add(key, value);
            }
        }

        return copy;
    }

    private void Print(Dictionary<(int x, int y), char> image)
    {
        var iy = image.Keys.Select(k => k.y).Distinct().OrderBy(y => y);
        var ix = image.Keys.Select(k => k.x).Distinct().OrderBy(x => x);
        foreach (var x in ix)
        {
            foreach (var y in iy)
            {
                ConsoleX.Write(image[(x, y)]);
            }

            ConsoleX.WriteLine();
        }

        ConsoleX.WriteLine();
        ConsoleX.WriteLine(string.Empty.PadRight(iy.Count(), '-'));
    }


    private int GetIndex(string neighbors)
    {
        return Convert.ToInt32(neighbors.Replace('.', '0').Replace('#', '1'), 2);
    }

    private string GetNeighbors(Dictionary<(int x, int y), char> start, int x, int y)
    {
        var result = new List<char>();
        var d = new[] { -1, 0, 1 };
        foreach (var dx in d)
        {
            foreach (var dy in d)
            {
                var key = (x + dx, y + dy);
                if (start.ContainsKey(key))
                {
                    result.Add(start[key]);
                }
                else
                {
                    result.Add('.');
                }
            }
        }

        return string.Join(string.Empty, result);
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        return 0;
    }

    public (char[] iea, Dictionary<(int x, int y), char> ii) Parse(string[] input)
    {
        var iea = input.First().ToCharArray();
        var ii = new Dictionary<(int x, int y), char>();
        int x = 0;
        foreach (var line in input.Skip(2))
        {
            int y = 0;
            foreach (var c in line)
            {
                ii.Add((x, y), c);
                y++;
            }
            x++;
        }

        return (iea, ii);
    }

}