using Common;
using System.Diagnostics;

namespace ConsoleApp2023;

public class Day22 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 407;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Brick.Parse).OrderBy(b => b.MinZ).ToArray();

        FreeFall(input);

        return input.Count(b => CanBeSafelyDisIntegrated(input.ToHashSet(), b));
    }
    
    public long Part2()
    {
        PuzzleContext.Answer2 = 59266;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Brick.Parse).OrderBy(b => b.MinZ).ToArray();

        FreeFall(input);
        
        return input.Sum(brick => CountFallen(input.ToHashSet(), brick));
    }

    private long CountFallen(HashSet<Brick> input, Brick firstBrick)
    {
        Queue<Brick> fronteer = new();
        fronteer.Enqueue(firstBrick);

        HashSet<Brick> fallen = new();

        while (fronteer.TryDequeue(out var brick))
        {
            if (fallen.Contains(brick))
            {
                continue;
            }

            fallen.Add(brick);
            ConsoleX.WriteLine($"{brick} has fallen");

            foreach (var other in input.Except(fallen))
            {
                var supports = GetAllBricksBelow(input, other).ToList();
                if (supports.Any() && supports.All(b => fallen.Contains(b)))
                {
                    fronteer.Enqueue(other);
                }
            }
        }

        return fallen.Count - 1;
    }

    private static void FreeFall(Brick[] input)
    {
        bool hasMoved;

        do
        {
            hasMoved = false;

            foreach (var brick in input)
            {
                while (brick.MinZ > 1 && !input.Any(b => b != brick && b.IsDirectlyBelow(brick)))
                { 
                    brick.MoveOneDown();
                    hasMoved = true;
                }
            }
        } while (hasMoved);
    }

    private bool CanBeSafelyDisIntegrated(HashSet<Brick> input, Brick brick)
    {
        var supportedBricks = GetAllBricksAbove(input, brick).ToList();

        // if the brick is not supporting any other brick, it can be disintegrated
        if (!supportedBricks.Any())
        {
            return true;
        }

        // if the brick is supporting another brick, but that brick is also supported by another brick, it can be disintegrated
        if (supportedBricks.All(b => GetAllBricksBelow(input, b).Any(b => b != brick)))
        {
            return true;
        }

        return false;
    }


    private Dictionary<Brick, IEnumerable<Brick>> _aboveCache = new();
    private IEnumerable<Brick> GetAllBricksAbove(HashSet<Brick> input, Brick brick)
    {
        if (_aboveCache.TryGetValue(brick, out var cached))
        {
            return cached;
        }

        var result = input.Where(b => b != brick && b.IsDirectlyAbove(brick)).ToList();
        _aboveCache.Add(brick, result);
        return result;
    }

    private Dictionary<Brick, IEnumerable<Brick>> _belowCache = new();
    private IEnumerable<Brick> GetAllBricksBelow(HashSet<Brick> input, Brick brick)
    {
        if (_belowCache.TryGetValue(brick, out var cached))
        {
            return cached;
        }

        var result = input.Where(b => b != brick && b.IsDirectlyBelow(brick)).ToList();
        _belowCache.Add(brick, result);
        return result;
    }
    
    private class Brick
    {
        private Point3<int> _begin;
        private Point3<int> _end;
        private string _name;

        public static Brick Parse(string input)
        {
            var parts = input.Split('~');
            var (x1, y1, z1) = parts[0].Split<int>(',');
            var (x2, y2, z2) = parts[1].Split<int>(',');
            return new Brick{ _name = input, _begin = new Point3<int>(x1, y1, z1), _end = new Point3<int>(x2, y2, z2) };
        }

        public static Brick Copy(Brick other)
        {
            return new Brick {_name = other.Name, _begin = other._begin, _end = other._end};
        }

        public int MinZ => Math.Min(_begin.z, _end.z);
        public int MaxZ => Math.Max(_begin.z, _end.z);

        public string Name => _name;
        
        public bool IsDirectlyAbove(Brick other)
        {
            return _begin.x <= other._end.x && _end.x >= other._begin.x
                   && _begin.y <= other._end.y && _end.y >= other._begin.y
                   && MinZ == other.MaxZ +1;
        }

        public bool IsDirectlyBelow(Brick other)
        {
            return _begin.x <= other._end.x && _end.x >= other._begin.x
                                          && _begin.y <= other._end.y && _end.y >= other._begin.y
                                          && MaxZ == other.MinZ - 1;
        }

        public void MoveOneDown()
        {
            _begin = new Point3<int>(_begin.x, _begin.y, _begin.z - 1);
            _end = new Point3<int>(_end.x, _end.y, _end.z - 1);
        }

        public override string ToString()
        {
            return $"{_name} at {_begin}~{_end}";
        }
    }

}