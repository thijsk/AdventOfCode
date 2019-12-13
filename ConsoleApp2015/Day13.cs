using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Rule
    {
        public string Name;
        public string NextToName;
        public int Happiness;
    }

    class Day13 : IDay
    {
        private string input = @"Alice would lose 2 happiness units by sitting next to Bob.
Alice would lose 62 happiness units by sitting next to Carol.
Alice would gain 65 happiness units by sitting next to David.
Alice would gain 21 happiness units by sitting next to Eric.
Alice would lose 81 happiness units by sitting next to Frank.
Alice would lose 4 happiness units by sitting next to George.
Alice would lose 80 happiness units by sitting next to Mallory.
Bob would gain 93 happiness units by sitting next to Alice.
Bob would gain 19 happiness units by sitting next to Carol.
Bob would gain 5 happiness units by sitting next to David.
Bob would gain 49 happiness units by sitting next to Eric.
Bob would gain 68 happiness units by sitting next to Frank.
Bob would gain 23 happiness units by sitting next to George.
Bob would gain 29 happiness units by sitting next to Mallory.
Carol would lose 54 happiness units by sitting next to Alice.
Carol would lose 70 happiness units by sitting next to Bob.
Carol would lose 37 happiness units by sitting next to David.
Carol would lose 46 happiness units by sitting next to Eric.
Carol would gain 33 happiness units by sitting next to Frank.
Carol would lose 35 happiness units by sitting next to George.
Carol would gain 10 happiness units by sitting next to Mallory.
David would gain 43 happiness units by sitting next to Alice.
David would lose 96 happiness units by sitting next to Bob.
David would lose 53 happiness units by sitting next to Carol.
David would lose 30 happiness units by sitting next to Eric.
David would lose 12 happiness units by sitting next to Frank.
David would gain 75 happiness units by sitting next to George.
David would lose 20 happiness units by sitting next to Mallory.
Eric would gain 8 happiness units by sitting next to Alice.
Eric would lose 89 happiness units by sitting next to Bob.
Eric would lose 69 happiness units by sitting next to Carol.
Eric would lose 34 happiness units by sitting next to David.
Eric would gain 95 happiness units by sitting next to Frank.
Eric would gain 34 happiness units by sitting next to George.
Eric would lose 99 happiness units by sitting next to Mallory.
Frank would lose 97 happiness units by sitting next to Alice.
Frank would gain 6 happiness units by sitting next to Bob.
Frank would lose 9 happiness units by sitting next to Carol.
Frank would gain 56 happiness units by sitting next to David.
Frank would lose 17 happiness units by sitting next to Eric.
Frank would gain 18 happiness units by sitting next to George.
Frank would lose 56 happiness units by sitting next to Mallory.
George would gain 45 happiness units by sitting next to Alice.
George would gain 76 happiness units by sitting next to Bob.
George would gain 63 happiness units by sitting next to Carol.
George would gain 54 happiness units by sitting next to David.
George would gain 54 happiness units by sitting next to Eric.
George would gain 30 happiness units by sitting next to Frank.
George would gain 7 happiness units by sitting next to Mallory.
Mallory would gain 31 happiness units by sitting next to Alice.
Mallory would lose 32 happiness units by sitting next to Bob.
Mallory would gain 95 happiness units by sitting next to Carol.
Mallory would gain 91 happiness units by sitting next to David.
Mallory would lose 66 happiness units by sitting next to Eric.
Mallory would lose 75 happiness units by sitting next to Frank.
Mallory would lose 99 happiness units by sitting next to George.";

        public int Part1()
        {
            List<Rule> rules = Parse(input);

            var persons = rules.Select(r => r.Name).Distinct();

            var permutations = persons.GetPermutations(persons.Count());

            int highest = 0;
//            IEnumerable<string> highestpersons ;
            foreach (var permutation in permutations)
            {
                int happyness = CalculateHappyness(permutation, rules);
                if (happyness > highest)
                {
                    highest = happyness;
                }
            }

            return highest;
        }

        private int CalculateHappyness(IEnumerable<string> permutation, List<Rule> rules)
        {
            LinkedList<string> setting = new LinkedList<string>(permutation);
            int totalhappyness = 0;
            foreach (var person in setting)
            {
                var item = setting.Find(person);
                var next = item.NextOrFirst().Value;
                var prev = item.PreviousOrLast().Value;
                var rulesum = rules.Where(r => r.Name == person && (r.NextToName == next || r.NextToName == prev)).Select(r => r.Happiness)
                    .Sum();
                totalhappyness += rulesum;
            }
            return totalhappyness;
        }

        private List<Rule> Parse(string rulestxt)
        {
            var result = new List<Rule>();
            foreach (var ruletxt in rulestxt.Split('\n'))
            {
                var parts = ruletxt.Split(' ');
                var rule = new Rule();
                rule.Name = parts[0];
                rule.NextToName = parts[10].Trim().Trim('.');
                rule.Happiness = int.Parse(parts[3]) * (parts[2] == "lose" ? -1 : 1);
                result.Add(rule);
            }
            return result;
        }

        public int Part2()
        {
            List<Rule> rules = Parse(input);

            AddMe(rules);

            var persons = rules.Select(r => r.Name).Distinct();

            var permutations = persons.GetPermutations(persons.Count());

            int highest = 0;
            //            IEnumerable<string> highestpersons ;
            foreach (var permutation in permutations)
            {
                int happyness = CalculateHappyness(permutation, rules);
                if (happyness > highest)
                {
                    highest = happyness;
                }
            }

            return highest;
        }

        private void AddMe(List<Rule> rules)
        {
            var names = rules.Select(r => r.Name).Distinct().ToArray();
            foreach (var name in names)
            {
                var rule1 = new Rule();
                rule1.Name = "Me";
                rule1.NextToName = name;
                rules.Add(rule1);
                var rule2 = new Rule();
                rule2.Name = name;
                rule2.NextToName = "Me";
                rules.Add(rule2);
            }
        }
    }
}
