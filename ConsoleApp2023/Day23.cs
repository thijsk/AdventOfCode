using Common;
using System.ComponentModel;
using System.Diagnostics;

namespace ConsoleApp2023;

public class Day23 : IDay
{
    private Dictionary<(int x, int y), List<(int x, int y)>> _tunnels;

    public long Part1()
    {
        PuzzleContext.Answer1 = 1930;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var start = (0, 1);
        var end = (input.GetRowCount()-1, input.GetColumnCount()-2);

        return GetLongestPath(input, start, end, new HashSet<(int, int)>());
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 6230;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var start = (0, 1);
        var end = (input.GetRowCount() - 1, input.GetColumnCount() - 2);

        _tunnels = FindTunnels(input);

        return GetLongestPath(input, start, end, new HashSet<(int, int)>(), 0, false);
    }

    private Dictionary<(int x, int y), List<(int x, int y)>> FindTunnels(char[,] input)
    {
        var tunnels = new Dictionary<(int x, int y), List<(int x, int y)>>();

        for (var x = 0; x < input.GetRowCount(); x++)
        {
            for (var y = 0; y < input.GetColumnCount(); y++)
            {
                if (input[x, y] != '#')
                {
                    var neighbors = input.GetNeighbors(x, y).Where(n => input[n.x, n.y] != '#').ToList();
                    if (neighbors.Count != 2)
                    {
                        foreach (var neighbor in neighbors)
                        {
                           tunnels[neighbor] = FollowTunnel(input, (x, y), neighbor);
                        }
                    }
                }
            }
        }

        return tunnels;
    }

    private List<(int x, int y)> FollowTunnel(char[,] input, (int x, int y) prev, (int x, int y) current)
    {
        var result = new List<(int x, int y)> { current };
        var next = input.GetNeighbors(current).Where(n => n != prev && input[n.x, n.y] != '#').ToList();
        if (next.Count == 1)
        {
            result.AddRange(FollowTunnel(input, current, next[0]));
        }
        return result;
    }


    private long GetLongestPath(char[,] input, (int x, int y) current, (int x, int y) end, HashSet<(int, int)> visited, long pathLength = 0, bool slopesEnabled = true)
    {
        var longestPath = 0L;
            
        if (current == end)
        {
            return pathLength;
        }

        List<(int x, int y)> neighborsToVisit;
        if (slopesEnabled)
        {
            var neighbors = input[current.x, current.y] switch
            {
                '.' => input.GetNeighbors(current),
                '>' => input.GetNeighborsInDirection(current, Directions.Right),
                '<' => input.GetNeighborsInDirection(current, Directions.Left),
                '^' => input.GetNeighborsInDirection(current, Directions.Up),
                'v' => input.GetNeighborsInDirection(current, Directions.Down),
                _ => throw new Exception("Invalid character")
            };

            neighborsToVisit = neighbors.Where(n => input[n.x, n.y] != '#').ToList();
        }
        else
        {
           neighborsToVisit = input.GetNeighbors(current).Where(n => input[n.x, n.y] != '#').ToList();
        }

        foreach (var neighbor in neighborsToVisit)
        {
            var n = neighbor;
            var steps = 1;
            if (!slopesEnabled && _tunnels.TryGetValue(n, out var tunnel))
            {
                n = tunnel[^1];
                steps = tunnel.Count;
            }

            if (!visited.Add(n))
            {
                continue;
            }

            var newPathLength = GetLongestPath(input, n, end, visited, pathLength + steps, slopesEnabled);

            if (newPathLength > longestPath)
            {
                longestPath = newPathLength;
            } 

            visited.Remove(n);
        }

        return longestPath;
    }

    private static void PrintGrid(char[,] input, (int x, int y)[] path)
    {
        input.ToConsole((p, c) =>
        {
            if (path.Contains(p))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(c);
        });
    }


    private long Parse(string line)
    {
        return long.Parse(line);
    }

}