using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2019
{
    class Day11 : IDay
    {


        private const string input = "3,8,1005,8,337,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,101,1,10,10,4,10,1008,8,1,10,4,10,101,0,8,29,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,0,10,4,10,102,1,8,51,1,1008,18,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,102,1,8,76,1006,0,55,1,1108,6,10,1,108,15,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,110,2,1101,13,10,1,101,10,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,139,1006,0,74,2,107,14,10,1,3,1,10,2,1104,19,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,177,2,1108,18,10,2,1108,3,10,1,109,7,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,101,0,8,210,1,1101,1,10,1,1007,14,10,2,1104,20,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,244,1,101,3,10,1006,0,31,1006,0,98,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,277,1006,0,96,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,0,10,4,10,1002,8,1,302,1,3,6,10,1006,0,48,2,101,13,10,2,2,9,10,101,1,9,9,1007,9,1073,10,1005,10,15,99,109,659,104,0,104,1,21101,937108976384,0,1,21102,354,1,0,1105,1,458,21102,1,665750077852,1,21101,0,365,0,1105,1,458,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21101,21478178856,0,1,21101,412,0,0,1105,1,458,21102,3425701031,1,1,21102,1,423,0,1106,0,458,3,10,104,0,104,0,3,10,104,0,104,0,21102,984458351460,1,1,21102,1,446,0,1105,1,458,21101,0,988220908388,1,21101,457,0,0,1105,1,458,99,109,2,22101,0,-1,1,21102,1,40,2,21101,489,0,3,21101,479,0,0,1105,1,522,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,484,485,500,4,0,1001,484,1,484,108,4,484,10,1006,10,516,1102,0,1,484,109,-2,2105,1,0,0,109,4,1201,-1,0,521,1207,-3,0,10,1006,10,539,21102,1,0,-3,21201,-3,0,1,21202,-2,1,2,21101,1,0,3,21101,558,0,0,1105,1,563,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,586,2207,-4,-2,10,1006,10,586,22102,1,-4,-4,1106,0,654,21202,-4,1,1,21201,-3,-1,2,21202,-2,2,3,21102,1,605,0,1106,0,563,21201,1,0,-4,21102,1,1,-1,2207,-4,-2,10,1006,10,624,21102,1,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,646,22101,0,-1,1,21102,646,1,0,106,0,521,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2106,0,0";

        public long Part1()
        {
            long[] intcode = input.Split(',').Select(i => long.Parse(i)).ToArray();
            var computer = new IntCodeComputer(intcode);

            var outputs = new BlockingCollection<long>();
            var inputs = new BlockingCollection<long>();
            var brain = Task.Run(() => computer.Execute(inputs, outputs));

            int orientation = 0;
            Point location = new Point(0, 0);

            var surface = new Dictionary<Point, long>();
            surface.Add(location,0);
            bool run = true;

            var robot = Task.Run(() => {
                while (run)
                {

                    if (!surface.ContainsKey(location))
                    {
                        surface.Add(location, 0);
                    }
                    inputs.Add(surface[location]);
                    
                    //Console.WriteLine($"Color at {location.x} {location.y} is {surface[location]}");
                    var color = outputs.Take();
                    surface[location] = color;

                    var direction = outputs.Take();
                    
                    switch (orientation)
                    {
                        case 0: // up
                            orientation = direction == 0 ? 3 : 1;
                            location = new Point(location.x + (direction == 0 ? -1 : 1), location.y);
                            break;
                        case 1: // right
                            orientation = direction == 0 ? 0 : 2;
                            location = new Point(location.x, location.y + (direction == 0 ? -1 : 1));
                            break;
                        case 2: // down
                            orientation = direction == 0 ? 1 : 3;
                            location = new Point(location.x + (direction == 0 ? 1 : -1), location.y);
                            break;
                        case 3: // left
                            orientation = direction == 0 ? 2 : 0;
                            location = new Point(location.x, location.y + (direction == 0 ? 1 : -1));
                            break;
                    }
                    

                    
                }
            });

            brain.Wait();
            run = false;

            return surface.Keys.Count();

        }

        public long Part2()
        {
            return 0;
        }
    }
}
