using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day11 : IDay
    {
        private string input = "hepxcrrq";
        public long Part1()
        {
            //input = "aziabca";
            Console.WriteLine(input);

            var oldpwd = input.ToCharArray();
            var newpwd = oldpwd;
            while (true)
            {
                newpwd = Increment(oldpwd, input.Length - 1);

                if (!OnlyAllowedLetters(newpwd))
                    continue;

                if (!HasIncreasingStraight(newpwd))
                    continue;

                if (!HasTwoPairs(newpwd))
                    continue;

                break;
            }


            Console.WriteLine(String.Concat(newpwd));
            return 0;
        }

        private bool HasTwoPairs(char[] pwd)
        {
            var pairs = new List<string>();
            for (int i = 0; i < pwd.Length -1 ; i++)
            {
                if (pwd[i] == pwd[i + 1])
                {
                    pairs.Add(String.Concat(pwd[i], pwd[i]));
                }
            }

            int count = pairs.Distinct().Count();
            return count >= 2;
        }

        readonly char[] forbidden = { 'i', 'o', 'l' };
        private bool OnlyAllowedLetters(char[] pwd)
        {
            return pwd.All(c => !forbidden.Contains(c));
        }

        private bool HasIncreasingStraight(char[] pwd)
        {
            int count = 1;
            char last = pwd[0];
            for (int i = 1; i < pwd.Length; i++)
            {
                char cur = pwd[i];
                if (cur == last)
                    count++;
                else
                    count = 1;
                if (count == 3)
                    return true;
                last = cur;
                last++;
            }
            return false;
        }

        private char[] Increment(char[] s, int i)
        {
            var current = s[i];
            if (current == 'z')
            {
                s = Increment(s, i - 1);
                s[i] = 'a';
                return s;
            }
            s[i]++;
            return s;

        }

        public long Part2()
        {
            //input = "aziabca";
            Console.WriteLine(input);

            var oldpwd = input.ToCharArray();
            var newpwd = oldpwd;

            bool first = true;
            while (true)
            {
                newpwd = Increment(oldpwd, input.Length - 1);

                if (!OnlyAllowedLetters(newpwd))
                    continue;

                if (!HasIncreasingStraight(newpwd))
                    continue;

                if (!HasTwoPairs(newpwd))
                    continue;

                if (first)
                {
                    first = false;
                    continue;
                }
                break;
            }


            Console.WriteLine(String.Concat(newpwd));
            return 0;
        }
    }
}
