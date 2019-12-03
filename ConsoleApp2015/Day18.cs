using System;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day18 : IDay
    {
        public int Part1()
        {
            var start = ParseInput();

            for (int i = 0; i < 100; i++)
            {
                var next = Move1(start);
                start = next;
            }

            var count = 0;
            foreach (var light in start)
            {
                if (light) count++;
            }

            return count;
        }

        public int Part2()
        {
            var start = ParseInput();

            for (int i = 0; i < 100; i++)
            {
                var next = Move2(start);
                start = next;
            }

            var count = 0;
            foreach (var light in start)
            {
                if (light) count++;
            }

            return count;
        }

        private bool[,] Move1(bool[,] start)
        {
            var result = new bool[100, 100];
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    var neighbors = GetNeighborsOf(x, y, start);
                    var neighborsOn = neighbors.Count(n => n);
                    var newstate = false;
                    if (start[x, y])
                    {//A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
                        newstate = (neighborsOn == 2 || neighborsOn == 3);
                    }
                    else
                    {//A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.
                        newstate = (neighborsOn == 3);
                    }
                    result[x, y] = newstate;
                }
            }
            return result;
        }

        private bool[,] Move2(bool[,] start)
        {
            start[0, 0] = true;
            start[0, 99] = true;
            start[99, 0] = true;
            start[99, 99] = true;

            var result = new bool[100, 100];
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    var neighbors = GetNeighborsOf(x, y, start);
                    var neighborsOn = neighbors.Count(n => n);
                    var newstate = false;
                    if (start[x, y])
                    {//A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
                        newstate = (neighborsOn == 2 || neighborsOn == 3);
                    }
                    else
                    {//A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.
                        newstate = (neighborsOn == 3);
                    }
                    result[x, y] = newstate;
                }
            }
            result[0, 0] = true;
            result[0, 99] = true;
            result[99, 0] = true;
            result[99, 99] = true;
            return result;
        }

        private bool[] GetNeighborsOf(int cx, int cy, bool[,] start)
        {
            var result = new bool[8];
            int idx = 0;
            for (int x = cx - 1; x <= cx + 1; x++)
            {
                for (int y = cy - 1; y <= cy + 1; y++)
                {
                    if (x == cx && y == cy)
                        continue;
                    if (x >= 0 && y >= 0 && x < 100 && y < 100)
                        result[idx] = start[x, y];
                    idx++;
                }
            }
            return result;
        }

        private void ToConsole(bool[,] start)
        {
            var counter = 0;
            foreach (var x in start)
            {
                Console.Write(x ? '#' : '.');
                counter++;
                if ((counter %= 100) == 0)
                    Console.WriteLine();
            }
        }

        private bool[,] ParseInput()
        {
            var result = new bool[100, 100];
            var linecounter = 0;
            foreach (var line in File.ReadLines("day18.txt"))
            {
                var colcounter = 0;
                foreach (var chr in line)
                {
                    if (chr == '#')
                        result[linecounter, colcounter] = true;
                    else
                        result[linecounter, colcounter] = false;
                    colcounter++;
                }

                linecounter++;
            }
            return result;
        }

        
    }
}
