using System;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    public class Day05 : IDay
    {
        public long Part1()
        {
            var inputs = File.ReadAllLines("Day05.txt");

            return inputs.Select(GetSeatId).Max();
        }

        private int GetSeatId(string input)
        {
            var rows = input.Substring(0, 7);
            var cols = input.Substring(7, 3);
            var row = BinSearch(rows);
            var col = BinSearch(cols);
            var result = (row * 8) + col;
            return result;
        }

        private int BinSearch(string rows)
        {
            var bin = rows.Replace("F", "0").Replace("B", "1")
                .Replace("L", "0").Replace("R", "1");
            return Convert.ToInt32(bin, 2);
        }

        public long Part2()
        {
            var inputs = File.ReadAllLines("Day05.txt");

            var seats=  inputs.Select(GetSeatId);

            var frontId = GetSeatId("FFFFFFBLLL");
            var backId = GetSeatId("BBBBBBFRRR");

            var empty = Enumerable.Range(frontId, backId).Where(id =>
                !seats.Contains(id) && seats.Contains(id - 1) && seats.Contains(id + 1));

            return empty.First();
        }
    }
}
