using System.Numerics;
using Common;

namespace ConsoleApp2022;

public class Day00 : IDay
{
    //https://aoc.infi.nl/2022/#
    public long Part1()
    {
        var direction = new Vector2(0, 1);

        var position = new Vector2(0, 0);

        foreach (var line in PuzzleContext.Input)
        {
            (string instruction, string value) = line.Split(' ');
            var intvalue = int.Parse(value);

            switch (instruction)
            {
                case "draai":
                    var radians = (float)(Math.PI / 180) * intvalue;
                    direction = Vector2.Transform(direction, Matrix3x2.CreateRotation(radians));
                    direction.X = (float)Math.Round(direction.X);
                    direction.Y = (float)Math.Round(direction.Y);
                    Console.WriteLine($"D {direction}");
                    break;
                case "loop":
                    position += (direction * intvalue);
                    break;
                case "spring":
                    position += (direction * intvalue);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Console.WriteLine($"P {position}");

        }

        return (long) (Math.Abs(position.X) + Math.Abs(position.Y));
    }

    public long Part2()
    {
        var direction = new Vector2(0, 1);

        var position = new Vector2(0, 0);

        var history = new List<Vector2>();
        history.Add(position);

        foreach (var line in PuzzleContext.Input)
        {
            (string instruction, string value) = line.Split(' ');
            var intvalue = int.Parse(value);

            switch (instruction)
            {
                case "draai":
                    var radians = (float)(Math.PI / 180) * intvalue;
                    direction = Vector2.Transform(direction, Matrix3x2.CreateRotation(radians));
                    direction.X = (float)Math.Round(direction.X);
                    direction.Y = (float)Math.Round(direction.Y);
                    Console.WriteLine($"D {direction}");
                    break;
                case "loop":
                    foreach (int j in ..(intvalue - 1))
                    {
                        position += (direction);
                        history.Add(position);
                    }

                    break;
                case "spring":
                    position += (direction * intvalue);
                    history.Add(position);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Console.WriteLine($"P {position}");

        }

        var minx = (int)history.Min(p => p.X);
        var miny = (int)history.Min(p=> p.Y);
        var maxx = (int)history.Max(p=> p.X);
        var maxy = (int)history.Max(p=> p.Y);

      
        for (int y = maxy ; y >= miny; y--)
        {
            for (int x = maxx; x >= minx; x--)
            {
                if (history.Any(p => (int)p.X == x && (int)p.Y == y))
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }

        return 0;
    }



}