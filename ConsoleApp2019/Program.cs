using Common;
using System;
using System.Reflection;
using System.Linq;

namespace ConsoleApp2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayClasses = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(mytype => mytype.GetInterfaces().Contains(typeof(IDay))).OrderBy(t => t.Name);

            foreach (var dayClass in dayClasses)
            {
                Console.WriteLine(dayClass.Name);

                IDay day = (IDay)Activator.CreateInstance(dayClass);
                Console.WriteLine("Part1 : " + day.Part1());
                Console.WriteLine("Part2 : " + day.Part2());
            }

            Console.ReadLine();
        }
    }
}
