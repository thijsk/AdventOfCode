using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace ConsoleApp2020
{
    public class Day04 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();
            return input.Count(p => p.IsValid());
        }

        public long Part2()
        {
            var input = ParseInput();
            return input.Count(p => p.IsValid2());
        }

        IEnumerable<Passport> ParseInput()
        {
            var passports = new List<Passport>();
            var passport = new Passport();

            var lines = File.ReadAllLines("Day04.txt");
            foreach (var line in lines)
            {
                if (line == "")
                {
                    passports.Add(passport);
                    passport = new Passport();
                }
                else
                {
                    passport.AddLine(line);
                }
            }
            passports.Add(passport);

            return passports;
        }
    }

    class Passport
    {
        private Dictionary<string, string> fields = new Dictionary<string, string>();

        private static bool ValidateByr(string value)
        {
            return true;
        }

        private static string[] valideyecolors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private Dictionary<string, Func<string, bool>> required = new Dictionary<string, Func<string, bool>>
        {
            {"byr", v =>
                {
                    var year = int.Parse(v);
                    return year.Between(1920, 2002);
                }
            },
            {
                "iyr", v =>
                {
                    var year = int.Parse(v);
                    return year.Between(2010, 2020);
                }
            }
            ,
            {
                "eyr", v =>
                {
                    var year = int.Parse(v);
                    return year.Between(2020, 2030);
                }
            },
            {
                "hgt", v =>
                {
                    if (v.EndsWith("cm"))
                    {
                        var l = int.Parse(v.Replace("cm", ""));
                        return l.Between(150, 193);
                    } else if (v.EndsWith("in"))
                    {
                        var l = int.Parse(v.Replace("in", ""));
                        return l.Between(59, 76);
                    }
                    else return false;
                }
            },
            {
                "hcl", v => Regex.IsMatch(v,@"^#[0-9a-f]{6}$")
            },
            {
                "ecl", v => valideyecolors.Contains(v)
            },
            {
                "pid", v => Regex.IsMatch(v, @"^\d{9}$")
            }
        };
        
        private string[] options = {"cid"};

        public void AddLine(string line)
        {
            foreach (var field in line.Split(" "))
            {
                AddField(field);
            }
        }

        void AddField(string pair)
        {
            var split = pair.Split(":");
            fields.Add(split[0], split[1]);
        }

        public bool IsValid()
        {
            foreach (var r in required)
            {
                if (!fields.ContainsKey(r.Key))
                    return false;
            }

            return true;
        }

        public bool IsValid2()
        {
            foreach (var r in required)
            {
                if (!fields.ContainsKey(r.Key))
                    return false;
                if (!r.Value(fields[r.Key]))
                    return false;
            }

            return true;
        
        }
    }
}
