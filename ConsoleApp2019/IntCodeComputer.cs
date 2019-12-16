using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2019
{
    public class IntCodeComputer
    {
        private long[] memory;
        long relativeBase = 0;
        long position = 0;
        long[] modes;

        public IntCodeComputer(long[] intCode)
        {
            memory = (long[])intCode.Clone();
        }

        public long[] Execute(BlockingCollection<long> inputs, BlockingCollection<long> outputs)
        {

            bool exit = false;

            while (!exit)
            {
                var instruction = memory[position];
                var opcode = (instruction) % 100;
                var mode1 = (instruction / 100) % 10;
                var mode2 = (instruction / 1000) % 10;
                var mode3 = (instruction / 10000) % 10;

                modes = new[] { 0, mode1, mode2, mode3 };

                // values for each mode:
                // 0 postion mode
                // 1 immediate mode
                // 2 relative mode

                // opcodes
                // 1 add
                // 2 multiply
                // 3 read input
                // 4 post output
                // 5 jump-if-true
                // 6 jump-if-false
                // 7 less-than
                // 8 equals
                // 9 set relative base
                // 99 terminate

                //Console.WriteLine($"{opcode} with mode {mode1}{mode2}{mode3}");

                switch (opcode)
                {
                    case 1: // add
                        {
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);

                            var result = value1 + value2;

                            SetValue(3, result);

                            position += 4;
                            break;
                        }
                    case 2: // multiply
                        {
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);

                            var result = value1 * value2;

                            SetValue(3, result);

                            position += 4;
                            break;
                        }
                    case 3: // read input
                        {
                            SetValue(1, inputs.Take());

                            position += 2;
                            break;
                        }
                    case 4: // write output
                        {
                            var result = GetValue(1);

                            outputs.Add(result);

                            position += 2;
                            break;
                        }
                    case 5: // jump-if-true
                        {
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);
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
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);
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
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);
                            var result = value1 < value2 ? 1 : 0;

                            SetValue(3, result);

                            position += 4;
                            break;
                        }
                    case 8: // equals
                        {
                            var value1 = GetValue(1);
                            var value2 = GetValue(2);
                            var result = value1 == value2 ? 1 : 0;

                            SetValue(3, result);

                            position += 4;
                            break;
                        }
                    case 9:
                        {
                            var value = GetValue(1);
                            relativeBase += value;
                            position += 2;
                            break;
                        }
                    case 99:
                        exit = true;
                        outputs.CompleteAdding();
                        break;
                    default:
                        throw new Exception($"Invalid opcode {opcode} at position {position}");
                }
            }

            return memory;
        }

        private void SetMemory(long index, long value)
        {
            if (index >= memory.Length)
            {
                ResizeMemory(index + 1);
            }
            memory[index] = value;
        }

        private void ResizeMemory(long size)
        {
            var tmp = new long[size + 1];
            memory.CopyTo(tmp, 0);
            memory = tmp;
        }

        private void SetValue(long arg, long result)
        {
            long mode = modes[arg];
            long argValue = GetMemory(position + arg);

            if (mode == 0)
            {
                SetMemory(argValue, result);
            }
            else if (mode == 1)
            {
                SetMemory((position + arg), result);
            }
            else if (mode == 2)
            {
                SetMemory((relativeBase + argValue), result);
            }
        }

        private long GetMemory(long index)
        {
            if (index >= memory.Length)
            {
                ResizeMemory(index + 1);
            }
            return memory[index];
        }


        private long GetValue(long arg)
        {
            long mode = modes[arg];
            long argValue = GetMemory(position + arg);

            if (mode == 0)
            {
                return GetMemory(argValue);
            }
            else if (mode == 1)
            {
                return argValue;
            }
            else if (mode == 2)
            {
                return GetMemory(relativeBase + argValue);
            }
            throw new Exception("invalid mode");
        }

        public static long[] ParseIntcodeString(string input)
        {
            return input.Split(',').Select(i => long.Parse(i)).ToArray();
        }

    }
}
