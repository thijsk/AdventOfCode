using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day10 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var adapters = input.OrderBy(i => i).ToList();
            var builtIn = adapters.Max() + 3;
            adapters.Add(builtIn);


            int[] differences = new int[3];

            

            var output = 0;
            foreach (var rating in adapters)
            {
                var diff = rating - output;
                if (diff <= 3)
                {
                    output = rating;
                }
                else
                {
                    throw new Exception("Invalid adapter");
                }

                differences[diff - 1]++;

            }


            return differences[0] * differences[2];
        }

        public long Part2_1()
        {
            var input = ParseInput().OrderBy(i => i).ToList();

            input.Insert(0, 0);
            input.Add(input.Max() + 3);

            var arrangements = CountArrangements(0, input);

            //Console.WriteLine(SolvePartTwo());
            return arrangements;
        }


        /*

0 [1 2 3] 4

0 1 2 3 4
0 1 2 4
0 1 3 4
0 2 3 4
0 1 4
0 2 4
0 3 4

een groepje van 3 opvolgende heeft 7 mogelijkheden

0 [1 2] 3

0 1 2 3
0 1 3
0 2 3
0 3

een groepje van 2 opvolgende heeft 4 mogelijkheden

0 [1] 2

0 1 2
0 2

een groepje van 1 opvolgende heeft 2 mogelijkheden
    

         */

        public long Part2()
        {
            var input = ParseInput().OrderBy(i => i).ToList();

            input.Insert(0, 0);
            input.Add(input.Max() + 3);

            int[] groups = new int[3];

            var group = 0;

            for (int i = 1; i < input.Count; i++)
            {
                var prev = input[i - 1];
                var me = input[i];

                if ((me - prev) == 1)
                {
                    group++;
                }
                else if (group > 1)
                {
                    groups[group - 2]++;
                    group = 0;
                }
                else
                {
                    group = 0;
                }

               // Console.WriteLine($"{i} : {input[i]} => {group}");
            }

            var result = Math.Pow(2, groups[0]) * Math.Pow(4, groups[1]) * Math.Pow(7, groups[2]);
            Console.WriteLine(result);
            return 0;
        }

        Dictionary<int, long> _countCache = new Dictionary<int, long>();
        private long CountArrangements(int start, List<int> input)
        {
            if (start == input.Count - 1)
            {
                return 1;
            }

            long count = 0;

            var output = input[start];
            var options = new List<int>();
            for (int i = start + 1; i < input.Count; i++)
            {
                var rating = input[i];

                var diff = rating - output;
                if (diff > 3)
                {
                    break;
                }

                if (_countCache.ContainsKey(i))
                {
                    count += _countCache[i];
                }
                else
                {
                    var nr = CountArrangements(i, input);
                    _countCache.Add(i, nr);
                    count += nr;
                }
            }

            Console.WriteLine($"{start} {count}");
            return count;
        }

        //thank you reddit
        //string SolvePartTwo()
        //{
        //    var XMAS = ParseInput().OrderBy(i => i).ToList();
        //    //time to find out many can be omitted.
        //    XMAS.Insert(0, 0);
        //    XMAS.Add(XMAS.Last() + 3);
        //    int POW2 = 0;
        //    int POW7 = 0;
        //    for (int i = 1; i < XMAS.Count - 1; i++)
        //    {
        //        long negative3 = (i >= 3) ? XMAS[i - 3] : -9999;
        //        if (XMAS[i + 1] - negative3 == 4)
        //        {
        //            POW7 += 1;
        //            POW2 -= 2;
        //        }
        //        else if (XMAS[i + 1] - XMAS[i - 1] == 2)
        //        {
        //            POW2 += 1;
        //        }
        //    }
        //    double danswer = System.Math.Pow(2, POW2) * System.Math.Pow(7, POW7);

        //    return $"{danswer}";
        //}

        public List<int> ParseInput()
        {
            return File.ReadAllLines($"Day10.txt").Select(int.Parse).ToList();

        }
    }
}