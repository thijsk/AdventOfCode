using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    public class Day03 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            return Downhill(input, 1, 3);
        }

        public long Part2()
        {
            /*
            Right 1, down 1.
            Right 3, down 1. (This is the slope you already checked.)
            Right 5, down 1.
            Right 7, down 1.
            Right 1, down 2.
            */


            var input = ParseInput();

            long result1 = Downhill(input, 1, 1);
            long result2 = Downhill(input, 1, 3);
            long result3 = Downhill(input, 1, 5);
            long result4 = Downhill(input, 1, 7);
            long result5 = Downhill(input, 2, 1);

            return result1 * result2 * result3 * result4 * result5;
        }

        private static int Downhill(bool[,] input, int down, int right)
        {
            var result = 0;
            var rows = input.GetUpperBound(0) + 1;
            var cols = input.GetUpperBound(1) + 1;

            var col = 0;
            for (int row = 0; row < rows; row += down)
            {
                //for (int pc = 0; pc < col; pc++)
                //{
                //    Console.Write(input[row, pc] ? "#" : ".");
                //}

                var tree = input[row, col];
                if (tree)
                {
                    result++;
                   // Console.Write("X");
                }
                else
                {
                   // Console.Write("O");
                }

                //for (int pc = col + 1; pc < cols; pc++)
                //{
                //    Console.Write(input[row, pc] ? "#" : ".");
                //}
                //Console.WriteLine();
                col += right;
                col %= cols;
            }

            return result;
        }



        private bool[,] ParseInput()
        {
            var input = File.ReadAllLines("Day03.txt");
            var result = new bool[input.Length, input[0].Length];
            var linecounter = 0;
            foreach (var line in input)
            {
              //  Console.WriteLine(line);
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
