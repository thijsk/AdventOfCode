using Common;

namespace ConsoleApp2023;

public class Day11 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 9509330;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        HashSet<int> emptyColIndexes = new();
        for (var ci =0 ; ci <input.GetNumberOfColumns(); ci++)
        {
            var c = input.GetColumn(ci);
            if (c.All(x => x == '.'))
            {
                emptyColIndexes.Add(ci);
            }
        }

        HashSet<int> emptyRowIndexes = new();
        for (var ri = 0; ri < input.GetNumberOfRows(); ri++)
        {
            var r = input.GetRow(ri);
            if (r.All(x => x == '.'))
            {
                emptyRowIndexes.Add(ri);
            }
        }

        var galaxies = input.Find('#');

        var distances = new List<int>();

        var pairs = galaxies.SelectMany((g, i) => galaxies.Skip(i + 1), (g1, g2) => (first: g1, second: g2)).ToList();

        long sum = 0;
        foreach (var (first, second) in pairs)
        {
            var distance = ((Point<int>)first).ManhattanDistanceTo((Point<int>)second);
            
            var minX = Math.Min(first.x, second.x);
            var maxX = Math.Max(first.x, second.x);
            var minY = Math.Min(first.y, second.y);
            var maxY = Math.Max(first.y, second.y);

            var emptyCols = emptyColIndexes.Where(x => x > minY && x < maxY).Count();
            var emptyRows = emptyRowIndexes.Where(x => x > minX && x < maxX).Count();

            sum += ((distance) + emptyCols + emptyRows);
        }


        return sum;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 635832237682;
        PuzzleContext.UseExample = false;
        var input = PuzzleContext.Input.GetGrid(c => c);

        HashSet<int> emptyColIndexes = new();
        for (var ci = 0; ci < input.GetNumberOfColumns(); ci++)
        {
            var c = input.GetColumn(ci);
            if (c.All(x => x == '.'))
            {
                emptyColIndexes.Add(ci);
            }
        }

        HashSet<int> emptyRowIndexes = new();
        for (var ri = 0; ri < input.GetNumberOfRows(); ri++)
        {
            var r = input.GetRow(ri);
            if (r.All(x => x == '.'))
            {
                emptyRowIndexes.Add(ri);
            }
        }

        var galaxies = input.Find('#');

        var distances = new List<int>();

        var pairs = galaxies.SelectMany((g, i) => galaxies.Skip(i + 1), (g1, g2) => (first: g1, second: g2)).ToList();

        const long factor = 1000000L;

        long sum = 0L;
        foreach (var (first, second) in pairs)
        {
            long distance = ((Point<int>)first).ManhattanDistanceTo((Point<int>)second);

            var minX = Math.Min(first.x, second.x);
            var maxX = Math.Max(first.x, second.x);
            var minY = Math.Min(first.y, second.y);
            var maxY = Math.Max(first.y, second.y);

            var emptyCols = (long)emptyColIndexes.Where(y => y > minY && y < maxY).Count();
            var emptyRows = (long)emptyRowIndexes.Where(x => x > minX && x < maxX).Count();

            sum += ((distance - emptyCols - emptyRows) + (emptyCols * factor) + (emptyRows * factor));
        }


        return sum;
    }
}