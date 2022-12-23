using Common;

namespace ConsoleApp2022;

public class Day23 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 0;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        LinkedList<char> proposalOrder = new(new[] { 'N', 'S', 'W', 'E' });

        var world = input;

        //PrintWorld(world);

        foreach (var round in 1..10)
        {
          
            // Propose
            foreach (var elf in world.Keys)
            {
                world[elf] = Propose(elf, world, proposalOrder);
            }

            var newWorld = new Dictionary<Point<int>, Point<int>?>();
            foreach (var (elf, proposal) in world)
            {
                if (!proposal.HasValue || world.Values.Count(p => p == proposal) > 1)
                {
                    newWorld.Add(elf, null);
                    
                }
                else
                {
                    newWorld.Add(proposal.Value, null);
                }
            }

            world = newWorld;
            ConsoleX.WriteLine($"End of Round {round} {string.Join(',', proposalOrder)}");
           //PrintWorld(world);

            var order = proposalOrder.First;
            proposalOrder.RemoveFirst();
            proposalOrder.AddLast(order);

        }

        var size = GetWorldSize(world);

        return size - world.Keys.Count;
    }


    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        LinkedList<char> proposalOrder = new(new[] { 'N', 'S', 'W', 'E' });

        var worldLock = new object();
        var world = input;

        PrintWorld(world);

        var round = 0;
        while (true)
        {
            round++;
            int moved = 0;
            // Propose
            Parallel.ForEach(world.Keys, elf =>
            {
                var prop = Propose(elf, world, proposalOrder);
                if (prop.HasValue)
                {
                    lock (worldLock)
                    {
                        world[elf] = prop;
                    }
                }
            });
  
            var newWorld = new Dictionary<Point<int>, Point<int>?>();

            var duplicates = world.Values.Where(p => p.HasValue).GroupBy(p => p).Where(g => g.Count() > 1).Select(g => g.Key).ToHashSet();
            foreach (var (elf, proposal) in world)
            {
                if (!proposal.HasValue || duplicates.Contains(proposal))
                {
                    newWorld.Add(elf, null);
                }
                else
                {
                    moved++;
                    newWorld.Add(proposal.Value, null);
                }
            }

            if (moved == 0) 
                return round;

            if (round % 10 == 0)
            {
                ConsoleX.WriteLine($"Round {round} moved {moved}");
            }

            world = newWorld;
            ConsoleX.WriteLine($"End of Round {round} {string.Join(',', proposalOrder)}");
 
            var order = proposalOrder.First;
            proposalOrder.RemoveFirst();
            proposalOrder.AddLast(order);
        }

        var size = GetWorldSize(world);

        return size - world.Keys.Count;
    }

    private int GetWorldSize(Dictionary<Point<int>, Point<int>?> world)
    {
        var minx = world.Keys.Min(k => k.x);
        var maxx = world.Keys.Max(k => k.x);
        var miny = world.Keys.Min(k => k.y);
        var maxy = world.Keys.Max(k => k.y);

        var xSize = 1 + (maxx - minx);
        var ySize = 1 + (maxy - miny);

        return xSize * ySize;

    }

    private void PrintWorld(Dictionary<Point<int>, Point<int>?> world)
    {
        var minx = world.Keys.Min(k => k.x);
        var maxx = world.Keys.Max(k => k.x);
        var miny = world.Keys.Min(k => k.y);
        var maxy = world.Keys.Max(k => k.y);

        for (int y = miny; y <= maxy; y++)
        {
            for (int x = minx; x <= maxx; x++)
            {
                if (world.ContainsKey((x, y)))
                {
                    ConsoleX.Write('#');
                }
                else
                {
                    ConsoleX.Write('.');
                }
            }
            ConsoleX.WriteLine();
        }

    }

    private readonly Point<int> North = (0, -1);
    private readonly Point<int> South = (0, 1);
    private readonly Point<int> West = (-1, 0);
    private readonly Point<int> East = (1, 0);

    private Point<int>? Propose(Point<int> elf, Dictionary<Point<int>, Point<int>?> world, LinkedList<char> proposalOrder)
    {
        var eNw = elf + North + West;
        var eN = elf + North;
        var eNe = elf + North + East;
        var eE = elf + East;
        var eSe = elf + South + East;
        var eS = elf + South;
        var eSw = elf + South + West;
        var eW = elf + West;
        
        Point<int>? proposal = null;
        if (IsEmpty(world, new[] { eNw, eN, eNe, eE, eSe, eS, eSw, eW } ))
            return proposal;

        foreach (var direction in proposalOrder)
        {
            switch (direction)
            {
                case 'N':
                {
                    if (IsEmpty(world, eNw, eN, eNe))
                    {
                        return eN;
                    }

                    break;
                }
                case 'S':
                {
                    if (IsEmpty(world, eSw, eS, eSe))
                    {
                        return eS;
                    }

                    break;
                }
                case 'E':
                {
                    if (IsEmpty(world, eE, eSe, eNe))
                    {
                        return eE;
                    }

                    break;
                }
                case 'W':
                {
                    if (IsEmpty(world, eW, eNw, eSw))
                    {
                        return eW;
                    }
                    break;
                }
            }
        }

        return proposal;
    }

    private bool IsEmpty(Dictionary<Point<int>, Point<int>?> world, params Point<int>[] points)
    {
        foreach (var point in points)
        {
            if (world.ContainsKey(point))
                return false;
        }
        return true;
    }


    public Dictionary<Point<int>, Point<int>?> Parse(string[] lines)
    {
        var result = new Dictionary<Point<int>, Point<int>?>();

        int y = 0;
        foreach (var line in lines)
        {
            int x = 0;
            foreach (var c in line)
            {
                if (c == '#')
                {
                    result.Add((x, y), null);
                }

                x++;
            }
            y++;
        }

        return result;

    }



}