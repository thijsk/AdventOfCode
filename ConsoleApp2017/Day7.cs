using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2017
{
    internal class Day7 : IDay
    {
        public int Part1()
        {
            var programlist = ParseInput();

            var roots = programlist.Where(p => !programlist.Any(i => i.ChildNames.Contains(p.Name)));
            var root = roots.First();
            Console.WriteLine(root.Name);
            return root.Weight;
        }

        public int Part2()
        {
            var programlist = ParseInput();
            var root = programlist.Where(p => !programlist.Any(i => i.ChildNames.Contains(p.Name))).First();

            FixChildren(root, programlist);

            var fixMe = FindWrongWeigth(root);

            var goodWeight = fixMe.Parent.Children.First(c => c.TotalWeight != (fixMe.Weight + fixMe.TotalWeight))
                .TotalWeight;
            var childWeight = fixMe.Children.Sum(c => c.TotalWeight);

            fixMe.Weight = goodWeight - childWeight;

            //var result = FindWrongWeigth(root); // returns root

            return goodWeight - childWeight;
        }

        private Program FindWrongWeigth(Program root)
        {
            var balanced = root.Children.Select(c => c.TotalWeight).Distinct().Count() == 1;

            if (!balanced)
            {
                var groups = root.Children.GroupBy(c => c.TotalWeight, c => c);

                var wrong = groups.First(g => g.Count() == 1);
                return FindWrongWeigth(groups.First(g => g.Key == wrong.Key).First());
            }

            return root;
        }

        private void FixChildren(Program root, List<Program> programlist)
        {
            foreach (var childName in root.ChildNames)
            {
                var child = programlist.Find(p => p.Name == childName);
                root.Children.Add(child);
                child.Parent = root;
                FixChildren(child, programlist);
            }
        }

        class Program
        {
            public string Name;
            public string[] ChildNames = new string[]{};
            public readonly List<Program> Children = new List<Program>();
            public int Weight;
            public Program Parent;

            public int TotalWeight =>
                (Weight + Children.Sum(c => c.TotalWeight));
        
        }


        private List<Program> ParseInput()
        {
            var result = new List<Program>();
            foreach (var programtxt in File.ReadAllLines("day7.txt"))
            {
                var p = new Program();
                var parts = programtxt.Split(" -> ");
                var nameweight = parts[0].Split(' ');

                p.Name = nameweight[0].Trim();
                p.Weight = int.Parse(nameweight[1].Trim().TrimStart('(').TrimEnd(')'));

                if (parts.Length > 1)
                    p.ChildNames = parts[1].Split(',').Select(n => n.Trim()).ToArray();

                result.Add(p);
            }

            return result;
        }

        
    }
}