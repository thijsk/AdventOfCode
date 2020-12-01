using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day01 : IDay
    {
        public long Part1()
        {
            var input = File.ReadAllLines("Day01.txt").Select(l => Int32.Parse(l)).ToArray();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i+1; j < input.Length; j++)
                {
                    var a = input[i];
                    var b = input[j];
                    var sum = a + b;
                    if (sum == 2020)
                        return a * b;
                }
            }

            return 0;
        }

        public long Part2()
        {
            var input = File.ReadAllLines("Day01.txt").Select(l => Int32.Parse(l)).ToArray();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    for (int k = j + 1; k < input.Length; k++)
                    {
                        var a = input[i];
                        var b = input[j];
                        var c = input[k];
                        var sum = a + b + c;
                        if (sum == 2020)
                            return a * b * c;
                    }
                }
            }

            return 0;
        }
    }
}
