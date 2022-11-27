using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day16 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var rules = input.Item1;
            var myTicket = input.Item2;
            var nearbyTickets = input.Item3;

            var rate = 0;
            foreach (var ticket in nearbyTickets)
            {
                rate += ticket.GetScanningErrorRate(rules);
            }

            return rate;
        }

        public long Part2()
        {
            var input = ParseInput();

            var rules = input.Item1;
            var myTicket = input.Item2;
            var nearbyTickets = input.Item3;


            var validTickets = new List<Ticket>();
            foreach (var ticket in nearbyTickets)
            {
                if (ticket.CanFit(rules))
                {
                    validTickets.Add(ticket);
                }
            }

            validTickets.Add(myTicket);

            var ruleToField = new Dictionary<TicketRule, List<int>>();
            var fields = Enumerable.Range(0, myTicket.Fields.Count).ToList();

            foreach (var rule in rules)
            {
                var ruleFields = new List<int>();
                foreach (var field in fields)
                {
                    if (validTickets.All(t => rule.ValueIsValid(t.Fields[field])))
                    {
                        ruleFields.Add(field);
                    }
                }
                ruleToField.Add(rule, ruleFields);
            }


            var fieldToRule = new Dictionary<int, List<TicketRule>>();
            foreach (var field in fields)
            {
                var fieldRules = new List<TicketRule>();
                foreach (var rule in rules)
                {
                    if (validTickets.All(t => rule.ValueIsValid(t.Fields[field])))
                    {
                        fieldRules.Add(rule);
                    }
                }
                fieldToRule.Add(field, fieldRules);
            }


            var correctRules = new Dictionary<TicketRule, int>();

            while (ruleToField.Any())
            {
                foreach (var rule in ruleToField.Keys.ToArray())
                {
                    if (ruleToField[rule].Count == 1)
                    {
                        correctRules.Add(rule, ruleToField[rule].First());
                        ruleToField.Remove(rule);
                        continue;
                    }

                    foreach (var field in ruleToField[rule].ToArray())
                    {
                        // als er een rule is, waar dit field het enige field is, dan deze weghalen bij de huidige rule
                        if (correctRules.Values.Contains(field))
                        {
                            ruleToField[rule].Remove(field);
                        }

                        if (ruleToField[rule].Count == 0)
                        {
                            Console.WriteLine("Help");
                        }
                    }
                }
            }

            long result = 1;
            foreach (var rule in correctRules)
            {
                if (rule.Key.Name.StartsWith("departure"))
                {
                    var fieldValue = myTicket.Fields[rule.Value];
                    Console.WriteLine($"{rule.Key.Name} - {myTicket.Fields[rule.Value]}");

                    result *= fieldValue;
                }
            }

            return result;

        }

        public (List<TicketRule>, Ticket, List<Ticket>) ParseInput()
        {
            int section = 0;

            var rules = new List<TicketRule>();
            Ticket myTicket = null;
            var nearbyTickets = new List<Ticket>();
            foreach (var line in File.ReadAllLines($"Day16.txt"))
            {
                if (section == 0)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        section++;
                        continue;
                    }

                    rules.Add(new TicketRule(line));
                }
                else if (section == 1)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        section++;
                        continue;
                    }
                    if (line == "your ticket:")
                    {
                        continue;
                    }

                    myTicket = new Ticket(line);
                }
                else
                {
                    if (line == "nearby tickets:")
                    {
                        continue;
                    }

                    nearbyTickets.Add(new Ticket(line));
                }
            }

            return (rules, myTicket, nearbyTickets);
        }

        internal class Ticket
        {
            public List<int> Fields;

            public Ticket(string line)
            {
                Fields = line.Split(',').Select(int.Parse).ToList();
            }

            public int GetScanningErrorRate(List<TicketRule> rules)
            {
                var rate = 0;
                foreach (var field in Fields)
                {
                    if (!rules.Any(r => r.ValueIsValid(field)))
                    {
                        rate += field;
                    }
                }

                return rate;
            }

            public bool CanFit(List<TicketRule> rules)
            {
                var rate = 0;
                foreach (var field in Fields)
                {
                    if (!rules.Any(r => r.ValueIsValid(field)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        internal class TicketRule
        {
            public string Name;
            private int _min1;
            private int _min2;
            private int _max1;
            private int _max2;

            public TicketRule(string line)
            {
                var split = line.Split(": ");
                Name = split[0];
                var ranges = split[1].Split(" or ");
                var range1 = ranges[0].Split("-");
                _min1 = int.Parse(range1[0]);
                _max1 = int.Parse(range1[1]);
                var range2 = ranges[1].Split("-");
                _min2 = int.Parse(range2[0]);
                _max2 = int.Parse(range2[1]);
            }

            public bool ValueIsValid(int value)
            {
                return value.Between(_min1, _max1) | value.Between(_min2, _max2);
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}