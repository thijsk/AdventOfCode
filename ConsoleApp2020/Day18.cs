using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day18 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var evaluator = new ExpressionEvaluator(new Dictionary<char, int> { { '+', 1 }, { '-', 1 }, { '*', 1 }, { '/', 1} });

            long sum = 0;
            foreach (var line in input)
            {
                var result = evaluator.Evaluate(line);
              //  Console.WriteLine($"{line} = {result}");
                sum += Convert.ToInt64(result);
            }

            return sum;
        }

        public long Part2()
        {
            var input = ParseInput();

            var evaluator = new ExpressionEvaluator(new Dictionary<char, int> { { '+', 2 }, { '-', 1 }, { '*', 1 }, { '/', 1 } });

            long sum = 0;
            foreach (var line in input)
            {
                var result = evaluator.Evaluate(line);
              //  Console.WriteLine($"{line} = {result}");
                sum += Convert.ToInt64(result);
            }

            return sum;
        }

        public string[] ParseInput()
        {
            return File.ReadAllLines($"Day18.txt");

        }
    }
}