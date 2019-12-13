using Common;
using System;
using System.Linq;

namespace ConsoleApp2019
{
    class Day2 : IDay
    {

        const string input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,2,6,19,23,1,23,5,27,1,27,13,31,2,6,31,35,1,5,35,39,1,39,10,43,2,6,43,47,1,47,5,51,1,51,9,55,2,55,6,59,1,59,10,63,2,63,9,67,1,67,5,71,1,71,5,75,2,75,6,79,1,5,79,83,1,10,83,87,2,13,87,91,1,10,91,95,2,13,95,99,1,99,9,103,1,5,103,107,1,107,10,111,1,111,5,115,1,115,6,119,1,119,10,123,1,123,10,127,2,127,13,131,1,13,131,135,1,135,10,139,2,139,6,143,1,143,9,147,2,147,6,151,1,5,151,155,1,9,155,159,2,159,6,163,1,163,2,167,1,10,167,0,99,2,14,0,0";
        //const string input = "1,1,1,4,99,5,6,0,99";
        public long Part1()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();
            var result = Execute(intcode, 12, 2);

            //Console.WriteLine(input);
            //Console.WriteLine(string.Join(',', intcode));
            return intcode[0];
        }

        private static int Execute(int[] intcode, int noun, int verb)
        {

            intcode[1] = noun;
            intcode[2] = verb;

            int position = 0;

            bool exit = false;

            while (!exit)
            {
                var opcode = intcode[position];



                switch (opcode)
                {
                    case 1:
                        {
                            var pos1 = intcode[position + 1];
                            var pos2 = intcode[position + 2];
                            var pos3 = intcode[position + 3];

                            intcode[pos3] = intcode[pos1] + intcode[pos2];
                            break;
                        }
                    case 2:
                        {
                            var pos1 = intcode[position + 1];
                            var pos2 = intcode[position + 2];
                            var pos3 = intcode[position + 3];

                            intcode[pos3] = intcode[pos1] * intcode[pos2];
                            break;
                        }

                    case 99:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid opcode " + opcode);
                        break;
                }

                position += 4;
            }

            return intcode[0];
        }

        public long Part2()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();

            for (int verb = 0; verb < 100; verb++)
                for (int noun = 0; noun < 100; noun++)
                {
                    var result = Execute((int[])intcode.Clone(), noun, verb);
                    if (result == 19690720)
                    {
                        Console.WriteLine($"Noun {noun} Verb {verb}");
                        return (100 * noun) + verb;
                    }
                }

            return -1;
        }
    }
}
