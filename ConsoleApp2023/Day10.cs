﻿using Common;
using System.Diagnostics;

namespace ConsoleApp2023;

public class Day10 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 6823;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var start = input.Find('S').First();
        
        var position = start;
        var lastPosition = start;
        List<(int, int)> pipe = new();
        while (true)
        {
            if (pipe.Contains(position))
            {
                break;
            }
            pipe.Add(position);
            var positionPipe = input[position.x, position.y];
            var n = input.GetNeighbors(position);
            foreach (var neighbor in n)
            {
                if (neighbor == lastPosition)
                    continue;
                var neighborPipe = input[neighbor.x, neighbor.y];
                var connected = IsConnected(positionPipe, neighborPipe, (neighbor.x - position.x,neighbor.y - position.y));

                if (connected)
                {
                    lastPosition = position;
                    position = neighbor;
                    break;
                }
            }
        }


        return pipe.Count / 2;
    }

    private bool IsConnected(char p, char n, (int x, int y) d)
    {
        return n switch
        {
            '.' or 'I' or 'O' => false,
            _ => d switch
            {
                (-1, 0) => (p is 'S' or '|' or 'J' or 'L' && n is '|' or '7' or 'F'),
                (1, 0) => (p is 'S' or '|' or '7' or 'F' && n is '|' or 'J' or 'L'),
                (0, 1) => (p is 'S' or '-' or 'F' or 'L' && n is '-' or 'J' or '7'),
                (0, -1) => (p is 'S' or '-' or '7' or 'J' && n is '-' or 'L' or 'F'),
                _ => throw new Exception("Invalid direction")
            }
        };
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 415;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.GetGrid(c => c);

        var start = input.Find('S').First();

        var position = start;
        var lastPosition = start;
        HashSet<(int x, int y)> pipe = new();
        while (true)
        {
            if (pipe.Contains(position))
            {
                break;
            }
            pipe.Add(position);
            var positionPipe = input[position.x, position.y];
            var n = input.GetNeighbors(position);
            foreach (var neighbor in n)
            {
                if (neighbor == lastPosition)
                    continue;
                var neighborPipe = input[neighbor.x, neighbor.y];
                var connected = IsConnected(positionPipe, neighborPipe, ((neighbor.x - position.x), (neighbor.y - position.y)));

                if (connected)
                {
                    lastPosition = position;
                    position = neighbor;
                    break;
                }

            }
        }

        var q = EnqueueOutside(input, pipe);
        var outside = FloodFill(q, input, pipe);

        var lastStep = pipe.First();
        HashSet<(int, int)> lefts = new();
        HashSet<(int,int)> rights = new();

        foreach (var step in pipe.Skip(1))
        {
            var travel = ((step.x - lastStep.x), (step.y - lastStep.y));
            foreach (var neighbor in input.GetNeighbors(step).Except(pipe))
            {
                var nd = ((neighbor.x - step.x), (neighbor.y - step.y));
                var isLeft = IsLeft(input[step.x, step.y], travel, nd);

                if (isLeft)
                {
                    if (!lefts.Contains(neighbor))
                        lefts.Add(neighbor);
                }
                else
                {
                    if (!rights.Contains(neighbor))
                        rights.Add(neighbor);
                }
            }
            lastStep = step;
        }
        
        HashSet<(int, int)> nest;
        if (lefts.Intersect(outside).Any())
        {
            nest = FloodFill(new Queue<(int x, int y)>(rights), input, pipe);
        }
        else
        {
            nest = FloodFill(new Queue<(int x, int y)>(lefts), input, pipe);
        }
        
        PrintGrid(input, outside.ToList(), pipe.ToList(), nest.ToList());

        return nest.Count;
    }

    private bool IsLeft(char c, (int, int) t, (int, int) n)
    {
        return c switch
        {
            '|' => t switch
            {
                (-1, 0) => n == (0, -1),
                (1, 0) => n == (0, 1),
                _ => throw new Exception("Invalid direction")
            },
            '-' => t switch
            {
                (0, -1) => n == (1, 0),
                (0, 1) => n == (-1, 0),
                _ => throw new Exception("Invalid direction")
            },
            '7' => t switch
            {
                (0, 1) => n == (0, 1) || n == (-1, 0),
                (-1, 0) => false,
                _ => throw new Exception("Invalid direction")
            },
            'J' => t switch
            {
                (1, 0) => n == (0, 1) || n == (1, 0),
                (0, 1) => false,
                _ => throw new Exception("Invalid direction")
            },
            'L' => t switch
            {
                (0, -1) => n == (0, -1) || n == (1, 0),
                (1, 0) => false,
                _ => throw new Exception("Invalid direction")
            },
            'F' => t switch
            {
                (-1, 0) => n == (0, -1) || n == (-1, 0),
                (0, -1) => false,
                _ => throw new Exception("Invalid direction")
            },
            _ => false
        };
    }

    private static Queue<(int,int)> EnqueueOutside(char[,] input, HashSet<(int, int)> pipe)
    {
        var topRow = input.GetColumnIndexes(0);
        var bottomRow = input.GetColumnIndexes(input.GetColumnCount() - 1);
        var leftColumn = input.GetRowIndexes(0);
        var rightColumn = input.GetRowIndexes(input.GetRowCount() - 1);

        return new Queue<(int,int)>(topRow.Union(bottomRow).Union(leftColumn).Union(rightColumn).Except(pipe));
    }

    private static void PrintGrid(char[,] input, List<(int, int)> outside, List<(int, int)> pipe, List<(int,int)> nest)
    {
        if (!Debugger.IsAttached)
            return;

        input.ToConsole((p, c) =>
        {
            ConsoleX.ForegroundColor = ConsoleColor.White;
            if (outside.Contains(p))
            {
                ConsoleX.ForegroundColor = ConsoleColor.Blue;
            }

            if (pipe.Contains(p))
            {
                ConsoleX.ForegroundColor = ConsoleColor.Green;
            }

            if (nest.Contains(p))
            {
                ConsoleX.ForegroundColor = ConsoleColor.Red;
            }   

            ConsoleX.Write(c);
        });
    }

    private static HashSet<(int, int)> FloodFill(Queue<(int x, int y)> q, char[,] input, HashSet<(int, int)> pipe)
    {
        HashSet<(int, int)> fill = new();
        while (q.TryDequeue(out var p))
        {
            fill.Add(p);
            var candidate = input.GetNeighbors(p).Except(q).Except(fill).Except(pipe);
            foreach (var c in candidate)
            {
                q.Enqueue(c);
            }
        }

        return fill;
    }
}