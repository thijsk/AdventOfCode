using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
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
        PuzzleContext.UseExample = true;

        var input = PuzzleContext.Input.First().AsCircular().GetEnumerator();
        var pieces = Parse(InputPieces).ToList().AsCircular().GetEnumerator();

        var height = DoPart1(pieces, input, 2022);

        return height;
    }

    private long DoPart1(IEnumerator<Piece> pieces, IEnumerator<char> input, long target)
    {
        var pieceCounter = 1L;

        var height = 0;
        Grid<bool> world = new Grid<bool>();

        const int leftBorder = 0;
        const int rightBorder = 6;

        while (pieceCounter <= target)
        {
            Point<int> spawn = (2, height + 3);
            pieces.MoveNext();
            var piece = pieces.Current;
            piece.Location = (0, 0);
            piece.Location += spawn;

            //ConsoleX.WriteLine($"The {pieceCounter} rock begins falling");

            if (pieceCounter == target)
            {
                DrawGame(world, piece, leftBorder, rightBorder);
            }

            while (true)
            {
                input.MoveNext();
                var move = input.Current;
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
                    foreach (var p in piece.Points)
                    {
                        world[p] = true;
                    }

                    break;
                }
            }

            height = GetHeight(world);

            if (pieceCounter == target)
            {
                ConsoleX.WriteLine(height, ConsoleColor.DarkMagenta);
                DrawGame(world, piece, leftBorder, rightBorder);
            }

            pieceCounter++;
        }

        return height;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        var inputList = PuzzleContext.Input.First().ToList();
        var pieceList = Parse(InputPieces).ToList();

        var target = 1000000000000;

        return DoPart2(pieceList, inputList, target);
    }

    private readonly Dictionary<int, int[]> _worldCache = new();
    readonly Dictionary<int, (int inc, int worldHash, int moveIndex, long totalHeight, long pieceCounter)> _cache = new();

    private long DoPart2(IEnumerable<Piece> pieces, IEnumerable<char> input, long target)
    {
        var pieceCounter = 0L;

        var totalHeight = 0L;
        var world = new int[] { -1, -1, -1, -1, -1, -1, -1 };

        var worldHash = GetWorldProfileHash(world);
        _worldCache.Add(worldHash, world);

        const int leftBorder = 0;
        const int rightBorder = 6;

        var pieceArray = pieces.ToArray();
        var pieceCount = pieceArray.Length;

        var moveArray = input.ToArray();
        var moveIndex = 0;
        var moveCount = moveArray.Length;

        var sw = new Stopwatch();
        sw.Start();

        //bool periodDetect = false;

        //int periodStartKey = 0;

        while (pieceCounter < target)
        {
            var pieceIndex = (pieceCounter % pieceCount);
            var cachekey = (pieceIndex, moveIndex, worldHash).GetHashCode();

            if (_cache.TryGetValue(cachekey, out var cacheresult))
            {
                //if (!periodDetect)
                //{
                //    periodDetect = true;
                //    periodStartKey = cachekey;
                //}

                //if (cachekey == periodStartKey)
                //{
                    var heightIncrement = totalHeight - cacheresult.totalHeight;
                    var pieceCounterIncrement = pieceCounter - cacheresult.pieceCounter;

                    var togo = target - pieceCounter;
                    var loops = togo / pieceCounterIncrement;
                    pieceCounter += pieceCounterIncrement * loops;
                    totalHeight += heightIncrement * loops;
                //}

                totalHeight += cacheresult.inc;

                worldHash = cacheresult.worldHash;
                moveIndex = cacheresult.moveIndex;
                pieceCounter++;
                continue;
            }

            var piece = pieceArray[pieceIndex];
            world = _worldCache[worldHash];

            var worldHeight = GetHeight(world);
            Point<int> spawn = (2, worldHeight + 3);
            piece.Location = (0, 0);
            piece.Location += spawn;

            while (true)
            {
                var move = moveArray[moveIndex];
                moveIndex++;
                moveIndex %= moveArray.Length;

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

            var newHeight = GetHeight(world);
            var increment = newHeight - worldHeight;
            totalHeight += increment;
            var reduction = Reduce(world);
            worldHash = GetWorldProfileHash(world);

            _cache.Add(cachekey, (increment, worldHash, moveIndex, totalHeight - increment, pieceCounter));
            _worldCache.TryAdd(worldHash, world.ToArray());

            pieceCounter++;
        }

        return totalHeight;
    }

    private int Reduce(int[] world)
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

    private int GetWorldProfileHash(int[] world)
    {
        HashCode hashCode = new HashCode();
        foreach (var x in world)
        {
            hashCode.Add(x);
        }
        return hashCode.ToHashCode();
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
                if (world[(x, y)])
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
        var touchesWorld = points.Any(p => world[p]);
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
