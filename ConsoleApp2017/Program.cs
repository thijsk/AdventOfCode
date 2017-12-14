using System;
using Common;

namespace ConsoleApp2017
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day9();
            Console.WriteLine("Part1 : " + day.Part1());
            Console.WriteLine("Part2 : " + day.Part2());
            Console.ReadLine();
        }
    }
   
}
