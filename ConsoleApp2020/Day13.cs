using System;
using System.Collections.Generic;
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