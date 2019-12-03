using Common;
using System;
using System.IO;

namespace ConsoleApp2019
{
    internal class Day1 : IDay
    {
        public Day1()
        {
        }

        public int Part1()
        {
            var totalFuel = 0;

            foreach (var line in File.ReadAllLines("day1.txt"))
            {
                int moduleMass = int.Parse(line);
                int moduleFuel = CalcFuelNeeded(moduleMass);
                totalFuel += moduleFuel;
            }
            return totalFuel;
        }

        private static int CalcFuelNeeded(int mass)
        {
            return (int)Math.Floor(mass / 3d) - 2;
        }

        public int Part2()
        {

            var totalFuel = 0;

            foreach (var line in File.ReadAllLines("day1.txt"))
            {
                int moduleMass = int.Parse(line);
                int moduleFuel = CalcFuelNeeded(moduleMass);
                var additionalFuel = moduleFuel;
                while (true)
                {
                    additionalFuel = CalcFuelNeeded(additionalFuel);
                    if (additionalFuel > 0)
                    {
                        moduleFuel += additionalFuel;
                    }
                    else break;
                } 
                                
                totalFuel += moduleFuel;
            }
            return totalFuel;
        }
    }
}