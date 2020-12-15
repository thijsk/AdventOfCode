using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day15 : IDay
    {
        private Dictionary<int, List<int>> _memory;

        public long Part1()
        {
            var input = ParseInput();
            _memory = new Dictionary<int, List<int>>();

            int turn = 1;
            int lastnumber = 0;

            foreach (var number in input)
            {
                AddToMemory(number, turn);
                lastnumber = number;
                turn++;
            }

            while (turn <= 2020)
            {
                if (!_memory.ContainsKey(lastnumber) || _memory[lastnumber].Count == 1)
                {
                    lastnumber = 0;
                }
                else
                {
                    var numbermemory = _memory[lastnumber].TakeLast(2).ToArray();
                    var difference = numbermemory[1] - numbermemory[0];
                    lastnumber = difference;
                }

                AddToMemory(lastnumber, turn);
                turn++;
            }
            Console.WriteLine();
            return lastnumber;
        }

        private void AddToMemory(in int number, in int turn)
        {
            // Console.Write($"{number},");
            if (_memory.ContainsKey(number))
            {
                _memory[number].Add(turn);
            }
            else
            {
                _memory.Add(number, new List<int>() { turn });
            }
        }

        public long Part2()
        {
            var input = ParseInput();
            _memory = new Dictionary<int, List<int>>();

            int turn = 1;
            int lastnumber = 0;

            foreach (var number in input)
            {
                AddToMemory(number, turn);
                lastnumber = number;
                turn++;
            }

            while (turn <= 30000000)
            {
                if (!_memory.ContainsKey(lastnumber) || _memory[lastnumber].Count == 1)
                {
                    lastnumber = 0;
                }
                else
                {
                    var numbermemory = _memory[lastnumber].TakeLast(2).ToArray();
                    var difference = numbermemory[1] - numbermemory[0];
                    lastnumber = difference;
                }

                AddToMemory(lastnumber, turn);
                turn++;
            }
            //  Console.WriteLine();
            return lastnumber;
        }

        public IEnumerable<int> ParseInput()
        {
            return File.ReadAllLines($"Day15.txt").First().Split(",").Select(int.Parse);

        }
    }
}