using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day13 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var time = input.Item1;

            while (true)
            {
                foreach (var bus in input.Item2)
                {
                    if (bus != -1)
                    {
                        if (time % bus == 0)
                        {
                            return bus * (time - input.Item1);
                        }
                    }
                }

                time++;
            }

            return 0;
        }

        public long FinalAttempt()
        {
            var input = ParseInput();
            var busses = input.Item2.Select((bus, index) => (bus, index)).Where(b => b.Item1 != -1).OrderByDescending(b => b.Item1).ToList();


            //We start at the highest bus number, should be faster
            long period = busses[0].bus;
            long time = period - busses[0].index;

            for (var count = 1; count <= busses.Count; count++)
            {
                // While not all busses match, add more time
                while (busses.Take(count).Any(bus => (time + bus.index) % bus.bus != 0))
                {
                    time += period;
                }

                // now the first {count} busses are in sync, so the period is now a multiple of their numbers
                period = busses.Take(count).Select(b => (long) b.bus).Aggregate((long) 1, (i, j) => i * j);
            }

            return time;
        }

        public long Part2()
        {
            var input = ParseInput();
            var bustable = input.Item2.ToArray();
            var busses = new List<int>();
            var offsets = new List<int>();
            var a = new List<int>();
            for (int i = 0; i < bustable.Length; i++)
            {
                var bus = bustable[i];
                if (bus != -1)
                {
                    busses.Add(bus);
                    offsets.Add(i);
                    a.Add(bus - i);
                }
            }

            Console.WriteLine("Put this in wolfram alpha:");
            for (int i = 0; i < busses.Count; i++)
            {

                if (i > 0)
                    Console.Write(", ");
                Console.Write($"(t + {offsets[i]}) mod {busses[i]} = 0");

            }

            Console.WriteLine();

            var result = ChineseRemainderTheorem.Solve(busses.ToArray(), a.ToArray());
            Console.WriteLine(result);

            result = Reddit(busses, offsets);
            Console.WriteLine(result);

            return FinalAttempt();
        }

        private static long Reddit(List<int> busses, List<int> offsets)
        {
            long time = 0;
            long advanceBy = busses[0];
            int correctBusIndex = 0;
            while (true)
            {
                bool found = true;
                for (int i = correctBusIndex + 1; i < busses.Count; i++)
                {
                    int bus = busses[i];
                    int offset = offsets[i] % busses[i];
                    if (TimeLeft(bus, time) == offset)
                    {
                        advanceBy *= bus;
                        correctBusIndex = i;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return time;
                }

                time += advanceBy;
            }
        }


        static int TimeLeft(int bus, long time)
        {
            int timeLeft = (int)(time % bus);
            if (timeLeft > 0)
            {
                timeLeft = bus - timeLeft;
            }
            return timeLeft;
        }

        public (int, IEnumerable<int>) ParseInput()
        {
            var lines = File.ReadAllLines($"Day13.txt");
            var time = int.Parse(lines[0]);
            var busses = lines[1].Split(',').Select(b => b == "x" ? -1 : int.Parse(b));
            return (time, busses);
        }
    }
}