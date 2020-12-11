using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Common;

namespace ConsoleApp2020
{
    class Day11 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            //PrintGrid(input);
            string inputString = AsString(input);
            while (true)
            {
               // PrintGrid(input);
                var nextGrid = Iterate(input);
                
                var nextString = AsString(nextGrid);
                if (nextString == inputString)
                    break;
                input = nextGrid;
                inputString = nextString;
            }

            return inputString.Count(c => c == '#');
        }

        private string AsString(char[,] grid)
        {
            var result = new StringBuilder();
            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    result.Append(grid[x, y]);

                }
            }

            return result.ToString();
        }

        public long Part2()
        {
            var input = ParseInput();

            //PrintGrid(input);
            string inputString = AsString(input);
            while (true)
            {
                //PrintGrid(input);
           //     Console.ReadLine();
                var nextGrid = Iterate2(input);

                var nextString = AsString(nextGrid);
                if (nextString == inputString)
                    break;
                input = nextGrid;
                inputString = nextString;
            }

            return inputString.Count(c => c == '#');
        }

        /*
            If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
            If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
            Otherwise, the seat's state does not change.
        */
        public char[,] Iterate(char[,] grid)
        {
            var result = grid.Clone() as char[,];

            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    result[x, y] = IterateSeat(grid, x, y);
                }
            }

            return result;
        }

        public char[,] Iterate2(char[,] grid)
        {
            var result = grid.Clone() as char[,];

            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    result[x, y] = IterateSeat2(grid, x, y);
                }
            }

            return result;
        }

        private char IterateSeat(char[,] grid, int x, int y)
        {
            char me = grid[x, y];
            if (me =='L')
            {
                int occupied = OccupiedAround(grid, x, y);
                if (occupied == 0)
                {
                    me = '#';
                }
            } else if (me == '#')
            {
                int occupied = OccupiedAround(grid, x, y);
                if (occupied >=4)
                {
                    me = 'L';
                }
            }

            return me;
        }
        private char IterateSeat2(char[,] grid, int x, int y)
        {
            char me = grid[x, y];
            if (me == 'L')
            {
                int occupied = OccupiedAround2(grid, x, y);
                if (occupied == 0)
                {
                    me = '#';
                }
            }
            else if (me == '#')
            {
                int occupied = OccupiedAround2(grid, x, y);
                if (occupied >= 5)
                {
                    me = 'L';
                }
            }

            return me;
        }

        private int OccupiedAround(char[,] grid, int ix, int iy)
        {

            var minx = Math.Max(ix-1, 0);
            var maxx = Math.Min(ix+1, grid.GetUpperBound(0));
            var miny = Math.Max(iy-1, 0);
            var maxy = Math.Min(iy+1, grid.GetUpperBound(1));

            int count = 0;

            for (int x = minx; x <= maxx; x++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    if (x == ix && y == iy)
                    {

                    }
                    else if (grid[x, y] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private const int debugx = 0;
        private const int debugy = 3;

        private int OccupiedAround2(char[,] grid, int ix, int iy)
        {
            int count = 0;
            for (int incx = -1; incx <= 1; incx++)
            {
                for (int incy = -1; incy <= 1; incy++)
                {
                    var seen = CountLine(grid, ix, iy, incx, incy);
                    count += seen;
                    //if (ix == debugx && iy == debugy)
                    //   Console.WriteLine($"{ix},{iy} {incx},{incy} : {seen} => {count}");
                 
                }
            }
            
            return count;
        }

        private int CountLine(char[,] grid, int ix, int iy, int incx, int incy)
        {
            if (incy == 0 && incx == 0)
                return 0;

            var minx = 0;
            var maxx = grid.GetUpperBound(0);
            var miny = 0;
            var maxy = grid.GetUpperBound(1);
            int startx = ix;
            int starty = iy;
            int endx = incx > 0 ? maxx : minx;
            int endy = incy > 0 ? maxy : miny;

            for (int x = startx, y = starty; (incx > 0 ? x <= endx : x >= endx)&&(incy > 0 ? y <= endy : y >= endy); x += incx,y += incy )
            {
                if (x == ix && y == iy)
                {

                }
                else if (grid[x, y] == '#')
                {
                    return 1;
                }
                else if (grid[x, y] == 'L')
                {
                    return 0;
                }
            }

            return 0;
        }

        public void PrintGrid(char[,] grid)
        {
            for (int x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    var olderColor = Console.ForegroundColor;
                    if (x == debugx && y == debugy)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(grid[x, y]);

                    Console.ForegroundColor = olderColor;

                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public char[,] ParseInput()
        {
            var lines = File.ReadAllLines($"Day11.txt");
            var grid = new char[lines.Length, lines[0].Length];
            for (int x = 0; x < lines.Length; x++)
            {
                for (int y = 0; y < lines[0].Length; y++)
                {
                    grid[x, y] = lines[x][y];
                }
            }

            return grid;
        }
    }
}