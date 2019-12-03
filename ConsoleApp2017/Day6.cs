using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2017
{
    class Day6 : IDay
    {
        private string input = @"5	1	10	0	1	7	13	14	3	12	8	10	7	12	0	6";

        

        public int Part1()
        {
            //input = @"0	2	7	0";

            int[] banks = ParseInput();

            var history = new List<int[]>();
            history.Add(banks);

            int count = 0;
            while (true)
            {
                count++;
                banks = Redistribute(banks);
                if (history.Any(b => b.SequenceEqual(banks)))
                break;
                history.Add(banks);
            }

            return count;
        }

        private int[] Redistribute(int[] banks)
        {
            var result = new int[banks.Length];
            var max = banks.Max();
            var redistindex = banks.ToList().IndexOf(max);

            banks.CopyTo(result,0);
            result[redistindex] = 0;
            while (max > 0)
            {
                var index = ++redistindex % result.Length;
                result[index]++;
                max--;
            }

            return result;
        }

        private int[] ParseInput()
        {
            return input.Split('\t').Select(m => m.Trim()).Select(n => int.Parse(n)).ToArray();
        }

        public int Part2()
        {
            //input = @"0	2	7	0";

            int[] banks = ParseInput();

            var history = new List<int[]>();
            history.Add(banks);

            int count = 0;
            while (true)
            {
                count++;
                banks = Redistribute(banks);
                if (history.Any(b => b.SequenceEqual(banks)))
                    break;
                history.Add(banks);
            }

            return count - history.FindIndex(b => b.SequenceEqual(banks));
        }
    
    }
}
