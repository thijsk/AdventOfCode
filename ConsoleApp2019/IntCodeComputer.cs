using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2019
{
    public class IntCodeComputer
    {
        private int[] memory;


        public IntCodeComputer(int[] intCode)
        {
            this.memory = (int[])intCode.Clone();
        }

        public int[] Execute(Func<int, int> onInput, Action<int, int> onOutput)
        {
            int position = 0;
            var outputs = new List<int>();
            bool exit = false;

            int inputNumber = 0;
            int outputNumber = 0;

            while (!exit)
            {
                var instruction = memory[position];
                var opcode = (instruction) % 100;
                var mode1 = (instruction / 100) % 10;
                var mode2 = (instruction / 1000) % 10;
                var mode3 = (instruction / 10000) % 10;

                switch (opcode)
                {
                    case 1: // addd
                        {
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var arg3 = memory[position + 3];

                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
                            if (mode3 == 0)
                            {
                                memory[arg3] = value1 + value2;
                            }
                            else
                            {
                                memory[position + 3] = value1 + value2;
                            }
                            position += 4;
                            break;
                        }
                    case 2: // multiply
                        {
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var arg3 = memory[position + 3];

                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
                            if (mode3 == 0)
                            {
                                memory[arg3] = value1 * value2;
                            }
                            else
                            {
                                memory[position + 3] = value1 * value2;
                            }
                            position += 4;
                            break;
                        }
                    case 3: // read input
                        {
                            var arg1 = memory[position + 1];
                            if (mode1 == 0)
                            {
                                memory[arg1] = onInput(inputNumber);
                            }
                            else
                            {
                                memory[position + 1] = onInput(inputNumber);
                            }
                            inputNumber++;
                            position += 2;
                            break;
                        }
                    case 4: // write output
                        {
                            var arg1 = memory[position + 1];
                            if (mode1 == 0)
                            {
                                outputs.Add(memory[arg1]);
                                onOutput?.Invoke(outputNumber, memory[arg1]);
                            }
                            else
                            {
                                outputs.Add(arg1);
                                onOutput?.Invoke(outputNumber, arg1);
                            }
                            outputNumber++;
                            position += 2;
                            break;
                        }
                    case 5: // jump-if-true
                        {
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
                            if (value1 != 0)
                            {
                                position = value2;
                            }
                            else
                            {
                                position += 3;
                            }
                            break;
                        }
                    case 6: // jump-if-false
                        {
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
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
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var arg3 = memory[position + 3];
                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
                            var result = value1 < value2 ? 1 : 0;
                            if (mode3 == 0)
                            {
                                memory[arg3] = result;
                            }
                            else
                            {
                                memory[position + 3] = result;
                            }
                            position += 4;
                            break;
                        }
                    case 8: // equals
                        {
                            var arg1 = memory[position + 1];
                            var arg2 = memory[position + 2];
                            var arg3 = memory[position + 3];
                            var value1 = mode1 == 0 ? memory[arg1] : arg1;
                            var value2 = mode2 == 0 ? memory[arg2] : arg2;
                            var result = value1 == value2 ? 1 : 0;
                            if (mode3 == 0)
                            {
                                memory[arg3] = result;
                            }
                            else
                            {
                                memory[position + 3] = result;
                            }
                            position += 4;
                            break;
                        }
                    case 99:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid opcode " + opcode);
                        return new[] { position };
                }


            }

            return memory;
        }

    }
}
