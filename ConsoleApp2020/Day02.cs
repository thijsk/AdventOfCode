using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    public class Day02 : IDay
    {
        private string[] _input;

        public Day02()
        {
            _input = File.ReadAllLines("Day02.txt");
        }

        public long Part1()
        {
            var result = _input.Select(l => new PasswordRule(l)).Count(r => r.Validate1());
            return result;
        }

        public long Part2()
        {
            var result = _input.Select(l => new PasswordRule(l)).Count(r => r.Validate2());
            return result;
        }
    }

    class PasswordRule
    {
        private string _password;
        private int _min;
        private int _max;
        private char _letter;

        public PasswordRule(string input)
        {
            var inputRule = input.Split(':');
            var rule = inputRule[0];
            _password = inputRule[1].Trim();
            var minmax = rule.Split(' ')[0];
            _min = int.Parse(minmax.Split('-')[0]);
            _max = int.Parse(minmax.Split('-')[1]);
            _letter = rule.Split(' ')[1][0];
        }

        public bool Validate1()
        {
            var i = _password.Count(c => c == _letter);
            return i >= _min && i <= _max;
        }

        public bool Validate2()
        {
            var l1 = _password[_min - 1];
            var l2 = _password[_max - 1];
            var valid = (l1 == _letter) ^ (l2 == _letter);
            return valid;
        }
    }

}
