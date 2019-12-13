using System;
using Common;

namespace ConsoleApp2015
{
    class Day20 : IDay
    {
        private int input = 29000000;

        public int Part1()
        {
            //var presents = 0;
            //var housenumber = 1;
            ////var maxpresents = 0;
            //while (presents < input)
            //{
            //    presents = 0;
            //    housenumber++;
            //    for (int elf = housenumber; elf > 0 ; elf --)
            //    {
            //        if (housenumber % elf == 0)
            //        {
            //          presents+= (elf * 10);
            //        }
            //    }

            //    //if (presents > maxpresents)
            //    //{
            //    //    maxpresents = presents;
            //    //    Console.WriteLine($"{housenumber} : {presents}");
            //    //}

            //}

            var houses = new int[input/10];
            var min = int.MaxValue;

            for (var elf = 1; elf < int.MaxValue; elf++)
            {
                for (int housenumber = elf; housenumber < houses.Length; housenumber = unchecked(elf + housenumber))
                {
                    houses[housenumber] += elf * 10;
                    if (houses[housenumber] >= input)
                        min = Math.Min(min, housenumber);
                }
            }


            return min;
        }

        public int Part2()
        {
            var houses = new int[input/10];
            var min = int.MaxValue;

            for (var elf = 1; elf < int.MaxValue; elf++)
            {
                for (int housenumber = elf, c = 0; c < 50 && housenumber < houses.Length; housenumber = unchecked(elf + housenumber), ++c)
                {
                    houses[housenumber] += elf * 11;
                    if (houses[housenumber] >= input)
                        min = Math.Min(min, housenumber);
                }
            }
            return min;
        }
    }
}
