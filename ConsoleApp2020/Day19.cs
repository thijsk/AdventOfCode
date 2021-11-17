using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Common;

namespace ConsoleApp2020
{
    class Day19 : IDay
    {


        public long Part1()
        {
            return 0;
            var input = ParseInput();


            var ok = 0;
            foreach (var message in input.Item2)
            {
                var rule0 = input.Item1[0];
                var index = 0;
                var valid = rule0.IsValid(message, ref index);
                valid = valid && message.Length == index;
                ConsoleX.WriteLine($"{message} : {valid}");
                if (valid)
                {
                    ok++;
                }
            }

            return ok;
        }

        public long Part2()
        {
            var input = ParseInput();

            var rule0str = new StringBuilder();
            rule0str.Append("0: ");
            for (int i8 = 1; i8 < 10; i8++)
            {
                for (int i11 = 1; i11 < 10; i11++)
                {
                    var r8 = " 42".Repeat(i8);
                    var r11l = " 42".Repeat(i11);
                    var r11r = " 31".Repeat(i11);
                    rule0str.Append($"{r8}{r11l}{r11r} |");
                    //8: 42 | 42 8
                    //11: 42 31 | 42 11 31

                }
            }

            input.Item1[0].UpdateRule(rule0str.ToString());

            var ok = 0;
            foreach (var message in input.Item2)
            {
                var rule0 = input.Item1[0];
                var index = 0;
                var valid = rule0.IsValid(message, ref index);
                var length = message.Length == index;
                ConsoleX.WriteLine($"{message} : {valid} {length}");
                if (valid && length)
                {
                    ok++;
                }
            }

            return ok;
        }

        public (Dictionary<int, MessageRule>, List<string>) ParseInput()
        {
            var linebreak = false;
            var rulebook = new Dictionary<int, MessageRule>();
            var messages = new List<string>();
            foreach (var line in File.ReadAllLines($"Day19.txt"))
            {
                if (string.IsNullOrEmpty(line))
                {
                    linebreak = true;
                    continue;
                }

                if (!linebreak)
                {
                    MessageRule.AddToRunbook(rulebook, line);
                }
                else
                {
                    messages.Add(line);
                }
            }

            return (rulebook, messages);
        }

    }

    internal class MessageRule
    {
        public static void AddToRunbook(Dictionary<int, MessageRule> rulebook, string line)
        {
            var rule = new MessageRule(line);
            rule.Rulebook = rulebook;
            rulebook.Add(rule.Number, rule);
        }

        private char _char = '?';
        private List<List<int>> _lists;
        private static int Depth = 0;

        public int Number { get; set; }

        private string _line;
        public Dictionary<int, MessageRule> Rulebook { get; set; }

        public MessageRule(string line)
        {
            UpdateRule(line);
        }

        public void UpdateRule(string line)
        {
            _lists = new List<List<int>>();
            _line = line;

            var split1 = line.Split(": ");
            Number = int.Parse(split1[0]);

            if (split1[1].StartsWith('"'))
            {
                _char = split1[1][1];
            }
            else
            {
                var tmp = split1[1];
                var split2 = tmp.Split("|", StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in split2)
                {
                    var list = t.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    _lists.Add(list);
                }
            }
        }


        public static void Debug(string s)
        {
            if (Debugger.IsAttached)
            {
                var spaces = "".PadRight(Depth, ' ');
                ConsoleX.WriteLine($"{spaces}{s}");
            }
        }

        public bool IsValid(string message, ref int fromIndex)
        {
            Debug($"{Number} starting: {_line} with message {message.Substring(fromIndex)}");
            if (Depth == 50)
            {
                return false;
            }

            bool result;
            if (_char != '?')
            {
                if (fromIndex > message.Length - 1)
                {
                    Debug($"{Number} : False - message too short");
                    return false;
                }

                result = message[fromIndex] == _char;
                Debug($"{Number} Done : {result} char at {fromIndex} is '{message[fromIndex]}'");
                if (result)
                {
                    fromIndex++;
                }

                return result;
            }

            var startIndex = fromIndex;
            result = false;
            foreach (var rules in _lists)
            {
                fromIndex = startIndex;
                foreach (var rule in rules)
                {
                    Depth++;
                    result = Rulebook[rule].IsValid(message, ref fromIndex);
                    Depth--;
                    if (!result)
                        break;
                }
                if (result)
                {
                    break;
                }
            }

            Debug($"{Number} Done : {result}");
            return result;

        }



    }
}