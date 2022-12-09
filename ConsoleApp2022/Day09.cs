using System.Numerics;
using Common;

namespace ConsoleApp2022;

public class Day09 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var visited = new List<Vector2>();
        var tail = new Vector2(0, 0);
        var head = new Vector2(0, 0);
        visited.Add(tail);

        foreach (var (direction, steps) in input)
        {
            ConsoleX.WriteLine($"Direction {direction} Steps: {steps}");
            foreach (var step in 1..steps)
            {
                ConsoleX.WriteLine($"S {step}");

                switch (direction)
                {
                    case 'U':
                        head.Y += 1;
                        break;
                    case 'D':
                        head.Y -= 1;
                        break;
                    case 'R':
                        head.X += 1;
                        break;
                    case 'L':
                        head.X -= 1;
                        break;
                }
               ConsoleX.WriteLine($"H {head}");
               MoveTail(head, ref tail);
               ConsoleX.WriteLine($"T {tail}");

               visited.Add(tail);
            }
        }

        return visited.Distinct().Count();
    }

    public long Part2()
    {
        
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var visited = new List<Vector2>();
        var head = new Vector2(0, 0);
        var rope = new List<Vector2>();
        foreach (var i in 1..9)
        {
            rope.Add(new Vector2(0,0));
        }
        
        visited.Add(new Vector2(0,0));


        foreach (var (direction, steps) in input)
        {
            ConsoleX.WriteLine($"Steps: {steps}");
            foreach (var step in 1..steps)
            {
                ConsoleX.WriteLine($"S {step}");

                switch (direction)
                {
                    case 'U':
                        head.Y += 1;
                        break;
                    case 'D':
                        head.Y -= 1;
                        break;
                    case 'R':
                        head.X += 1;
                        break;
                    case 'L':
                        head.X -= 1;
                        break;
                }
                
                ConsoleX.WriteLine($"H {head}");

                var myhead = head;
                foreach (var taili in 0..8)
                {
                    var tail = rope[taili];
                    MoveTail(myhead, ref tail);
                    rope[taili] = tail;
                    ConsoleX.WriteLine($"T{taili} {tail}");
                    myhead = tail;
                }

                visited.Add(rope[8]);
            }

        }

        return visited.Distinct().Count();
    }

    private static void MoveTail(Vector2 head, ref Vector2 tail)
    {
        var distance = head - tail;

        var move = new Vector2(Math.Sign(distance.X), Math.Sign(distance.Y));

        var moveX = Math.Abs(distance.X) > 1;
        var moveY = Math.Abs(distance.Y) > 1;

        if (moveX)
        {
            tail.X += move.X;
        }

        if (moveY)
        {
            tail.Y += move.Y;
        }
        
        if (moveX && !moveY && distance.Y != 0)
        {
            tail.Y += move.Y;
        }

        if (moveY && !moveX && distance.X != 0)
        {
            tail.X += move.X;
        }
    }


    public (char direction, int steps) Parse(string line)
    {
        var split = line.Split(' ');
        var direction = split[0][0];
        var steps = int.Parse(split[1]);
        return (direction, steps);
    }

}