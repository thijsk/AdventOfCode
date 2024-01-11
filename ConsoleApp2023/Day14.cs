using Common;

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

    private static readonly Dictionary<int, (long cycle, long weight, int hash)> CycleCache = new();

    public long Part2()
    {
        PuzzleContext.Answer2 = 94255;
        PuzzleContext.UseExample = false;

        char[,] input = PuzzleContext.Input.GetGrid();
        var hash = input.GetHashCode<char>();

        long weight = 0;

        const int iterations = 1000000000;

        for (var cycle = 1L; cycle<= iterations; cycle++)
        {
            if (CycleCache.TryGetValue(hash, out var cached))
            {
                long lastcycle;
                (lastcycle, weight, hash) = cached;
                var jump = cycle - lastcycle;
                var remainder = (iterations - cycle) % jump;
                cycle = iterations - remainder;
                continue;
            }

            input = DoCycle(input); 
            weight = GetSupportNorthBeamWeight(input);
            var newHash = input.GetHashCode<char>();
            CycleCache.Add(hash, (cycle, weight, newHash));
            hash = newHash;
        }

        //var sum = GetSupportNorthBeamWeight(input);
        
        return weight;
    }
    
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