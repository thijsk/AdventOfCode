using System;
using Common;

namespace ConsoleApp2015
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day15();

            Console.WriteLine(day.Part1());
            Console.WriteLine(day.Part2());
            Console.ReadLine();
        }
    }
}
