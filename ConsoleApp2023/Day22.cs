using Common;

namespace ConsoleApp2023;

public class Day22 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 407;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Brick.Parse).OrderBy(b => b.MinZ).ToArray();

        FreeFall(input);

        return input.Count(b => CanBeSafelyDisIntegrated(input, b));
    }



    public long Part2()
    {
        PuzzleContext.Answer2 = 59266;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Brick.Parse).OrderBy(b => b.MinZ).ToArray();

        ConsoleX.WriteLine(input.Max(b => b.MaxZ));

        FreeFall(input);

        ConsoleX.WriteLine(input.Max(b => b.MaxZ));

        long sum = 0L;

        foreach (var brick in input)
        {
            var falling = CountFallingIfDisintegrated(input, brick);
            sum += falling;
        }

        return sum;
    }

    private long CountFallingIfDisintegrated(Brick[] input, Brick brick)
    {
        var copy = input.Where(b => b!= brick).Select(Brick.Copy).ToArray();
        return FreeFall(copy);
    }

    private static long FreeFall(Brick[] input)
    {
        bool hasMoved;
        List<string> fallen = new();

        do
        {
            hasMoved = false;

            foreach (var brick in input)
            {
                if (brick.MinZ > 1 && !input.Any(b => b.Name != brick.Name && b.IsDirectlyBelow(brick)))
                { 
                    fallen.Add(brick.Name);
                    brick.MoveOneDown();
                    hasMoved = true;
                }
            }
        } while (hasMoved);

        return fallen.Distinct().Count();
    }

    private bool CanBeSafelyDisIntegrated(Brick[] input, Brick brick)
    {
        // if the brick does not support any other, it can be disintegrated
        if (!input.Any(b => b != brick && b.IsDirectlyAbove(brick)))
        {
           // ConsoleX.WriteLine($"Brick {brick} can be disintegrated because it does not support any other brick");
            return true;
        }

        // if the brick is supporting another brick, but that brick is supported by another brick, it can be disintegrated
        var supportedBricks = GetAllBricksAbove(input, brick);

        if (supportedBricks.All(b => GetAllBricksBelow(input, b).Any(b => b != brick)))
        {
            return true;
        }

        return false;
    }

    //private IEnumerable<Brick> GetFallingBricksRecursive(Brick[] input, Brick[] supports)
    //{
    //    var bricks = input.Where(b => GetAllBricksAbove(input, b).All(b => b.GetAllBricksBelow(input, b))).ToList();
    //    bricks.AddRange(GetFallingBricksRecursive(input, bricks.ToArray()));
    //    return bricks;
    //}

    private IEnumerable<Brick> GetAllBricksAbove(Brick[] input, Brick brick)
    {
        return input.Where(b => b != brick && b.IsDirectlyAbove(brick));
    }

    private IEnumerable<Brick> GetAllBricksBelow(Brick[] input, Brick brick)
    {
        return input.Where(b => b != brick && b.IsDirectlyBelow(brick));
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