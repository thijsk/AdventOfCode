using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day17 : IDay
    {
        private string input = @"33
14
18
20
45
35
16
35
1
13
18
13
50
44
48
6
24
41
30
42";

        struct Container
        {
            public int Number;
            public int Size;
        }
    
        public long Part1()
        {
            List<Container> containers = ParseInput();

            var powerset = containers.GetPowerSet();
            var solutions = powerset.Where(s => s.Sum(c => c.Size) == 150);

            return solutions.Count();
        }
        
        private List<Container> ParseInput()
        {
            var result = new List<Container>();

            int number = 0;
            foreach (var ic in input.Split('\n'))
            {
                number++;
                var c = new Container();
                c.Number = number;
                c.Size = int.Parse(ic.Trim());
                result.Add(c);
            }

            return result;
        }

        public long Part2()
        {
            List<Container> containers = ParseInput();

            var powerset = containers.GetPowerSet().ToList();
            var solutions = powerset.Where(s => s.Sum(c => c.Size) == 150).ToList();
            var solution = solutions.Min(s => s.Count() );

            var number = solutions.Count(s => s.Count() == solution);

            return number;
        }
    }
}
