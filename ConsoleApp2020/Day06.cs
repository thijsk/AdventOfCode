using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    public class Day06 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();
            var total = 0;
            foreach (var group in input)
            {
                var count = group.SelectMany(l => l).Distinct().Count();
                total += count;
            }

            return total;
        }

        public long Part2()
        {
            var input = ParseInput();
            var total = 0;
            foreach (var group in input)
            {
                // https://stackoverflow.com/a/1676684
                var intersection = group
                    .Skip(1)
                    .Aggregate(
                        new HashSet<char>(group.First()),
                        (h, e) => { h.IntersectWith(e); return h; }
                    );
                var count = intersection.Count();
                total += count;
            }

            return total;
        }

        public List<List<char[]>> ParseInput()
        {
            var lines = File.ReadAllLines("Day06.txt");

            var groups = new List<List<char[]>>();
            var group = new List<char[]>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    groups.Add(group);
                    group = new List<char[]>();
                }
                else
                {
                    group.Add(line.ToCharArray());
                }
            }

            groups.Add(group);

            return groups;
        }
    }
}
