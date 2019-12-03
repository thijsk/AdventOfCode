using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day17 : IDay
    {
        private string input = @"43
3
4
10
21
44
4
6
47
41
34
17
17
44
36
31
46
9
27
38";

        struct Container
        {
            public int Number;
            public int Size;
        }
    
        public int Part1()
        {
            List<Container> containers = ParseInput();

            var powerset = containers.PowerSet();
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

        public int Part2()
        {
            List<Container> containers = ParseInput();

            var powerset = containers.PowerSet().ToList();
            var solutions = powerset.Where(s => s.Sum(c => c.Size) == 150).ToList();
            var solution = solutions.Min(s => s.Count() );

            var number = solutions.Count(s => s.Count() == solution);

            return number;
        }
    }
}
