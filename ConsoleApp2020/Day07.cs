using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day07 : IDay
    {
        const string  myBag = "shiny gold";

        public long Part1()
        {
            var input = ParseInput();
        


            
            var foundColors = new HashSet<string>();

            var searchBags = new List<string>() { myBag };

            while (searchBags.Any())
            {
                var newColors = new List<string>();
                foreach (var searchBag in searchBags)
                {
                    var outerBags = FindOuterBags(input, searchBag);
                    foreach (var bag in outerBags)
                    {
                        if (!foundColors.Contains(bag))
                        {
                            foundColors.Add(bag);
                            newColors.Add(bag);
                        }
                    }
                }

                searchBags = newColors;
            }


            return foundColors.Count();
        }

        public long Part2()
        {
            var input = ParseInput();
            return CountBags(input, myBag) - 1;
        }

        private long CountBags(IEnumerable<Rule> input, string findBag)
        {
            var rule = input.First(r => r.outerBag == findBag);
            long count = 1;
            foreach (var inner in rule.innerBags)
            {
                long innerCount = inner.Item1 * CountBags(input, inner.Item2);
                count += innerCount;
            }

            return count;
        }

        private IEnumerable<string> FindOuterBags(IEnumerable<Rule> input, string myBag)
        {
            return input.Where(i => i.innerBags.Any(i => i.Item2 == myBag)).Select(i => i.outerBag);
        }

        private IEnumerable<Rule> ParseInput()
        {
            var rules = new List<Rule>();
            foreach (var line in File.ReadAllLines("Day07.txt"))
            {
                var rule = new Rule(line);
                rules.Add(rule);
            }

            return rules;
        }
    }

    class Rule
    {
        public string outerBag;
        public List<(int, string)> innerBags = new List<(int, string)>();

        public Rule(string input)
        {
            ////muted gold bags contain 1 wavy red bag, 3 mirrored violet bags, 5 bright gold bags, 5 plaid white bags.
            var split1 = input.TrimEnd('.').Split("contain");
            outerBag = split1[0].Replace("bags", "").Trim();
            var split2 = split1[1].Split(",");
            foreach (var s in split2)
            {
                var split3 = s.Trim().Split(' ', 2);
                if (split3[0] != "no")
                {
                    var i = int.Parse(split3[0].Trim());
                    var j = split3[1].Replace("bags", "").Replace("bag", "").Trim();
                    innerBags.Add((i, j));
                }
            }
        }

    }
}
