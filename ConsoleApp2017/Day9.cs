using System.IO;
using Common;

namespace ConsoleApp2017
{
    class Day9 : IDay
    {
        public int Part1()
        {
            string input = File.ReadAllText("day9.txt");

            //input = "{{<a!>},{<a!>},{<a!>},{<ab>}}";

            int nestedGroupDepth = 0;
            int sum = 0;
            bool inGarbage = false;
            bool skipNext = false;

            foreach (var chr in input)
            {
                if (skipNext)
                {
                    skipNext = false;
                    continue;
                }
                if (chr == '!')
                {
                    skipNext = true;
                    continue;
                }

                if (!inGarbage)
                {
                  
                    if (chr == '{')
                    {
                        nestedGroupDepth++;
                    }
                    if (chr == '}')
                    {
                        sum += nestedGroupDepth;
                        nestedGroupDepth--;
                    }
                }
                if (chr == '<')
                {
                    inGarbage = true;
                    continue;
                }
                if (chr == '>')
                {
                    inGarbage = false;
                    continue;
                }

            }

            return sum;
        }

        public int Part2()
        {
            string input = File.ReadAllText("day9.txt");

          //  input = "<{o\"i!a,<{i<a>";

            int nestedGroupDepth = 0;
            int sum = 0;
            bool inGarbage = false;
            bool skipNext = false;
            
            foreach (var chr in input)
            {
                if (skipNext)
                {
                    skipNext = false;
                    continue;
                }
                if (chr == '!')
                {
                    skipNext = true;
                    continue;
                }

                if (!inGarbage)
                {

                    if (chr == '{')
                    {
                        nestedGroupDepth++;
                    }
                    if (chr == '}')
                    {
                        nestedGroupDepth--;
                    }
                }
                if (chr == '>')
                {
                    inGarbage = false;
                    continue;
                }

                if (inGarbage)
                {
                    sum++;
                }

                if (chr == '<')
                {
                    inGarbage = true;
                    continue;
                }
               
               

            }

            return sum;
        }
    }
}
