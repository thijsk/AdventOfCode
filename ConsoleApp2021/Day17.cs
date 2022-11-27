using System.Runtime.ExceptionServices;
using Common;

namespace ConsoleApp2021;

public class Day17 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).First();

        var hits = new List<((int xvel, int yvel) vel, int ytop, (int x, int y)[] trajectory)>();


        for (int initialx = 1; initialx < 100; initialx++)
        {
            for (int initialy = 1; initialy < 100; initialy++)
            {
                var result = Shoot(initialx, initialy, input);
                if (result.hit)
                {
                    hits.Add(((initialx, initialy), result.topy, result.trajectory));
                }
            }
        }

        var top = hits.Max(h => h.ytop);

        Print(input, hits.First(r => r.ytop == top).trajectory);
        return top;
    }

    private void Print(((int minx, int maxx) x, (int miny, int maxy) y) input, (int x, int y)[] resultTrajectory)
    {
        var trajectory = resultTrajectory.ToHashSet();
        var maxy = trajectory.Max(t => t.y);

        for (var y = maxy; y >= input.y.miny; y--)
        {
            for (var x = 0; x <= input.x.maxx; x++)
            {
                var c = '.';

                if (x.Between(input.x.minx, input.x.maxx) && y.Between(input.y.miny, input.y.maxy))
                    c = 'T';
                if (trajectory.Contains((x, y)))
                    c = '#';
                if (x == 0 && y == 0)
                    c = 'S';
                ConsoleX.Write(c);
            }

            ConsoleX.WriteLine();
        }
    }

    private static (bool hit, int topy, (int x, int y)[] trajectory) Shoot(int xvelocity, int yvelocity, ((int minx, int maxx) x, (int miny, int maxy) y) input)
    {
        //ConsoleX.WriteLine($"Trying {xvelocity}, {yvelocity}");
        var xvel = xvelocity;
        var yvel = yvelocity;
        var xpos = 0;
        var ypos = 0;
        var topy = 0;
        var trajectory = new List<(int x, int y)>();
        for (;;)
        {
            xpos += xvel;
            ypos += yvel;
            //ConsoleX.WriteLine($"Pos { xpos},{ypos}");
            trajectory.Add((xpos, ypos));
            topy = Math.Max(topy, ypos);

            if (xvel > 0)
            {
                xvel -= 1;
            }

            if (xvel < 0)
            {
                xvel += 1;
            }

            yvel -= 1;

            var hit = (xpos.Between(input.x.minx, input.x.maxx) && ypos.Between(input.y.miny, input.y.maxy));
            if (hit)
            {
                ConsoleX.WriteLine($"Hit: {xvelocity},{yvelocity} => {xpos},{ypos} # {topy}");
                return (true, topy, trajectory.ToArray());
            }

            if (xpos > input.x.maxx || ypos < input.y.miny)
            {
                //ConsoleX.WriteLine($"Miss: {xpos},{ypos}");
                return (false, 0, trajectory.ToArray());
            }
        }
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).First();

        var hits = new List<((int xvel, int yvel), int ytop)>();

        for (int initialx = 1; initialx <= input.x.maxx; initialx++)
        {
            for (int initialy = input.y.miny; initialy < 1000; initialy++)
            {
                var result = Shoot(initialx, initialy, input);
                if (result.hit)
                {
                    hits.Add(((initialx, initialy), result.topy));
                }
            }
        }

        return hits.Count();
    }

    public ((int minx, int maxx) x,(int miny, int maxy) y) Parse(string line)
    {
        //target area: x = 20..30, y = -10..-5

        line = line.Replace("target area: ", "");
        var xysplit = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var xsplit = xysplit[0].Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Split("..").Select(int.Parse).ToArray();
        var ysplit = xysplit[1].Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Split("..").Select(int.Parse).ToArray();
        return ((xsplit[0], xsplit[1]), (ysplit[0], ysplit[1]));
    }

}