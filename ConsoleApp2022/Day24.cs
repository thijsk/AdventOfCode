using System.Diagnostics;
using System.Numerics;
using Common;

namespace ConsoleApp2022;

public class Day24 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 262;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        var maps = GetMinuteMaps(input);
        var period = maps.Keys.Count;

        var start = (0, 1);
        var goal = (input.GetLength(0) - 1, input.GetLength(1) - 2);

        var minutes = FindExit(maps, start, goal, 0);
        
        return minutes;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 785;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        var maps = GetMinuteMaps(input);
        var period = maps.Keys.Count;

        var start = (0, 1);
        var goal = (input.GetLength(0) - 1, input.GetLength(1) - 2);

        var trip1 = FindExit(maps, start, goal, 0);
        ConsoleX.WriteLine($"Trip 1 {trip1}");
        var trip2 = FindExit(maps, goal, start, trip1);
        ConsoleX.WriteLine($"Trip 2 {trip2}");
        var trip3 = FindExit(maps, start, goal, trip1+trip2);
        ConsoleX.WriteLine($"Trip 2 {trip3}");

        return trip1 + trip2 + trip3;
    }

    private int FindExit(Dictionary<int, Tile[,]> maps, (int row, int col) start, (int row, int col) goal, int startminute)
    {
        var period = maps.Keys.Count;
        Dictionary<(int minute, (int row, int col)), int> visited = new();
        Queue< (int minute, (int row, int col) position)> frontier = new();

        frontier.Enqueue((startminute, start));

        var fastest = int.MaxValue;

        var clock =0 ;

        while (frontier.Count > 0)
        {
            clock++;
            if (clock % 1000000 == 0)
            {
                Console.WriteLine(frontier.Count);
            }

            var (minute, position) = frontier.Dequeue();
            var key = (minute % period, position);
            if (visited.ContainsKey(key))
            {
                var visit = visited[key];
                if (minute >= visit)
                    continue;

                visited[key] = minute;
            }
            else
            {
                visited.Add(key, minute);
            }

            if (position.Equals(goal))
            {
                if (minute < fastest)
                {
                    fastest = minute;
                    ConsoleX.WriteLine($"Fasted time: {fastest - startminute}", ConsoleColor.Green);
                }
                continue;
            }

            //var map = maps[minute % period];
  
            
            minute++;
            var map = maps[minute % period];
            var neighbors = map.GetNeighbors(position.row, position.col);
            foreach (var neighbor in neighbors)
            {
                if (map[neighbor.x, neighbor.y] == Tile.Empty)
                {
                    frontier.Enqueue((minute, (neighbor.x, neighbor.y)));
                }
            }
            if (map[position.row, position.col] == Tile.Empty)
                frontier.Enqueue((minute, position));
        }

        return fastest - startminute;
    }

    private Dictionary<int, Tile[,]> GetMinuteMaps(Tile[,] map)
    {
        var rows = map.GetLength(0) - 2;
        var cols = map.GetLength(1) - 2;
        var period = Math2.LeastCommonMultiple(rows, cols);

        var cache = new Dictionary<int, Tile[,]> {{0, map}};

        foreach (var minute in 1..((int) period-1))
        {
            map = NextMap(map);
            cache.Add(minute, map);
        }
        return cache;
    }

    private int GetGridHashCode<T>(T[,] array)
    {
        HashCode hash = new HashCode();
        foreach (var item in array)
        {
            hash.Add(item);
        }
        return hash.ToHashCode();
    }

    private static Tile[,] NextMap(Tile[,] map)
    {
        var nextmap = new Tile[map.GetLength(0),map.GetLength(1)];
        var maxrow = map.GetLength(0) - 1;
        var maxcol = map.GetLength(1) - 1;
        for (var row = 0; row <= maxrow; row++)
        {
            for (var col = 0; col <= maxcol; col++)
            {
                var tile = map[row, col];
                if (tile is Tile.Wall)
                {
                    nextmap[row, col] = tile;
                    continue;
                }
                
                if (tile.HasFlag(Tile.Up))
                {
                    var nextrow = row - 1;
                    if (nextrow == 0)
                        nextrow = maxrow - 1;
                    nextmap[nextrow, col] |= Tile.Up;
                }

                if (tile.HasFlag(Tile.Down))
                {
                    var nextrow = row + 1;
                    if (nextrow == maxrow)
                        nextrow = 1;
                    nextmap[nextrow, col] |= Tile.Down;
                }

                if (tile.HasFlag(Tile.Left))
                {
                    var nextcol = col - 1;
                    if (nextcol == 0)
                        nextcol = maxcol - 1;
                    nextmap[row, nextcol] |= Tile.Left;
                }

                if (tile.HasFlag(Tile.Right))
                {
                    var nextcol = col + 1;
                    if (nextcol == maxcol)
                        nextcol = 1;
                    nextmap[row, nextcol] |= Tile.Right;
                }
            }
        }

        return nextmap;
    }

    private void PrintGrid(Tile[,] input, (int row, int col) p)
    {
        for (var row = 0; row < input.GetLength(0); row++)
        {
            for (var col = 0; col < input.GetLength(1); col++)
            {
                if (row == p.row && col == p.col)
                {
                    Debug.Assert(input[row,col] == Tile.Empty);
                    ConsoleX.Write('E');
                    continue;
                }

                var tile = input[row, col];
                var c = tile switch
                {
                    Tile.Wall => '#',
                    Tile.Empty => '.',
                    Tile.Up => '^',
                    Tile.Down => 'v',
                    Tile.Left => '<',
                    Tile.Right => '>',
                    _ => BitOperations.PopCount((uint)tile).ToString()[0]
                };

                ConsoleX.Write(c);
            }
            ConsoleX.WriteLine();
        }
    }



    private Tile[,] Parse(string[] lines)
    {
        return lines.GetGrid(c =>
        {
            return c switch
            {
                '#' => Tile.Wall,
                '.' => Tile.Empty,
                '^' => Tile.Up,
                'v' => Tile.Down,
                '<' => Tile.Left,
                '>' => Tile.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
            };
        });
    }

    [Flags]
    enum Tile
    {
        Empty = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        Wall = 16
    }

}