using Common;
using System.Runtime.CompilerServices;

namespace ConsoleApp2023;

public class Day14 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 113078;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid();

        RollNorth(input);

        var sum = GetSupportNorthBeamWeight(input);


        return sum;
    }

    private static long GetSupportNorthBeamWeight(char[,] input)
    {
        long sum = 0;
        var maxvalue = input.GetRowCount();
        for (var ri = 0; ri < input.GetRowCount(); ri++)
        {
            var rowvalue = maxvalue - ri;
            var rockcount = input.GetRow(ri).Count(ri => ri == 'O');

            sum += (rowvalue * rockcount);
        }

        return sum;
    }

    private static void RollNorth(char[,] input)
    {
        bool hasmoved;
        do
        {
            hasmoved = false;
            for (var ri = input.GetRowCount() - 1; ri > 0; ri--)
            {
                var row = input.GetRow(ri);

                for (var ci = 0; ci < input.GetColumnCount(); ci++)
                {
                    var c = row[ci];
                    var nc = input[ri - 1, ci];
                    if (c == 'O' && nc == '.')
                    {
                        input[ri - 1, ci] = 'O';
                        input[ri, ci] = '.';
                        hasmoved = true;
                    }
                }
            }
        } while (hasmoved);
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 94255;
        PuzzleContext.UseExample = false;

        char[,] input = PuzzleContext.Input.GetGrid();
        var hash = input.GetHashCode<char>();

        for (var cycle = 1L; cycle<=1000000000; cycle++)
        {
            if (CycleCache.TryGetValue(hash, out var cached))
            {
                (input, hash) = cached;
                continue;
            }

            input = DoCycle(input);
            var newHash = input.GetHashCode<char>();
            CycleCache.Add(hash, ((char[,])input.Clone(), newHash));
            hash = newHash;
        }

        var sum = GetSupportNorthBeamWeight(input);
        
        return sum;
    }


    private static readonly Dictionary<int, (char[,], int)> CycleCache = new();

    private static char[,] DoCycle(char[,] input)
    {
        RollNorth(input); // North
        input = input.RotateRight();
        RollNorth(input); // West
        input = input.RotateRight();
        RollNorth(input); // South
        input = input.RotateRight();
        RollNorth(input); // East
        input = input.RotateRight();
        
        return input;
    }

}