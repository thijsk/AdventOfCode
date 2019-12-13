using Common;
using System;
using System.Linq;

namespace ConsoleApp2019
{
    class Day5 : IDay
    {

        const string input = "3,225,1,225,6,6,1100,1,238,225,104,0,2,171,209,224,1001,224,-1040,224,4,224,102,8,223,223,1001,224,4,224,1,223,224,223,102,65,102,224,101,-3575,224,224,4,224,102,8,223,223,101,2,224,224,1,223,224,223,1102,9,82,224,1001,224,-738,224,4,224,102,8,223,223,1001,224,2,224,1,223,224,223,1101,52,13,224,1001,224,-65,224,4,224,1002,223,8,223,1001,224,6,224,1,223,224,223,1102,82,55,225,1001,213,67,224,1001,224,-126,224,4,224,102,8,223,223,1001,224,7,224,1,223,224,223,1,217,202,224,1001,224,-68,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,1002,176,17,224,101,-595,224,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,1102,20,92,225,1102,80,35,225,101,21,205,224,1001,224,-84,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,1101,91,45,225,1102,63,5,225,1101,52,58,225,1102,59,63,225,1101,23,14,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,677,677,224,1002,223,2,223,1006,224,329,101,1,223,223,1108,226,677,224,1002,223,2,223,1006,224,344,101,1,223,223,7,677,226,224,102,2,223,223,1006,224,359,1001,223,1,223,8,677,226,224,102,2,223,223,1005,224,374,1001,223,1,223,1107,677,226,224,102,2,223,223,1006,224,389,1001,223,1,223,1008,226,226,224,1002,223,2,223,1005,224,404,1001,223,1,223,7,226,677,224,102,2,223,223,1005,224,419,1001,223,1,223,1007,677,677,224,102,2,223,223,1006,224,434,1001,223,1,223,107,226,226,224,1002,223,2,223,1005,224,449,1001,223,1,223,1008,677,226,224,102,2,223,223,1006,224,464,1001,223,1,223,1007,677,226,224,1002,223,2,223,1005,224,479,1001,223,1,223,108,677,677,224,1002,223,2,223,1006,224,494,1001,223,1,223,108,226,226,224,1002,223,2,223,1006,224,509,101,1,223,223,8,226,677,224,102,2,223,223,1006,224,524,101,1,223,223,107,677,226,224,1002,223,2,223,1005,224,539,1001,223,1,223,8,226,226,224,102,2,223,223,1005,224,554,101,1,223,223,1108,677,226,224,102,2,223,223,1006,224,569,101,1,223,223,108,677,226,224,102,2,223,223,1006,224,584,1001,223,1,223,7,677,677,224,1002,223,2,223,1005,224,599,101,1,223,223,1007,226,226,224,102,2,223,223,1005,224,614,1001,223,1,223,1107,226,677,224,102,2,223,223,1006,224,629,101,1,223,223,1107,226,226,224,102,2,223,223,1005,224,644,1001,223,1,223,1108,677,677,224,1002,223,2,223,1005,224,659,101,1,223,223,107,677,677,224,1002,223,2,223,1006,224,674,1001,223,1,223,4,223,99,226";
        //const string input = "1002,4,3,4,33";
        //const string input = "1101,100,-1,4,0";
        public long Part1()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();
            var result = Execute(intcode, 1);
            return result;
        }

        private static int Execute(int[] intcode, int input)
        {
            int position = 0;
            var output = 0;

            bool exit = false;

            while (!exit)
            {
                var instruction = intcode[position];
                var opcode = (instruction) % 100;
                var mode1 = (instruction / 100) % 10;
                var mode2 = (instruction / 1000) % 10;
                var mode3 = (instruction / 10000) % 10;

                switch (opcode)
                {
                    case 1: // addd
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var arg3 = intcode[position + 3];

                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            if (mode3 == 0)
                            {
                                intcode[arg3] = value1 + value2;
                            } else
                            {
                                intcode[position + 3] = value1 + value2;
                            }
                            position += 4;
                            break;
                        }
                    case 2: // multiply
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var arg3 = intcode[position + 3];

                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            if (mode3 == 0)
                            {
                                intcode[arg3] = value1 * value2;
                            }
                            else
                            {
                                intcode[position + 3] = value1 * value2;
                            }
                            position += 4;
                            break;
                        }
                    case 3: // read input
                        {
                            var arg1 = intcode[position + 1];
                            if (mode1 == 0)
                            {
                                intcode[arg1] = input;
                            }
                            else
                            {
                                intcode[position + 1] = input;
                            }
                            position += 2;
                            break;
                        }
                    case 4: // write output
                        {
                            var arg1 = intcode[position + 1];
                            if (mode1 == 0)
                            {
                                output = intcode[arg1];
                            }
                            else
                            {
                                output = arg1;
                            }
                            position += 2;
                            break;
                        }
                    case 5: // jump-if-true
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            if (value1 != 0)
                            {
                                position = value2;
                            } else
                            {
                                position += 3;
                            }
                            break;
                        }
                    case 6: // jump-if-false
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            if (value1 == 0)
                            {
                                position = value2;
                            }
                            else
                            {
                                position += 3;
                            }
                            break;
                        }
                    case 7: // less than
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var arg3 = intcode[position + 3];
                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            var result = value1 < value2 ? 1 : 0;
                            if (mode3 == 0)
                            {
                                intcode[arg3] = result;
                            }
                            else
                            {
                                intcode[position + 3] = result;
                            }
                            position += 4;
                            break;
                        }
                    case 8: // equals
                        {
                            var arg1 = intcode[position + 1];
                            var arg2 = intcode[position + 2];
                            var arg3 = intcode[position + 3];
                            var value1 = mode1 == 0 ? intcode[arg1] : arg1;
                            var value2 = mode2 == 0 ? intcode[arg2] : arg2;
                            var result = value1 == value2 ? 1 : 0;
                            if (mode3 == 0)
                            {
                                intcode[arg3] = result;
                            }
                            else
                            {
                                intcode[position + 3] = result;
                            }
                            position += 4;
                            break;
                        }
                    case 99:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid opcode " + opcode);
                        return position;
                }

                
            }

            return output;
        }

        public long Part2()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();
            var result = Execute(intcode, 5);

            return result;
        }
    }
}
