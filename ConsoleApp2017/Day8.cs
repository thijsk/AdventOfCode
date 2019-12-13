using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2017
{
    class Day8 : IDay
    {
        public long Part1()
        {
            var registers = new Dictionary<string, int>();
            var instructions = ParseInput();

            foreach (var instruction in instructions)
            {
                instruction.Execute(registers);
            }

            return registers.Values.Max();

        }

        public int GetRegister(Dictionary<string, int> registers, string name)
        {
            var value = 0;
            if (!registers.TryGetValue(name, out value))
            {
                registers.Add(name, value);
            }

            return value;
        }


        private List<Instruction> ParseInput()
        {
            var result = new List<Instruction>();
            //smi inc 781 if epx > -2
            foreach (var instructiontxt in File.ReadAllLines("day8.txt"))
            {
                var parts = instructiontxt.Split(' ');
                var instruction = new Instruction();
                instruction.Register = parts[0];
                var value = int.Parse(parts[2]);
                instruction.Value = parts[1] == "inc" ? value : value * -1;
                instruction.Condition = new Condition();
                instruction.Condition.Register = parts[4];
                instruction.Condition.Operand = parts[5];
                instruction.Condition.Value = int.Parse(parts[6]);
                result.Add(instruction);
            }
            return result;
        }

        public long Part2()
        {
            var registers = new Dictionary<string, int>();
            var instructions = ParseInput();

            var alltimeMax = 0;

            foreach (var instruction in instructions)
            {
                instruction.Execute(registers);
                alltimeMax = Math.Max(registers.Values.Max(), alltimeMax);
            }

            return alltimeMax;

        }
    }
    
    internal class Instruction
    {
        public int Value { get; set; }
        public string Register { get; set; }
        public Condition Condition { get; set; }

        public void Execute(Dictionary<string, int> registers)
        {
            if (Condition.Check(registers))
            {
                var currentValue = 0;
                if (!registers.TryGetValue(this.Register, out currentValue))
                {
                    registers.Add(this.Register, currentValue);
                }
                var newValue = currentValue + this.Value;
                registers[this.Register] = newValue;
            }
        }
    }

    internal class Condition
    {
        public string Register
        { get; set; }

        public string Operand { get; set; }
        public int Value { get; set; }

        public bool Check(Dictionary<string, int> registers)
        {
            var currentValue = 0;
            if (!registers.TryGetValue(this.Register, out currentValue))
            {
                registers.Add(this.Register, currentValue);
            }

            switch (Operand)
            {
                case "==":
                    return currentValue == Value;
                case "!=":
                    return currentValue != Value;
                case ">=":
                    return currentValue >= Value;
                case "<=":
                    return currentValue <= Value;
                case ">":
                    return currentValue > Value;
                case "<":
                    return currentValue < Value;
                default:
                    throw new NotImplementedException(Operand);
            }
        }
    }
}
