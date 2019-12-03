using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2017
{
    class Day12 : IDay
    {

        class Program
        {
            public int Number;
            public List<Program> Pipes = new List<Program>();
        }

        public int Part1()
        {
            var programs = new List<Program>();

            foreach (var line in File.ReadAllLines("day12.txt"))
            {
                var parts = line.Split(" <-> ");
                var program = FindOrCreate(programs, int.Parse(parts[0]));
                program.Pipes = parts[1].Split(',').Select(p => int.Parse(p.Trim())).Select(p => FindOrCreate(programs, p)).ToList();
                foreach (var other in program.Pipes)
                {
                    other.Pipes = other.Pipes.Union(new[] {program}).ToList();
                }
                programs.Add(program);
            }

            var todo = new Stack<Program>();
            todo.Push(programs.First(p => p.Number == 0));
            var found = new List<Program>();
            while (todo.Any())
            {
                var current = todo.Pop();
                if (!found.Any(f => f.Number == current.Number))
                {
                    found.Add(current);
                }
                var more = current.Pipes.Where(p => !found.Exists(f => f.Number == p.Number));
                foreach (var m in more)
                {
                    todo.Push(m);
                }
            }


            return found.Count();

        }

        public int Part2()
        {
            var programs = new List<Program>();

            foreach (var line in File.ReadAllLines("day12.txt"))
            {
                var parts = line.Split(" <-> ");
                var program = FindOrCreate(programs, int.Parse(parts[0]));
                program.Pipes = parts[1].Split(',').Select(p => int.Parse(p.Trim())).Select(p => FindOrCreate(programs, p)).ToList();
                foreach (var other in program.Pipes)
                {
                    other.Pipes = other.Pipes.Union(new[] { program }).ToList();
                }
                programs.Add(program);
            }



            var groupCount = 0;
            var startProgram = programs.First(p => p.Number == 0);
            var allInGroup = new List<Program>();
            while (startProgram != null)
            {
                var group = GetGroup(programs, startProgram);
                allInGroup.AddRange(group);
                groupCount++;

                startProgram = programs.FirstOrDefault(p => !allInGroup.Any(g => g.Number == p.Number));
            }

            return groupCount;

        }

        private static List<Program> GetGroup(List<Program> programs, Program start)
        {
            var todo = new Stack<Program>();
            todo.Push(start);
            var found = new List<Program>();
            while (todo.Any())
            {
                var current = todo.Pop();
                if (!found.Any(f => f.Number == current.Number))
                {
                    found.Add(current);
                }
                var more = current.Pipes.Where(p => !found.Exists(f => f.Number == p.Number));
                foreach (var m in more)
                {
                    todo.Push(m);
                }
            }
            return found;
        }

        private Program FindOrCreate(List<Program> programs, int number)
        {
            var program = programs.FirstOrDefault(p => p.Number == number);
            if (program == null)
            {
                program = new Program();
                program.Number = number;
            }
            return program;
        }
        
    }
}
