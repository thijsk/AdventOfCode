using Common;

namespace ConsoleApp2022;

public class Day17 : IDay
{
    private const string InputPieces =
@"####

.#.
###
.#.

..#
..#
###

#
#
#
#

##
##";


    public long Part1()
    {
        PuzzleContext.Answer1 = 3100;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.First().ToList();
        var pieces = Parse(InputPieces).ToList();

        var height = Solve(pieces, input, 2022);

        return height;
    }

    private long Solve(IEnumerable<Piece> pieces, IEnumerable<char> input, long target)
    {
        Dictionary<int, (long totalHeight, long pieceCounter)> cache = new();

        var pieceCounter = 0L;
        var pieceArray = pieces.ToArray();
        var pieceCount = pieceArray.Length;
        var moveArray = input.ToArray();
        var moveIndex = 0;
        var moveCount = moveArray.Length;

        var totalHeight = 0L;
        var totalReduction = 0L;
        Grid<bool> world = new Grid<bool>();

        const int leftBorder = 0;
        const int rightBorder = 6;

        while (pieceCounter < target)
        {
            var pieceIndex = (pieceCounter % pieceCount);

            var worldHash = GetWorldHash(world);

            var cachekey = (pieceIndex, moveIndex, worldHash).GetHashCode();

            if (cache.TryGetValue(cachekey, out var cacheresult))
            {
                var heightIncrement = totalHeight - cacheresult.totalHeight;
                var pieceCounterIncrement = pieceCounter - cacheresult.pieceCounter;

                var togo = target - pieceCounter;
                var loops = togo / pieceCounterIncrement;
                pieceCounter += pieceCounterIncrement * loops;
                totalHeight += heightIncrement * loops;
            }

            var worldHeight = GetHeight(world);
            Point<int> spawn = (2, worldHeight + 3);
            var piece = pieceArray[pieceIndex];
            piece.Location = (0, 0);
            piece.Location += spawn;


            //ConsoleX.WriteLine($"The {pieceCounter} rock begins falling");

            while (true)
            {
                var move = moveArray[moveIndex];
                moveIndex++;
                moveIndex %= moveCount;
                if (move == '<')
                {
                    //ConsoleX.WriteLine("Moves left");
                    piece.Location.x -= 1;
                    if (!CanMove(piece, world, leftBorder, rightBorder))
                    {
                        //ConsoleX.WriteLine("But nothing happens");
                        piece.Location.x += 1;
                    }
                }
                else if (move == '>')
                {
                    //ConsoleX.WriteLine("Moves right");
                    piece.Location.x += 1;
                    if (!CanMove(piece, world, leftBorder, rightBorder))
                    {
                        //ConsoleX.WriteLine("But nothing happens");
                        piece.Location.x -= 1;
                    }
                }

                //ConsoleX.WriteLine("Rock falls one unit");
                piece.Location.y -= 1;
                if (!CanMove(piece, world, leftBorder, rightBorder))
                {
                    //ConsoleX.WriteLine("... causing it to rest");
                    piece.Location.y += 1;
                    foreach (var p in piece.Points)
                    {
                        world[p] = true;
                    }

                    break;
                }
            }


            if (pieceCounter + 1 == target)
            {
                Console.WriteLine($"After move {moveIndex} {worldHash}");
                DrawGame(world, piece, 0, 6);
            }

            var newHeight = GetHeight(world);
            var increment = newHeight - worldHeight;
            totalHeight += increment;

            var reduction = Reduce(world, increment);
            
            cache.TryAdd(cachekey, (totalHeight, pieceCounter));

            pieceCounter++;
        }

        return totalHeight;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 1540634005751;
        PuzzleContext.UseExample = false;

        var inputList = PuzzleContext.Input.First().ToList();
        var pieceList = Parse(InputPieces).ToList();

        var target = 1000000000000;
        
        return Solve(pieceList, inputList, target);
    }


    /// <summary>
    /// This could work... but does not account for "caves" that pieces can move into sideways
    /// </summary>
    private long DoPart2(IEnumerable<Piece> pieces, IEnumerable<char> input, long target)
    {
        Dictionary<int, int[]> worldCache = new();
        Dictionary<int, (long inc, int worldHash, int moveIndex, long totalHeight, long pieceCounter)> cache = new();


        var pieceCounter = 0L;

        var totalHeight = 0L;
        var world = new int[] { -1, -1, -1, -1, -1, -1, -1 };

        var worldHash = GetWorldProfileHash(world);
        worldCache.Add(worldHash, world);

        const int leftBorder = 0;
        const int rightBorder = 6;

        var pieceArray = pieces.ToArray();
        var pieceCount = pieceArray.Length;

        var moveArray = input.ToArray();
        var moveIndex = 0;
        var moveCount = moveArray.Length;

        while (pieceCounter < target)
        {
            var pieceIndex = (pieceCounter % pieceCount);
            var cachekey = (pieceIndex, moveIndex, worldHash).GetHashCode();

            if (cache.TryGetValue(cachekey, out var cacheresult))
            {
                var heightIncrement = totalHeight - cacheresult.totalHeight;
                var pieceCounterIncrement = pieceCounter - cacheresult.pieceCounter;

                var togo = target - pieceCounter;
                var loops = togo / pieceCounterIncrement;
                pieceCounter += pieceCounterIncrement * loops;
                totalHeight += heightIncrement * loops;
            }

            var piece = pieceArray[pieceIndex];

            var worldHeight = GetHeight(world);
            Point<int> spawn = (2, worldHeight + 3);
            piece.Location = (0, 0);
            piece.Location += spawn;

            if (pieceCounter + 1 == target)
            {
                Console.WriteLine($"Part2 {moveIndex} {worldHash}");
                DrawGame(world, piece, 0, 6);
                Console.WriteLine("=======");
            }

            while (true)
            {
                var move = moveArray[moveIndex];
                moveIndex++;
                moveIndex %= moveCount;

                if (pieceCounter == target) ConsoleX.Write(move);
                if (move == '<')
                {
                    //ConsoleX.WriteLine("Moves left");
                    piece.Location.x -= 1;
                    if (!CanMove(piece, world, leftBorder, rightBorder))
                    {
                        //ConsoleX.WriteLine("But nothing happens");
                        piece.Location.x += 1;
                    }
                }
                else if (move == '>')
                {
                    //ConsoleX.WriteLine("Moves right");
                    piece.Location.x += 1;
                    if (!CanMove(piece, world, leftBorder, rightBorder))
                    {
                        //ConsoleX.WriteLine("But nothing happens");
                        piece.Location.x -= 1;
                    }
                }

                //ConsoleX.WriteLine("Rock falls one unit");
                piece.Location.y -= 1;
                if (!CanMove(piece, world, leftBorder, rightBorder))
                {
                    //ConsoleX.WriteLine("... causing it to rest");
                    piece.Location.y += 1;
                    var piecePoints = piece.Points.ToList();
                    foreach (var p in piecePoints)
                    {
                        if (world[p.x] < p.y)
                            world[p.x] = p.y;
                    }

                    break;
                }
            }

            if (pieceCounter + 1 == target)
            {
                Console.WriteLine($"After move {moveIndex} {worldHash}");
                DrawGame(world, piece, 0, 6);
            }


            var newHeight = GetHeight(world);
            var increment = newHeight - worldHeight;
            totalHeight += increment;

            var reduction = Reduce(world);
            worldHash = GetWorldProfileHash(world);

            cache.TryAdd(cachekey, (increment, worldHash, moveIndex, totalHeight - increment, pieceCounter));
            worldCache.TryAdd(worldHash, world.ToArray());
            
            pieceCounter++;
        }

        return totalHeight;
    }

    private long Reduce(int[] world)
    {
        var min = world.Min();
        if (min > 0)
        {
            for (int x = 0; x <= 6; x++)
            {
                world[x] -= (min);
            }

            return min;
        }

        return 0;
    }

    private long Reduce(Grid<bool> world, int increment)
    {
        var worldPoints = world.Keys.ToList();

        if (!worldPoints.Any())
            return 0;

        var height = GetHeight(world);

        if (height > 50)
        {
            world.Clear();
            foreach (var p in worldPoints)
            {
                var newPoint = new Point<int>(p.x, p.y - increment);
                world.Add(newPoint, true);
            }
        }
        
        return increment;
    }

    private int GetWorldHash(Grid<bool> world)
    {
        var hashCode = new HashCode();

        var worldPoints = world.Keys.ToList();

        var maxy = worldPoints.Any() ? worldPoints.Max(p => p.y) : 0;

        for (var y = maxy; y > 0; y--)
        {
            for (int x = 0; x <= 6; x++)
            {
                if (world.ContainsKey((x, y)))
                {
                    hashCode.Add((x, y));
                }
            }
        }

        return hashCode.ToHashCode();
    }

    private int GetWorldProfileHash(int[] world)
    {
        return string.Join(',', world).GetHashCode();
    }

    private void DrawGame(Grid<bool> world, Piece piece, int leftBorder, int rightBorder)
    {
        var piecePoints = piece.Points;
        var worldPoints = world.Keys.ToList();

        var allPoints = worldPoints.Union(piecePoints);

        var maxy = allPoints.Max(p => p.y);

        for (var y = maxy; y >= Math.Max(maxy - 15, 0); y--)
        {
            for (int x = leftBorder; x <= rightBorder; x++)
            {
                if (piecePoints.Any(p => p == (x, y)))
                {
                    Console.Write('@');
                    continue;
                }
                if (world.ContainsKey((x, y)))
                {
                    Console.Write('#');
                    continue;
                }
                Console.Write('.');
            }

            Console.WriteLine();
        }


    }

    private void DrawGame(int[] world, Piece piece, int leftBorder, int rightBorder)
    {
        var piecePoints = piece.Points;
        var worldPoints = world.Select((y, x) => new Point<int>(x, y));

        int maxy = Math.Max(piecePoints.Max(p => p.y), world.Max());

        for (var y = maxy; y >= 0; y--)
        {
            for (int x = leftBorder; x <= rightBorder; x++)
            {
                if (piecePoints.Any(p => p == (x, y)))
                {
                    Console.Write('@');
                    continue;
                }
                if (worldPoints.Any(p => p == (x, y)))
                {
                    Console.Write('#');
                    continue;
                }
                Console.Write('.');
            }

            Console.WriteLine();
        }


    }

    private int GetHeight(Grid<bool> world)
    {
        if (!world.Keys.Any())
            return 0;
        return world.Keys.Max(k => k.y) + 1;
    }

    private int GetHeight(int[] world)
    {
        return world.Max() + 1;
    }

    private bool CanMove(Piece piece, Grid<bool> world, int xleft, int xright)
    {
        var points = piece.Points.ToList();
        var touchesFloor = points.Any(p => p.y < 0);
        var touchesSides = points.Any(p => p.x < xleft || p.x > xright);
        var touchesWorld = points.Any(p => world.ContainsKey(p));
        return !(touchesFloor || touchesSides || touchesWorld);
    }

    private bool CanMove(Piece piece, int[] world, int xleft, int xright)
    {
        var points = piece.Points.ToList();
        var touchesFloor = points.Any(p => p.y < 0);
        var touchesSides = points.Any(p => p.x < xleft || p.x > xright);
        var touchesWorld = points.Any(p => p.x >= xleft && p.x <= xright && world[p.x] >= p.y);
        return !(touchesFloor || touchesSides || touchesWorld);
    }

    private static IEnumerable<Piece> Parse(string pieces)
    {
        var lines = pieces.SplitOnNewlines();
        var blocks = lines.SplitByEmptyLines();
        foreach (var block in blocks)
            yield return new Piece(block);
    }

}

internal class Piece
{
    private readonly List<Point<int>> _points = new();

    public Point<int> Location;
    private readonly int _hashCode;

    public IEnumerable<Point<int>> Points =>
        _points.Select(p => p + Location);

    public Piece(string[] block)
    {
        var grid = block.GetGrid(c => c);
        int y = 0;
        foreach (var row in grid.GetRows().Reverse())
        {
            int x = 0;
            foreach (var c in row)
            {
                if (c == '#')
                    _points.Add((x, y));
                x++;
            }
            y++;
        }

        _hashCode = GetHashCodeInit();
    }

    public override int GetHashCode() => _hashCode;

    private int GetHashCodeInit()
    {
        HashCode hashCode = new HashCode();
        foreach (var point in _points)
        {
            hashCode.Add(point.GetHashCode());
        }
        return hashCode.ToHashCode();
    }
}
