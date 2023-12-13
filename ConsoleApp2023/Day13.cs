using System.Collections;
using Common;

namespace ConsoleApp2023;

public class Day13 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 34911;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.SplitByEmptyLines().Select(i => i.GetGrid(c => c)).ToArray();

        long result = 0L;
        foreach (var map in input)
        {
            map.ToConsole();

            (var value, var found) = FindMirror(map);

            result += value;
        }

        return result;
    }

    private (long value, bool found) FindMirror(char[,] map, long skipValue = 0)
    {
        var columns = map.GetColumns();
        foreach (var colindex in Enumerable.Range(1, map.GetLength(1) - 1))
        {
            var left = columns.Take(colindex).Reverse().ToArray();
            var right = columns.Skip(colindex).ToArray();

            if (left.Zip(right).All(p => p.First.SequenceEqual(p.Second)))
            {
                if (colindex == skipValue)
                    continue;
                ConsoleX.WriteLine($"Found column mirror at {colindex}");
                return (colindex, true);
            }
        }

        var rows = map.GetRows();
        foreach (var rowindex in Enumerable.Range(1, map.GetLength(0) - 1))
        {
            var top = rows.Take(rowindex).Reverse().ToArray();
            var bottom = rows.Skip(rowindex).ToArray();

            if (top.Zip(bottom).All(p => p.First.SequenceEqual(p.Second)))
            {
                if (100 * rowindex == skipValue)
                    continue;
                ConsoleX.WriteLine($"Found row mirror at {rowindex}");
                return (100 * rowindex, true);
            }
        }

        ConsoleX.WriteLine($"No mirror found");
        return (0, false);
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 33183;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.SplitByEmptyLines().Select(i => i.GetGrid(c => c)).ToArray();

        long result = 0L;
        foreach (var map in input)
        {
            map.ToConsole();
            var maps = GetPermutations(map);

            var (originalValue, found) = FindMirror(map);

            foreach (var permutation in maps)
            {
                permutation.ToConsole();
                
                (var value, found) = FindMirror(permutation, originalValue);
                if (found)
                {
                    result += value;
                    break;
                }
                    
            }
        }

        return result;
    }

    private IEnumerable<char[,]> GetPermutations(char[,] map)
    {

        for (var x = 0; x < map.GetLength(0); x++)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                var newMap = map.Clone() as char[,];
                newMap[x, y] = map[x, y] == '#' ? '.' : '#';
                yield return newMap;
            }
        }
    }
}