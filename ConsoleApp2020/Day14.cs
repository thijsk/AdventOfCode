using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day14 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            string mask = "";
            var mem = new Dictionary<long, long>();

            foreach (var line in input)
            {
                if (line[0] == "mask")
                {
                    mask = line[1];
                } else if (line[0].StartsWith("mem"))
                {
                    var address = Int32.Parse(line[0].Replace("mem[","").Replace("]", ""));
                    var value= long.Parse(line[1]);
                    mem[address] = ApplyValueMask(mask, value);
                }
            }

            return mem.Values.Sum();
        }

        private long ApplyValueMask(string mask, long value)
        {
            var bitValue = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();
            for (int i = 0; i < mask.Length; i++)
            {
                var bit = mask[i];
                if (bit != 'X')
                {
                    bitValue[i] = bit;
                }
            }

            return Convert.ToInt64(new string(bitValue), 2);
        }


        private long[] ApplyAddressMask(string mask, long value)
        {
            var bitValue = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();
            for (int i = 0; i < mask.Length; i++)
            {
                var bit = mask[i];
                if (bit != '0')
                {
                    bitValue[i] = bit;
                }
            }

            var work = new Stack<char[]>();
            var result = new List<char[]>();
            work.Push(bitValue);
            while (work.Count > 0)
            {
                var workValue = work.Pop();
                var index = new string(workValue).IndexOf('X');
                if (index > -1)
                {
                    workValue[index] = '1';
                    work.Push((char[])workValue.Clone());
                    workValue[index] = '0';
                    work.Push((char[])workValue.Clone());
                }
                else
                {
                    result.Add(workValue);
                }
            }

            return result.Select(r => Convert.ToInt64(new string(r), 2)).ToArray();
        }

        public long Part2()
        {
            var input = ParseInput();

            string mask = "";
            var mem = new Dictionary<long, long>();

            foreach (var line in input)
            {
                if (line[0] == "mask")
                {
                    mask = line[1];
                }
                else if (line[0].StartsWith("mem"))
                {
                    var address = Int32.Parse(line[0].Replace("mem[", "").Replace("]", ""));
                    var value = long.Parse(line[1]);
                    var addresses = ApplyAddressMask(mask, address);
                    foreach (var newAddress in addresses)
                    {
                        mem[newAddress] = value;
                    }
                }
            }

            return mem.Values.Sum();
        }

        public IEnumerable<string[]> ParseInput()
        {
            return File.ReadAllLines($"Day14.txt").Select(l => l.Split(" = "));

        }
    }
}