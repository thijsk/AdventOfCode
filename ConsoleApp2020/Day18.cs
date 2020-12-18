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

            //Edit the precedence in the Operation class
            var evaluator = new ExpressionEvaluator();

            long sum = 0;
            foreach (var line in input)
            {
                var result = evaluator.Evaluate(line);
                Console.WriteLine($"{line} = {result}");
                sum += Convert.ToInt64(result);
            }

            return sum;
        }

        public long Part2()
        {
            var input = ParseInput();

            return 0;
        }

        public string[] ParseInput()
        {
            return File.ReadAllLines($"Day18.txt");

        }
    }
}