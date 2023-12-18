using System.Diagnostics;
using Common;
using System.Drawing;
using System.Formats.Asn1;

namespace ConsoleApp2023;

public class Day18 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 52231;
        PuzzleContext.UseExample = true;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var start = new Point<int>(0, 0);
        var location = start;

        var grid = new Grid<int>();

        grid.Add(location, 1);

        foreach (var step in input)
        {
            for (int i = 0; i < step.meters; i++)
            {
                location = location + step.d;
                grid[location] = 1;
            }
        }

        var xoffset = grid.Keys.Min(p => p.x);
        var yoffset = grid.Keys.Min(p => p.y);

        var width = grid.Keys.Max(p => p.x) - xoffset + 1;
        var height = grid.Keys.Max(p => p.y) - yoffset + 1;

        var intgrid = new int[width, height];
        foreach (var p in grid.Keys)
        {
            intgrid[p.x - xoffset, p.y - yoffset] = grid[p];
        }

        intgrid.ToConsole();

        var fill = FloodFill(new Queue<Point<int>>(new[] { start + Directions.RightDown }), grid);

        return grid.Count + fill.Count;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 952408144115;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).Select(i => i.color).Select(ColorToInstruction).ToArray();

        var start = new Point<int>(0, 0);
        var end = start;

        List<Line<int>> lines = new();

        foreach (var step in input)
        {
            end = start + (step.d.x * (step.meters), step.d.y * (step.meters));
            Line<int> line = new(start, end);
            lines.Add(line);
            start = end;
        }

        Debug.Assert(lines.First().start == lines.Last().end);

        var sumOfLeftPairs = 0L;
        var sumOfRightPairs = 0L;
        foreach (var line in lines)
        {
            sumOfLeftPairs += (line.start.x * (long)line.end.y);
            sumOfRightPairs += (line.start.y * (long)line.end.x);
        }
        var shoelace = Math.Abs(sumOfLeftPairs - sumOfRightPairs) / 2L;

        var sumOfLength = 0L;
        foreach (var line in lines)
        {
            var xlength = Math.Abs(line.start.x - line.end.x);
            var ylength = Math.Abs(line.start.y - line.end.y);
            Debug.Assert(xlength == 0 || ylength == 0);
            Debug.Assert(xlength + ylength > 0);
            sumOfLength += xlength + ylength;
        }

        return (shoelace) + (sumOfLength / 2) + 1L;
    }

    private ((int x, int y) d, int meters) ColorToInstruction(string color)
    {
        var meterstring = color[1..6];
        var meters = Convert.ToInt32(meterstring, 16);
        
        return color[6] switch
        {
            '0' => (Directions.Right, meters),
            '1' => (Directions.Down, meters),
            '2' => (Directions.Left, meters),
            '3' => (Directions.Up, meters),
            _ => throw new Exception("Invalid direction")
        };
    }


    private ((int x, int y) d, int meters, string color) Parse(string line)
    {
        var (p, m, c) = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var d = p switch
        {
            "R" => Directions.Right,
            "L" => Directions.Left,
            "U" => Directions.Up,
            "D" => Directions.Down,
            _ => throw new Exception("Invalid direction")
        };
        return (d, int.Parse(m), c.Trim('(', ')') );
    }

    private static HashSet<Point<int>> FloodFill(Queue<Point<int>> q, Dictionary<Point<int>, int> trench)
    {
        HashSet<Point<int>> fill = new();
        while (q.TryDequeue(out var p))
        {
            fill.Add(p);
            foreach (var d in Directions.All)
            {
                var n = p + d;

                if (trench.ContainsKey(p + d))
                {
                    continue;
                }
                if (q.Contains(n))
                {
                    continue;
                }
                if (fill.Contains(n))
                {
                    continue;
                }
                q.Enqueue(p+d);
            }
        }

        return fill;
    }

}