using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace ConsoleApp2015
{
    class Day20 : IDay
    {
        private int input = 36000000;

        public int Part1()
        {
            var presents = 0;
            var housenumber = 1;
            var maxpresents = 0;
            while (presents < input)
            {
                presents = 0;
                housenumber++;
                for (int elf = 1; elf <= housenumber; elf++)
                {
                    if (housenumber % elf == 0)
                    {
                      presents+= (elf * 10);
                    }
                }

                if (presents > maxpresents)
                {
                    maxpresents = presents;
                    Console.WriteLine($"{housenumber} : {presents}");

                }
               
            }

            return housenumber;
        }

        public int Part2()
        {
            return -2;
        }
    }
}
