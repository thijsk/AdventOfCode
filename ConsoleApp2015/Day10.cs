using System.Text;
using Common;

namespace ConsoleApp2015
{
    class Day10 : IDay
    {
        private string input = @"1321131112";

        public int Part1()
        {
            string str = input;

            for (int i = 1; i <= 40; i++)
            {
                str = Say(str);
            }

            //Console.WriteLine(str);

            return str.Length;
        }

        public int Part2()
        {
            string str = input;

            for (int i = 1; i <= 50; i++)
            {
               
                str = Say(str);
            }

            //Console.WriteLine(str);

            return str.Length;
        }

        private string Say(string str)
        {
            StringBuilder result = new StringBuilder();

            var lastChar = str[0];
            var charCount = 1;
            for (int i = 1; i < str.Length; i++)
            {
                var thisChar = str[i];
                if (thisChar == lastChar)
                {
                    charCount++;
                }
                else
                {
                    result.Append($"{charCount}{lastChar}");
                    charCount = 1;
                }
                lastChar = thisChar;
            }
            result.Append($"{charCount}{lastChar}");
            return result.ToString();
        }

    }
}
