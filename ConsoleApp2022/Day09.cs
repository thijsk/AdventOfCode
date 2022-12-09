using System.Numerics;
using Common;
using Microsoft.VisualBasic.CompilerServices;

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
           // Console.WriteLine($"Steps: {steps}");
            foreach (var step in 1..steps)
            {
              //  Console.WriteLine($"S {step}");

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
               // Console.WriteLine($"H {head}");


                var xdistance = head.X - tail.X;
                var ydistance = head.Y - tail.Y;

                var xmove = false;
                var ymove = false;

                if (xdistance > 1)
                {
                    tail.X += 1;
                    xmove = true;
                }

                if (xdistance < -1)
                {
                    tail.X -= 1;
                    xmove = true;
                }

                if (ydistance > 1)
                {
                    tail.Y += 1;
                    ymove = true;
                }

                if (ydistance < -1)
                {
                    tail.Y -= 1;
                    ymove = true;
                }

                if (xmove && ydistance > 0)
                {
                    tail.Y += 1;
                }
                if (xmove && ydistance < 0)
                {
                    tail.Y -= 1;
                }
                if (ymove && xdistance > 0)
                {
                    tail.X += 1;
                }
                if (ymove && xdistance < 0)
                {
                    tail.X -= 1;
                }



              //  Console.WriteLine($"T {tail}");

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
            Console.WriteLine($"Steps: {steps}");
            foreach (var step in 1..steps)
            {
                Console.WriteLine($"S {step}");

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
                
                Console.WriteLine($"H {head}");

                var myhead = head;
                foreach (var taili in 0..8)
                {
                    var tail = rope[taili];
                    MoveTail(myhead, ref tail);
                    rope[taili] = tail;
                    Console.WriteLine($"T{taili} {tail}");
                    myhead = tail;
                }

                visited.Add(rope[8]);
            }

        }

        return visited.Distinct().Count();
    }

    private static void MoveTail(Vector2 head, ref Vector2 tail)
    {
        var xdistance = head.X - tail.X;
        var ydistance = head.Y - tail.Y;

        var xmove = false;
        var ymove = false;

        if (xdistance > 1)
        {
            tail.X += 1;
            xmove = true;
        }

        if (xdistance < -1)
        {
            tail.X -= 1;
            xmove = true;
        }

        if (ydistance > 1)
        {
            tail.Y += 1;
            ymove = true;
        }

        if (ydistance < -1)
        {
            tail.Y -= 1;
            ymove = true;
        }

        if (xmove && !ymove && ydistance > 0)
        {
            tail.Y += 1;
        }

        if (xmove && !ymove && ydistance < 0)
        {
            tail.Y -= 1;
        }

        if (ymove && !xmove && xdistance > 0)
        {
            tail.X += 1;
        }

        if (ymove && !xmove && xdistance < 0)
        {
            tail.X -= 1;
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