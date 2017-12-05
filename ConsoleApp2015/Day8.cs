using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp2015
{
    internal class Day8
    {
        private string input = File.ReadAllText("day8.txt");


        public int Part1()
        {
            //input = "\"\"\n\"abc\"\"aaa\\\"aaa\"\"\\x27\"";

            var numcode = 0;
            var nummem = 0;

            foreach (var raw in input.Split("\n"))
            {
                var line = raw.Trim();
                var linecode = line.Length;
                var linemem = 0;
                var i = -1;
                var isEscaped = false;
                while (i < line.Length -1 )
                {
                    i++;
                    if (isEscaped)
                    {
                        if (line[i] == 'x')
                            i += 2;
                        isEscaped = false;
                    }
                    else
                    {
                        if (line[i] == '"')
                            continue;
                        if (line[i] == '\\')
                            isEscaped = true;
                        linemem++;
                    }
                }
                numcode += linecode;
                nummem += linemem;
            }

            Console.WriteLine(numcode);
            Console.WriteLine(nummem);
            return numcode - nummem;
        }

        public int Part2()
        {
            //input = "\"\"\n\"abc\"\n\"aaa\\\"aaa\"\n\"\\x27\"";

            var numcode = 0;
            var numenc = 0;

            foreach (var raw in input.Split("\n"))
            {
                var line = raw.Trim();
                var linecode = line.Length;
                var lineenc = line.Length;
                foreach (var c in line)
                {
                    if (c == '\\' || c == '"')
                        lineenc++;
                }
                lineenc += 2;                

                numcode += linecode;
                numenc += lineenc;
            }

            Console.WriteLine(numcode);
            Console.WriteLine(numenc);
            return numenc - numcode;
        }
    }
}
