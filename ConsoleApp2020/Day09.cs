using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day09 : IDay
    {
        public long Part1()
        {

            var input = ParseInput();
            var preamblesize = 25;

            var xInput = ExpandInput(input, preamblesize);

            for(int i = preamblesize; i < input.Count(); i++)
            {
                var numberToCheck = input[i];
                var set = xInput.Skip(i - preamblesize).Take(preamblesize);
                if (!set.Any(s=> s.Contains(numberToCheck)))
                    return numberToCheck;
            }

            return 0;
        }

        private List<HashSet<long>> ExpandInput(List<long> input, int preamblesize)
        {
            var result = new List<HashSet<long>>();
            for (int n=0; n < input.Count(); n++)
            {
                var inp = input[n];
                var res = input.Skip(n + 1).Take(preamblesize-1).Select(i => inp + i).ToHashSet();
                result.Add(res);
            }

            return result;
        }

        public long Part2()
        {
            var input = ParseInput();
            var target = 138879426;

            for (int i = 0; i < input.Count; i++)
            {
                var sum = input[i];
                for (int j = i+1; i < input.Count; j++)
                {
                    sum += input[j];
                    if (sum > target)
                    {
                        break;
                    }

                    if (sum == target)
                    {
                        long min =long.MaxValue;
                        long max = long.MinValue;
                        for (int k = i; k <= j; k++)
                        {
                            min = Math.Min(input[k], min);
                            max = Math.Max(input[k], max);
                        }
                        return min + max;
                    }
                }
            }

            return 0;
        }

        public List<long> ParseInput()
        {
            return File.ReadAllLines($"Day09.txt").Select(l => long.Parse(l.Trim())).ToList();

        }
    }
}