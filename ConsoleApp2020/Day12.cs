using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day12 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var nav = new Navigation();
            nav.ExecuteInstructions(input);

            return Math.Abs(nav.ns) + Math.Abs(nav.ew);
        }

        public long Part2()
        {
            var input = ParseInput();

            var nav = new Navigation();
            nav.wpns = 1;
            nav.wpew = -10;
            nav.ExecuteInstructions2(input);

            return Math.Abs(nav.ns) + Math.Abs(nav.ew);
        }

        public IEnumerable<(string, int)> ParseInput()
        {
            return File.ReadAllLines($"Day12.txt").Select(l => (l.Substring(0, 1), int.Parse(l.Substring(1))));
        }
    }

    class Navigation
    {
        public int ew;
        public int ns;

        public int wpew;
        public int wpns;

        private string headings = "NESW";

        private string heading = "E";

        public void ExecuteInstructions(IEnumerable<(string, int)> instructions)
        {
            foreach (var instruction in instructions)
            {
                Execute(instruction);
                Console.WriteLine($"{instruction} : {heading} {ns},{ew}");
            }
        }

        public void ExecuteInstructions2(IEnumerable<(string, int)> instructions)
        {
            foreach (var instruction in instructions)
            {
                Execute2(instruction);
                Console.WriteLine($"{instruction} : ship: {ns},{ew} waypoint: {wpns},{wpew}");
            }
        }

        private void Execute2((string, int) instruction)
        {
            switch (instruction.Item1)
            {
                case "N":
                    wpns += instruction.Item2;
                    break;
                case "E":
                    wpew -= instruction.Item2;
                    break;
                case "S":
                    wpns -= instruction.Item2;
                    break;
                case "W":
                    wpew += instruction.Item2;
                    break;
                case "R":
                    {
                        var rotation = instruction.Item2 / 90;
                        
                        while (rotation > 0)
                        {
                            var curew = wpew;
                            var curns = wpns;
                            wpew = -1 * curns;
                            wpns = curew;

                            rotation--;
                        }
                    }
                    break;
                case "L":
                    {
                        var rotation = instruction.Item2 / 90;
                        
                        while (rotation > 0)
                        {
                            var curew = wpew;
                            var curns = wpns;
                            wpew = curns;
                            wpns = -1 * curew;

                            rotation--;
                        }
                    }
                    break;
                case "F":
                    ew += instruction.Item2 * wpew;
                    ns += instruction.Item2 * wpns;
                    break;
                default:
                    throw new Exception($"Invalid instruction {instruction.Item1}");
            }
        }


        private void Execute((string, int) instruction)
        {
            switch (instruction.Item1)
            {
                case "N":
                    //   heading = "N";
                    ns += instruction.Item2;
                    break;
                case "E":
                    //   heading = "E";
                    ew -= instruction.Item2;
                    break;
                case "S":
                    //  heading = "S";
                    ns -= instruction.Item2;
                    break;
                case "W":
                    //  heading = "W";
                    ew += instruction.Item2;
                    break;
                case "L":
                    heading = headings[(headings.IndexOf(heading) + 4 - (instruction.Item2 / 90)) % 4].ToString();
                    break;
                case "R":
                    heading = headings[(headings.IndexOf(heading) + instruction.Item2 / 90) % 4].ToString();
                    break;
                case "F":
                    Execute((heading, instruction.Item2));
                    break;
                default:
                    throw new Exception($"Invalid instruction {instruction.Item1}");
            }
        }
    }
}