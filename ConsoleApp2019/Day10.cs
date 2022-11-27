
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2019
{
    class Day10 : IDay
    {
        public const string input1 = @".#..#..#..#...#..#...###....##.#....
.#.........#.#....#...........####.#
#..##.##.#....#...#.#....#..........
......###..#.#...............#.....#
......#......#....#..##....##.......
....................#..............#
..#....##...#.....#..#..........#..#
..#.#.....#..#..#..#.#....#.###.##.#
.........##.#..#.......#.........#..
.##..#..##....#.#...#.#.####.....#..
.##....#.#....#.......#......##....#
..#...#.#...##......#####..#......#.
##..#...#.....#...###..#..........#.
......##..#.##..#.....#.......##..#.
#..##..#..#.....#.#.####........#.#.
#......#..........###...#..#....##..
.......#...#....#.##.#..##......#...
.............##.......#.#.#..#...##.
..#..##...#...............#..#......
##....#...#.#....#..#.....##..##....
.#...##...........#..#..............
.............#....###...#.##....#.#.
#..#.#..#...#....#.....#............
....#.###....##....##...............
....#..........#..#..#.......#.#....
#..#....##.....#............#..#....
...##.............#...#.....#..###..
...#.......#........###.##..#..##.##
.#.##.#...##..#.#........#.....#....
#......#....#......#....###.#.....#.
......#.##......#...#.#.##.##...#...
..#...#.#........#....#...........#.
......#.##..#..#.....#......##..#...
..##.........#......#..##.#.#.......
.#....#..#....###..#....##..........
..............#....##...#.####...##.";

        private const string input2 = @".#..#
.....
#####
....#
...##";

        private const string input3 = @"...
.##
..#";

        private const string input4 = @"#...#
..#..
.###.
..#..
#...#";

        public const string input = input1;

        int coorr = 0;
        int coorc = 0;

        public long Part1()
        {
            var grid = ParseInput(input);

            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            var max = 0;


            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (grid[r, c] == 1)
                    {
                        grid[r, c] = CountHits(grid, r, c);
                        if (grid[r, c] > max)
                        {
                            max = grid[r, c];
                            coorr = r;
                            coorc = c;
                        }
                        Console.Write(grid[r, c]);
                    }
                    else
                    {
                        Console.Write('.');
                    }

                }

                Console.WriteLine();
            }


            Console.WriteLine($"{coorr},{coorc}");

            return max;
        }

        private int CountHits(int[,] grid, int ir, int ic)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            var vectors = new HashSet<double>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (!(ir == r && ic == c) && grid[r, c] > 0)
                    {

                        var angle = Math.Atan2(r - ir, c - ic);
                        //Console.WriteLine($"{r},{c} {angle}");
                        if (!vectors.Contains(angle))
                        {
                            vectors.Add(angle);
                        }
                    }
                }
            }

            return vectors.Count();
        }

        private int[,] ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine).ToArray();

            var width = lines.First().Length;
            var height = lines.Count();

            var grid = new int[height, width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    grid[h, w] = lines[h][w] == '#' ? 1 : 0;
                }
            }
            return grid;
        }

        public void PrintGrid(int[,] grid)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (grid[r, c] > 0)
                    {
                        int i = grid[r, c];
                        Console.Write($"{i:D2}");
                    }
                    else
                    {
                        Console.Write(" .");
                    }

                }

                Console.WriteLine();
            }
        }

        private struct Astroid
        {
            public int row;
            public int column;
            public double distance;
            public double angle;
        }


        public long Part2()
        {
            var grid = ParseInput(input);

            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            var ir = coorr;
            var ic = coorc;
            int zapcount = 0;

            while (true)
            {
                var vectors = new Dictionary<double, Astroid>();

                // scan
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        if (!(ir == r && ic == c) && grid[r, c] > 0)
                        {
                            var vr = r - ir;
                            var vc = c - ic;
                            var astroid = new Astroid()
                            {
                                row = r,
                                column = c,
                                distance = Math.Sqrt((vr * vr) + (vc * vc)),
                                angle = ((Math.Atan2(r - ir, c - ic) * (180 / Math.PI)) + 360 + 90) % 360
                            };

                            //Console.WriteLine($"{r},{c} {angle}");
                            if (vectors.TryGetValue(astroid.angle, out var existing))
                            {
                                if (existing.distance > astroid.distance)
                                {
                                    vectors[astroid.angle] = astroid;
                                }
                            }
                            else
                            {
                                vectors.Add(astroid.angle, astroid);
                            }
                        }
                    }
                }

                // vape
                Console.WriteLine($"Zapped {zapcount}");
                PrintGrid(grid);

                var sortedVectors = vectors.Values.OrderBy(v => (v.angle)).ToList();
                if (sortedVectors.Count ==0)
                {
                    break;
                }
                foreach (var vector in sortedVectors)
                {
                    grid[vector.row, vector.column] = 0;
                    zapcount++;
                    if (zapcount == 200)
                    {
                        return (vector.column * 100) + vector.row;
                    }
                }
            }
            return 0;
        }
    }
}
