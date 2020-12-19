using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day19 : IDay
    {


        public long Part1()
        {
            var input = ParseInput();


            var ok = 0;
            foreach (var message in input.Item2)
            {
                var rule0 = input.Item1[0];
                var index = 0;
                var valid = rule0.IsValid(message, ref index);
                valid = valid && message.Length == index;
                Console.WriteLine($"{message} : {valid}");
                if (valid)
                {
                    ok++;
                }
            }

            return ok;
        }

        public long Part2()
        {
            var why = new Cheat();
            why.IsThisSoHard(File.ReadAllLines($"Day19.txt").ToList());


            var input = ParseInput2();


            var ok = 0;
            foreach (var message in input.Item2)
            {
                var rule0 = input.Item1[0];
                var index = 0;
                var valid = rule0.IsValid(message.Backwards(), ref index);
                var length = message.Length == index;
                Console.WriteLine($"{message} : {valid} {length}");
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
                    MessageRule.AddToRunbook(rulebook, line, false);
                }
                else
                { 
                    messages.Add(line);
                }
            }

            return (rulebook, messages);
        }

        public (Dictionary<int, MessageRule>, List<string>) ParseInput2()
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
                    if (line.StartsWith("8:"))
                    {
                        MessageRule.AddToRunbook(rulebook, "8: 42 | 42 42 | 42 42 | 42 42 42 | 42 42 42 42 | 42 42 42 42 42", true);
                    }
                    else if (line.StartsWith("11:"))
                    {
                        MessageRule.AddToRunbook(rulebook, "11: 42 31 | 42 42 31 31 | 42 42 31 31 |42 42 42 31 31 31 ", true);
                    }
                    else
                    {
                        MessageRule.AddToRunbook(rulebook, line, true);
                    }
                    
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
        public static void AddToRunbook(Dictionary<int, MessageRule> rulebook, string line, bool reverse)
        {
            var rule = new MessageRule(line, true);
            rule.Rulebook = rulebook;
            rulebook.Add(rule.Number, rule);
        }

        private char _char = '?';
        private List<int> _list1;
        private List<int> _list2;
        private static int Depth = 0;


        public int Number { get; set; }

        private string _line;
        public Dictionary<int, MessageRule> Rulebook { get; set; }

        public MessageRule(string line, bool reverse)
        {
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
                var split2 = tmp.Split("|");
                _list1 = split2[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if (split2.Length > 1)
                {
                    _list2 = split2[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                }
                else
                {
                    _list2 = new List<int>();
                }
                if (reverse)
                {
                    _list1.Reverse();
                    _list2.Reverse();
                }
            }
        }


        public static void Debug(string s)
        {
            if (Debugger.IsAttached)
            {
                var spaces = "".PadRight(Depth, ' ');
                Console.WriteLine($"{spaces}{s}");
            }
        }

        public bool IsValid(string message, ref int fromIndex)
        {
            Debug($"{Number} starting: {_line} with message {message.Substring(fromIndex)}");
            if (Depth == 50)
            {
                return false;
            }

            if (_char != '?')
            {
                if (fromIndex > message.Length - 1)
                {
                    Debug($"{Number} : False - message too short");
                    return false;
                }

                var result = message[fromIndex] == _char;
                Debug($"{Number} Done : {result} char at {fromIndex} is '{message[fromIndex]}'");
                if (result)
                {
                    fromIndex++;
                }
                
                return result;
            }

            var startIndex = fromIndex;
            var result1 = false;
            foreach (var rule in _list1)
            {
                Depth++;
                result1 = Rulebook[rule].IsValid(message, ref fromIndex);
                Depth--;
                if (!result1)
                    break;
            }
            var result1Index = fromIndex;

            fromIndex = startIndex;
            var result2 = false;
            foreach (var rule in _list2)
            {
                Depth++;
                result2 = Rulebook[rule].IsValid(message, ref fromIndex);
                Depth--;
                if (!result2)
                    break;
            }
            var result2Index = fromIndex;

            if (result1 && result2)
            {
                fromIndex = Math.Min(result1Index, result2Index);
            } else if (result1)
            {
                fromIndex = result1Index;
            }
            else
            {
                fromIndex = result2Index;
            }

            Debug($"{Number} Done : {result1}||{result2}");
            return result1 || result2;

        }



    }
    internal class Cheat
    {
        public void IsThisSoHard(List<string> input)
        {
            Dictionary<int, string> rules = new Dictionary<int, string>();
            List<string> msg = new List<string>();

            foreach (var item in input)
            {
                if (item != "" && item.Contains(":"))
                {
                    rules[int.Parse(item.Split(':')[0])] = item.Split(':')[1].Substring(1);
                }
                else if (item != "")
                {
                    msg.Add(item);
                }
            }

            string rule = rules[0];
            Regex regex = new Regex(@"\d+", RegexOptions.Compiled);
            while (true)
            {
                Match match = regex.Match(rule);
                if (match.Success)
                {
                    string thing = rules[int.Parse(match.Value)];
                    if (thing.Contains("\""))
                    {
                        thing = thing.Substring(1, thing.Length - 2);
                    }
                    else
                    {
                        thing = "(" + thing + ")";
                    }
                    rule = regex.Replace(rule, thing, 1);
                }
                else
                {
                    break;
                }
                //Console.WriteLine(rule);
            }
            rule = rule.Replace(" ", "");
            //Console.WriteLine(rule);
            int valid = 0;
            foreach (var item in msg)
            {
                if (Regex.IsMatch(item, "^" + rule + "$")) valid++;
            }
            Console.WriteLine(valid);


            rules[8] = "42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42))))))))))";
            rules[11] = "42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 31) 31) 31) 31) 31) 31) 31) 31) 31) 31) 31";

            rule = rules[0];
            while (true)
            {
                Match match = regex.Match(rule);
                if (match.Success)
                {
                    string thing = rules[int.Parse(match.Value)];

                    if (thing.Contains("\""))
                    {
                        thing = thing.Substring(1, thing.Length - 2);
                    }
                    else
                    {
                        thing = "(" + thing + ")";
                    }
                    rule = regex.Replace(rule, thing, 1);
                }
                else
                {
                    break;
                }
                //Console.WriteLine(rule);
            }
            rule = rule.Replace(" ", "");
            //Console.WriteLine(rule);
            valid = 0;
            foreach (var item in msg)
            {
                if (Regex.IsMatch(item, "^" + rule + "$")) valid++;
            }
            Console.WriteLine(valid);


            /*rulesParsed[0] = ValidMessages(0);
			;

			foreach (var item in rulesParsed[0])
			{
				Console.WriteLine(item);
			}*/
            /*List<string> ValidMessages(int ID)
			{
				List<string> accept = new List<string>();
				string rule = rules[ID];
				if (rulesParsed.ContainsKey(ID))
				{
					return rulesParsed[ID];
				}
				if (rule.StartsWith("\""))
				{
					accept.Add(rules[ID].Split('"')[1]);
				}
				else
				{
					string[] subrule = rule.Split('|');
					foreach (string item in subrule)
					{
						string aaa = "";
						string[] subsubrule = item.Split(' ');
						foreach (string subitem in subsubrule)
						{
							if (subitem != "")
							{
								foreach (var piece in ValidMessages(int.Parse(subitem)))
								{
									aaa += piece;

								}

							}
						}
						accept.Add(aaa);
					}
				}
				rulesParsed[ID] = accept;
				return accept;
			}*/
        }
    }

}