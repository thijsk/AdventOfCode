﻿using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2019
{
    class Day15 : IDay
    {
       
                

        private const string input = @"3,1033,1008,1033,1,1032,1005,1032,31,1008,1033,2,1032,1005,1032,58,1008,1033,3,1032,1005,1032,81,1008,1033,4,1032,1005,1032,104,99,1002,1034,1,1039,1001,1036,0,1041,1001,1035,-1,1040,1008,1038,0,1043,102,-1,1043,1032,1,1037,1032,1042,1105,1,124,1001,1034,0,1039,102,1,1036,1041,1001,1035,1,1040,1008,1038,0,1043,1,1037,1038,1042,1105,1,124,1001,1034,-1,1039,1008,1036,0,1041,101,0,1035,1040,102,1,1038,1043,1001,1037,0,1042,1106,0,124,1001,1034,1,1039,1008,1036,0,1041,1001,1035,0,1040,102,1,1038,1043,1001,1037,0,1042,1006,1039,217,1006,1040,217,1008,1039,40,1032,1005,1032,217,1008,1040,40,1032,1005,1032,217,1008,1039,9,1032,1006,1032,165,1008,1040,5,1032,1006,1032,165,1101,0,2,1044,1105,1,224,2,1041,1043,1032,1006,1032,179,1102,1,1,1044,1106,0,224,1,1041,1043,1032,1006,1032,217,1,1042,1043,1032,1001,1032,-1,1032,1002,1032,39,1032,1,1032,1039,1032,101,-1,1032,1032,101,252,1032,211,1007,0,40,1044,1106,0,224,1101,0,0,1044,1106,0,224,1006,1044,247,102,1,1039,1034,101,0,1040,1035,101,0,1041,1036,1001,1043,0,1038,1001,1042,0,1037,4,1044,1106,0,0,26,29,83,66,1,36,14,44,33,12,3,15,20,56,9,35,51,55,6,20,13,71,15,23,94,38,45,15,47,30,89,39,11,55,5,9,47,29,41,36,78,12,4,65,48,66,36,94,76,30,63,41,32,1,73,1,35,65,87,46,18,90,11,44,30,73,87,8,38,46,17,78,51,34,19,53,37,26,20,24,46,64,17,6,26,41,10,62,14,88,23,94,13,55,5,45,10,39,83,99,32,34,72,30,58,33,71,47,21,38,97,38,46,41,18,39,37,8,86,55,35,4,92,19,21,53,61,6,55,69,16,85,62,26,63,17,80,33,10,53,91,2,37,94,37,93,7,97,18,55,54,36,17,62,89,12,92,32,69,4,46,47,19,89,25,12,51,91,9,1,71,35,56,39,98,48,7,49,24,95,15,45,2,1,93,82,19,7,11,70,30,64,28,27,58,4,39,30,94,72,33,43,90,98,26,32,70,1,81,25,35,47,17,31,92,15,73,13,27,72,65,30,67,2,22,89,77,30,47,12,58,26,79,22,37,74,41,3,42,30,39,67,24,18,62,98,19,59,95,25,6,67,42,35,85,51,48,7,63,17,67,53,45,13,25,43,1,54,4,65,55,20,73,32,70,1,33,39,93,88,19,35,56,21,13,53,73,31,21,44,73,31,13,69,30,42,26,51,25,90,16,49,9,93,50,28,60,24,18,61,23,11,98,19,45,77,12,61,31,3,66,56,4,77,24,59,87,31,38,65,67,7,9,23,71,9,59,35,55,83,22,12,94,17,67,87,96,63,8,29,32,34,15,55,39,60,41,74,39,81,47,51,25,26,57,28,18,60,84,20,16,66,42,14,25,16,94,2,22,74,85,19,63,32,9,19,11,91,44,34,21,1,56,12,87,8,52,18,56,7,90,5,86,81,24,98,21,9,80,59,68,10,80,53,18,75,50,9,14,43,26,29,57,86,39,41,93,3,69,55,16,84,15,22,84,30,72,19,13,15,19,80,97,79,32,68,77,82,30,19,4,71,45,67,14,95,17,54,80,88,25,13,80,41,37,96,15,28,26,33,73,32,45,79,21,52,23,98,82,21,16,13,64,32,39,93,17,33,95,61,36,12,21,3,84,4,88,22,26,59,80,27,82,2,85,79,29,33,52,17,23,95,8,64,16,56,23,42,43,18,41,11,9,84,42,62,4,67,17,98,76,99,1,16,72,72,10,79,19,76,4,54,9,99,34,33,7,97,85,19,76,93,38,6,90,37,90,2,83,61,19,43,39,2,91,17,60,21,79,2,32,94,38,32,7,64,8,14,7,68,23,28,75,24,73,50,29,63,22,89,4,51,66,2,7,33,82,13,23,84,81,23,55,68,15,27,9,97,27,79,42,86,75,56,13,95,74,5,88,25,44,99,33,14,24,29,21,78,4,15,75,32,92,74,11,56,24,57,10,28,73,8,10,90,77,30,96,8,60,3,71,20,41,9,33,89,38,74,95,4,95,35,13,18,55,10,81,9,60,17,67,7,34,48,48,15,54,79,37,66,43,22,64,28,28,4,91,5,9,92,30,64,37,98,66,15,92,2,3,25,70,25,33,61,56,25,70,58,30,41,97,18,54,10,49,45,3,1,30,57,30,46,8,55,79,39,58,46,35,19,38,80,86,4,36,75,29,62,39,71,2,41,6,66,36,99,21,61,39,72,3,48,29,43,31,59,84,71,12,52,61,82,11,56,23,51,30,60,88,65,35,48,24,58,76,49,93,51,33,72,0,0,21,21,1,10,1,0,0,0,0,0,0";
        public long Part1()
        {
            ConcurrentDictionary<Point<int>, (byte, int)> screen;
            Point<int> start;
            RunSimulation(out screen, out start);
            var distance = 0;
            var neighbors = AddDistance(screen, start, distance);
            while (neighbors.Any())
            {
                distance++;
                neighbors = neighbors.SelectMany(n => AddDistance(screen, n, distance)).ToList();
            }

            var destinationtile = screen.First(s => s.Value.Item1 == 2);

            return destinationtile.Value.Item2;
        }

        private void RunSimulation(out ConcurrentDictionary<Point<int>, (byte, int)> screen, out Point<int> start)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var code = IntCodeComputer.ParseIntcodeString(input);
            var computer = new IntCodeComputer(code);

            var outputs = new BlockingCollection<long>();
            var inputs = new BlockingCollection<long>();
            var computerTask = Task.Run(() => computer.Execute(inputs, outputs));

            screen = new ConcurrentDictionary<Point<int>, (byte, int)>();
            var location = new Point<int>(0, 0);
            start = location;
            screen.TryAdd(location, (0, 0));

            int direction = 1;

            var iter = 1;

            while (!outputs.IsCompleted)
            {
                iter++;
                inputs.Add(direction);
                var result = outputs.Take();
                if (result == 0) // hits wall
                {
                    // register wall location
                    var walllocation = AddDirection(location, direction);
                    UpdateScreen(screen, walllocation, (0, 0));
                    // try to rotate right
                    direction = TurnRight(direction);
                }
                else if (result == 1 || result == 2)
                {
                    location = AddDirection(location, direction);
                    UpdateScreen(screen, location, ((byte)result, 0));
                    // continue in same direction
                    direction = TurnLeft(direction);
                }

                if (location.Equals(start) && iter > 100)
                    break;
            }
            //Console.SetCursorPosition(0, 0);
            DrawScreen(screen, location, direction);
            inputs.Add(5);
        }

        private IEnumerable<Point<int>> AddDistance(ConcurrentDictionary<Point<int>, (byte, int)> screen, Point<int> point, int distance)
        {
            var value = screen[point];
            value.Item2 = distance;
            screen[point] = value;

            var north = AddDirection(point, 1);
            var south = AddDirection(point, 2);
            var west = AddDirection(point, 3);
            var east = AddDirection(point, 4);
            var neighbors = new [] { north, south, west, east};
            
            return neighbors.Where(p => {
                var n = screen[p];
                return (n.Item1 == 1 || n.Item1 == 2) && n.Item2 == 0;
            }
            );
        }

        private int TurnLeft(int direction)
        {
            return TurnRight(TurnRight(TurnRight(direction)));
        }

        private int TurnRight(int direction)
        {
            switch (direction)
            {
                case 1: // north
                    return 4; // east
                case 2: // south
                    return 3; // west
                case 3: //west
                    return 1;
                case 4: //east
                    return 2; // south
            }
            return 1;
        }

        private Point<int> AddDirection(Point<int> location, int direction)
        {
            var x = location.x;
            var y = location.y;
            if (direction == 3)
            {
                x -= 1;
            } else if (direction == 4)
            {
                x += 1;
            }

            if (direction == 1)
            {
                y -= 1;
            } else if (direction == 2)
            {
                y += 1;
            }

            return new Point<int>(x, y);
        }

        public void UpdateScreen(ConcurrentDictionary<Point<int>, (byte, int)> screen, Point<int> point, (byte, int) value)
        {
            if (!screen.TryAdd(point, value))
            {
                screen[point] = value;
            }
        }


        private void DrawScreen(ConcurrentDictionary<Point<int>, (byte,int)> screen, Point<int> location, int direction)
        {
            var minx = screen.Keys.Min(p => p.x);
            var miny = screen.Keys.Min(p => p.y);
            var maxx = screen.Keys.Max(p => p.x);
            var maxy = screen.Keys.Max(p => p.y);

            foreach (var y in Enumerable.Range(miny, maxy - miny + 1))
            {
                var line = new StringBuilder();
                foreach (var x in Enumerable.Range(minx, maxx - minx + 1))
                {
                    var point = new Point<int>(x, y);
                    char chr = ' ';

                    if (!screen.TryGetValue(point, out var value))
                    {
                        value = (7, 0);
                    }
                    var tile = value.Item1;
                    
                    switch (tile)
                    {
                        case 0:
                            chr = '█';
                            break;
                        case 1:
                            chr = ' ';
                            break;
                        case 2:
                            chr = 'O';
                            break;
                        case 3:
                            chr = 'R';
                            break;
                        default:
                            chr = '?';
                            break;
                    }

                    if (location.Equals(point))
                    {
                        
                        switch (direction)
                        {
                            case 1:
                                chr = '↑';
                                break;
                            case 2:
                                chr = '↓';
                                break;
                            case 3:
                                chr = '←';
                                break;
                            case 4:
                                chr = '→';
                                break;
                        }
                        
                    }
                    line.Append(chr);
                }
                Console.WriteLine(line);
            }

        }

        public long Part2()
        {
            ConcurrentDictionary<Point<int>, (byte, int)> screen;
            Point<int> start;
            RunSimulation(out screen, out start);
            var destinationtile = screen.First(s => s.Value.Item1 == 2);
            start = destinationtile.Key;
            var minutes = 0;
            var neighbors = AddOxygen(screen, start, minutes);
            while (neighbors.Any())
            {
                minutes++;
                neighbors = neighbors.SelectMany(n => AddOxygen(screen, n, minutes)).ToList();
            }
            DrawScreen(screen, start, 1);
            return minutes;
        }

        private IEnumerable<Point<int>> AddOxygen(ConcurrentDictionary<Point<int>, (byte, int)> screen, Point<int> point, int minutes)
        {
            var value = screen[point];
            value.Item1 = 2;
            value.Item2 = minutes;
            screen[point] = value;

            var north = AddDirection(point, 1);
            var south = AddDirection(point, 2);
            var west = AddDirection(point, 3);
            var east = AddDirection(point, 4);
            var neighbors = new[] { north, south, west, east };

            return neighbors.Where(p =>
            {
                var n = screen[p];
                return (n.Item1 == 1);
            }
            );
        }
    }
}