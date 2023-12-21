using Common;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ConsoleApp2023;

public class Day21 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 3830;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);
        var start = input.Find('S').First();

        return GetCoverage(input, start, 64);
    }

    private void ToConsole(char[,] input, HashSet<(int x, int y)> starts)
    {
        if (Debugger.IsAttached)
        {
            input.ToConsole((i, c) =>
            {
                if (starts.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('O');
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(c);
                }
            });
        }
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 637087163925555;
        PuzzleContext.UseExample = false;
        var input = PuzzleContext.Input.GetGrid(c => c);
        var start = input.Find('S').First();

        var size = input.GetLength(0);
        var halfsize = size / 2;
        var steps = 26501365;
        if (PuzzleContext.UseExample) steps = 17;

        // All this only works if the input is a set up in specific way
        Debug.Assert(input.GetLength(0) == input.GetLength(1));
        Debug.Assert(size %2 == 1);
        Debug.Assert(halfsize == start.x);
        Debug.Assert(halfsize == start.y);
        Debug.Assert(steps % size == halfsize);
        Debug.Assert(input.GetRow(halfsize).All(c => c is '.' or 'S'));
        Debug.Assert(input.GetColumn(halfsize).All(c => c is '.' or 'S'));

        
        long fullWidth = (steps - halfsize) / size - 1; // number of full grids next to the center one
        var oddGridCount = fullWidth * fullWidth;
        var evenGridCount = (fullWidth + 1) * (fullWidth + 1);
        var smallCount = fullWidth + 1;
        var largeCount = fullWidth;

        var oddGridCoverage = GetCoverage(input, start, (2 * size) + 1);
        var evenGridCoverage = GetCoverage(input, start, (2 * size));

        var topPointCoverage = GetCoverage(input, (size-1, start.y), size - 1);
        var rightPointCoverage = GetCoverage(input, (start.x, size-1), size - 1);
        var bottomPointCoverage = GetCoverage(input, (0, start.y), size - 1);
        var leftPointCoverage = GetCoverage(input, (start.x, 0), size - 1);

        var smallTopRightCoverage = GetCoverage(input, (size - 1, 0), halfsize - 1);
        var smallBottomRightCoverage = GetCoverage(input, (0, 0), halfsize - 1);
        var smallBottomLeftCoverage = GetCoverage(input, (0, size - 1), halfsize - 1);
        var smallTopLeftCoverage = GetCoverage(input, (size - 1, size - 1), halfsize - 1);
        
        var largeTopRightCoverage = GetCoverage(input, (size - 1, 0), size + halfsize - 1);
        var largeBottomRightCoverage = GetCoverage(input, (0, 0), size + halfsize - 1);
        var largeBottomLeftCoverage = GetCoverage(input, (0, size - 1), size + halfsize - 1);
        var largeTopLeftCoverage = GetCoverage(input, (size - 1, size - 1), size + halfsize - 1);

        return (oddGridCoverage * oddGridCount) +
               (evenGridCoverage * evenGridCount) +
               (topPointCoverage + rightPointCoverage + bottomPointCoverage + leftPointCoverage) +
               (smallCount * (smallTopRightCoverage + smallBottomRightCoverage + 
                              smallBottomLeftCoverage + smallTopLeftCoverage)) +
               (largeCount * (largeTopRightCoverage + largeBottomRightCoverage + 
                              largeBottomLeftCoverage + largeTopLeftCoverage));
    }

    private long GetCoverage(char[,] input, (int x, int y) start, int totalsteps)
    {
        var q = new Queue<((int x, int y), int steps)>();

        var stepsOddOrEven = totalsteps % 2;

        q.Enqueue((start, 0));

        var visited = new HashSet<(int x, int y)>();
        long count = 0;

        while (q.TryDequeue(out var qItem))
        {
            var (current, steps) = qItem;

            if (visited.Contains(current))
                continue;

            visited.Add(current);

            if (steps % 2 == stepsOddOrEven)
            {
                count++;
            }

            if (steps == totalsteps)
                continue;
            var neighbors = input.GetNeighbors(current).Where(n => input[n.x, n.y] != '#').Except(visited);

            foreach (var neighbor in neighbors)
            {
                q.Enqueue((neighbor, steps + 1));
            }

        }

        //ToConsole(input, visited);

        return count;
    }
}