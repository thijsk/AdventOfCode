using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day08 : IDay
    {
        public long Part1()
        {
            var instructions = ParseInput();

            var cpu = new Cpu();
            cpu.Instructions = instructions;

            try
            {
                cpu.Execute();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
            }
            

            return cpu.Accumulator;
        }

        public long Part2()
        {
            var instructions = ParseInput();

            var options = Permutate(instructions);

            foreach (var option in options)
            {
                var cpu = new Cpu();
                cpu.Instructions = option;

                try
                {
                    cpu.Execute();
                }
                catch (Exception e)
                {
                    continue;
                    //Console.WriteLine(e);
                }

                return cpu.Accumulator;
            }

            return -1;
        }

        private IEnumerable<List<(string,int)>> Permutate(List<(string, int)> instructions)
        {
            var result = new List<List<(string, int)>>();

            for (int index = 0; index < instructions.Count; index++)
            {
                if (instructions[index].Item1 == "jmp")
                {
                    var copy = instructions.ToArray();
                    copy[index].Item1 = "nop";
                    result.Add(copy.ToList());
                } else if (instructions[index].Item1 == "nop")
                {
                    var copy = instructions.ToArray();
                    copy[index].Item1 = "jmp";
                    result.Add(copy.ToList());
                }
            }

            return result;
        }


        private List<(string,int)> ParseInput()
        {
            return File.ReadAllLines("Day08.txt").Select(i => i.Split(" ")).Select(i => (i[0], int.Parse(i[1])))
                .ToList();
        }
    }

    internal class Cpu
    {
        public List<(string, int)> Instructions { get; set; }
        public long Accumulator { get; set; }

        private HashSet<int> _loopDetect;
        private int _pointer;

        public Cpu()
        {
            Accumulator = 0;
            _loopDetect = new HashSet<int>();
            _pointer = 0;
        }

        public void Execute()
        {
            while (true)
            {
                if (_pointer >= Instructions.Count)
                {
                    return;
                }

                if (_loopDetect.Contains(_pointer))
                {
                    throw new Exception("InfiniteLoop");
                }

                _loopDetect.Add(_pointer);
                var instruction = Instructions[_pointer];

                switch (instruction.Item1)
                {
                    case "nop":
                        _pointer++; 
                        continue;
                    case "jmp":
                        _pointer += instruction.Item2;
                        continue;
                    case "acc":
                        Accumulator += instruction.Item2;
                        _pointer++;
                        continue;
                    default:
                        Console.WriteLine($"Invalid instruction {instruction}");
                        break;
                }
            }
        }
    }
}
