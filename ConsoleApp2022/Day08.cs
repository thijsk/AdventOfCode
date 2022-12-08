﻿using Common;

namespace ConsoleApp2022;

public class Day08 : IDay
{
    public long Part1()
    {
        var input = ParseInput(PuzzleContext.Input);

        long count = 0;

        var rows = input.GetRows();
        var columns = input.GetColumns();
        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
             

                var visible = IsVisibleFromEdge(input, x, y, rows, columns);
                var i = visible ? 1 : 0;
                count += i;
                //Console.Write(i);
            }
            //Console.WriteLine();
        }

        return count;
    }

    private bool IsVisibleFromEdge(int[,] input, int x, int y, int[][] rows, int[][] columns)
    {
        int height = input[x, y];

        var row = rows[x];
        var col = columns[y];

        var left = y == 0 || row[..y].All(t => t < height);
        var right = y == row.Length-1 ||  row[(y+1)..].All(t => t < height);
        var top = x == 0 || col[..x].All(t => t < height);
        var bottom = x == col.Length-1 || col[(x+1)..].All(t => t < height);

        bool visibile = left || right || top || bottom;
        return visibile;
    }

    public long Part2()
    {
        var input = ParseInput(PuzzleContext.Input);

        long score = 0;

        var rows = input.GetRows();
        var columns = input.GetColumns();

        for (int x = 0; x <= input.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                var treescore = GetScenicScore(input, x, y, rows, columns);
                score = Math.Max(score, treescore);
            }
        }
        return score;
    }

    private long GetScenicScore(int[,] input, int x, int y, int[][] rows, int[][] columns)
    {
        int height = input[x, y];

        var row = rows[x];
        var col = columns[y];

        var left = y == 0 ? 0 : GetDirectionScore(height, row[..y].Reverse());
        var right = y == row.Length-1 ? 0 : GetDirectionScore(height, row[(y + 1)..]);
        var top = x == 0 ? 0 : GetDirectionScore(height, col[..x].Reverse());
        var bottom = x == col.Length - 1 ? 0 : GetDirectionScore(height, col[(x + 1)..]);

        var score = left * right * top * bottom;
        return score;
    }

    private int GetDirectionScore(int height, IEnumerable<int> trees)
    {
        int count = 0;
        foreach (var tree in trees)
        {
            count++;
            if (tree >= height)
            {
                return count;
            }
        }

        return count;
    }

    private int[,] ParseInput(string[] lines)
    {
        var width = lines.First().Length;
        var height = lines.Count();

        var grid = new int[height, width];

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                var chr = lines[h][w];
                grid[h, w] = int.Parse(chr.ToString());
            }
        }
        return grid;
    }

}