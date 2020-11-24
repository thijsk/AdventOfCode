using System;
using System.Linq;
using System.Reflection;
using Common;

namespace ConsoleApp2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IDay))).OrderBy(t => int.Parse(t.Name.Substring(3)));

            foreach (var dayClass in dayClasses.TakeLast(1))
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
