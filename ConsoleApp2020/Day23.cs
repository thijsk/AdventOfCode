using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day23 : IDay
    {
        public long Part1()
        {
//            return 0;
            var cups = ParseInput();

            var final = Play(cups, 100);

            ConsoleX.WriteLine($"-- final --");
            var finalcupsstr = string.Join(" ", final);
            ConsoleX.WriteLine($"cups: {finalcupsstr}");

            var answer = final.AsCircular().Skip(final.IndexOf(1)).Skip(1).Take(8);
            var answerstr = string.Join("", answer);

            return Convert.ToInt64(answerstr);


            //// -- move 1 --
            ////cups: (3) 8  9  1  2  5  4  6  7
            ////pick up: 8, 9, 1
            ////destination: 2

            //long move = 1;
            //int index = 0;

            //while (move <= 100)
            //{


            //    var newcups = new byte[9];

            //    var current = cups[index];
            //    var index1 = Inc(index, 1);
            //    var index2 = Inc(index, 2);
            //    var index3 = Inc(index, 3);
            //    var take3indexes = new[] { index1, index2, index3 };
            //    var take3values = new[] { cups[index1], cups[index2], cups[index3] };
            //    var destinationvalue = FindDestination(current, take3values);

            //    ConsoleX.WriteLine($"-- move {move} --");
            //    var cupsstr = string.Join(" ", cups);
            //    ConsoleX.WriteLine($"cups: {cupsstr}");
            //    ConsoleX.WriteLine($"current: {current}");
            //    var pickupstr = string.Join(", ", take3values);
            //    ConsoleX.WriteLine($"pick up: {pickupstr}");
            //    ConsoleX.WriteLine($"destination: {destinationvalue}");

            //    byte newindex = 0;

            //    for (byte currentindex = 0; currentindex < 9; currentindex++)
            //    {
            //        if (take3indexes.Contains(currentindex))
            //        {
            //            continue;
            //        }
            //        newcups[newindex] = cups[currentindex];
            //        if (newcups[newindex] == current)
            //            index = newindex;
            //        newindex++;
            //        if (cups[currentindex] == destinationvalue)
            //        {
            //            take3values.CopyTo(newcups, newindex);
            //            newindex += 3;
            //        }
            //    }

            //    cups = newcups;

            //    index = Inc(index, 1);
            //    move++;
            //    ConsoleX.WriteLine();
            //}

            //ConsoleX.WriteLine($"-- final --");
            //var finalcupsstr = string.Join(" ", cups);
            //ConsoleX.WriteLine($"cups: {finalcupsstr}");

            //var cupslist = cups.ToList();

            //var answer = cupslist.AsCircular().Skip(cupslist.IndexOf(1)).Skip(1).Take(8);
            //var answerstr = string.Join("", answer);

            //return Convert.ToInt64(answerstr);
        }

        //private byte FindDestination(in byte current, byte[] take3)
        //{
        //    byte next;
        //    if (current == 1)
        //        next = 9;
        //    else
        //        next = (byte)(current - 1);
        //    if (take3.Contains(next))
        //    {
        //        next = FindDestination(next, take3);
        //    }
        //    return next;
        //}

        //private int FindDestination2(in int current, ref int[] take3)
        //{
        //    int next;
        //    if (current == 1)
        //        next = 1000000-1;
        //    else
        //        next = (int)(current - 1);
        //    if (take3.Contains(next))
        //    {
        //        next = FindDestination2(next, ref take3);
        //    }
        //    return next;
        //}

        //private int Inc(in int index, int amount)
        //{
        //    return (index + amount) % 9;
        //}

        //private int Inc2(in int index, int amount)
        //{
        //    return (index + amount) % 1000000;
        //}

        public long Part2()
        {
            var cups = ParseInput();
            var cupslist = cups.Select(i => (int)i).ToList();
            cupslist.AddRange(Enumerable.Range(10, 1000000-9));
            cups = cupslist.ToArray();

            var final = Play(cups, 10000000);
            var answer = final.AsCircular().Skip(final.IndexOf(1)).Skip(1).Take(2).ToArray();
            Console.WriteLine($"{answer[0]} {answer[1]}");
            return (long)answer[0] * answer[1];


            //var input = ParseInput();
            //var cupslist = input.Select(i => (int) i).ToList();
            //cupslist.AddRange(Enumerable.Range(10, 1000000-9));
            //var cups = cupslist.ToArray();

            //long move = 1;
            //int index = 0;

            //var sw = new Stopwatch();
            //sw.Start();

            //var newcups = new int[1000000];
            //while (move <= 10000000)
            //{
            //    var current = cups[index];
            //    var index1 = Inc2(index, 1);
            //    var index2 = Inc2(index, 2);
            //    var index3 = Inc2(index, 3);
            //    //var take3indexes = new[] { index1, index2, index3 };
            //    var take3values = new[] { cups[index1], cups[index2], cups[index3] };
            //    var destinationvalue = FindDestination2(current, ref take3values);

            ////    ConsoleX.WriteLine($"-- move {move} --");
            ////    var cupsstr = string.Join(" ", cups);
            ////    ConsoleX.WriteLine($"cups: {cupsstr}");
            ////    ConsoleX.WriteLine($"current: {current}");
            ////    var pickupstr = string.Join(", ", take3values);
            ////    ConsoleX.WriteLine($"pick up: {pickupstr}");
            ////    ConsoleX.WriteLine($"destination: {destinationvalue}");

            //    int newindex = 0;

            //    for (int currentindex = 0; currentindex < 1000000; currentindex++)
            //    {
            //        if (currentindex == index1 || currentindex == index2 || currentindex == index3)
            //        {
            //            continue;
            //        }

            //        var currentvalue = cups[currentindex];
            //        newcups[newindex] = currentvalue;
            //        if (currentvalue == current)
            //            index = newindex;
            //        newindex++;
            //        if (currentvalue == destinationvalue)
            //        {
            //            newcups[newindex] = take3values[0];
            //            newindex++;
            //            newcups[newindex] = take3values[1];
            //            newindex++;
            //            newcups[newindex] = take3values[2];
            //            newindex ++;
            //        }
            //    }

            //    cups = newcups;

            //    index = Inc2(index, 1);
            //    move++;

            //    if (move % 10000 == 0)
            //    {
            //        var eta = new TimeSpan(((10000000 / move) * sw.ElapsedTicks) - sw.ElapsedTicks);
            //        Console.WriteLine($"move {move} eta {eta}");
            //    }

            ////    ConsoleX.WriteLine();
            //}

            ////ConsoleX.WriteLine($"-- final --");
            ////var finalcupsstr = string.Join(" ", cups);
            ////ConsoleX.WriteLine($"cups: {finalcupsstr}");

            //cupslist = cups.ToList();

            //var answer = cupslist.AsCircular().Skip(cupslist.IndexOf(1)).Skip(1).Take(2).ToArray();
            //return answer[0] * answer[1];
            return 0;
        }


        public List<int> Play(int[] cups, int moves)
        {
            // cup, nextcup
            var nocups = cups.Length;
            var maxindex = nocups;
            var pointers = new Dictionary<int,int>();
           
            var index = 0;
            foreach (var cup in cups)
            {
                pointers.Add(cup, cups[(++index % maxindex)]);
            }

            var currentcup = cups[0];

            var sw = new Stopwatch();
            sw.Start();

            var move = 1;
            while (move <= moves)
            {

                //ConsoleX.WriteLine($"-- move {move} --");
                //var cupsstr = string.Join(" ", DictionaryToList(pointers).Select(c => c == currentcup ? $"({c})": $"{c}"));
                //ConsoleX.WriteLine($"cups: {cupsstr}");
                //ConsoleX.WriteLine($"current: {currentcup}");

                var pickup1 = pointers[currentcup];
                var pickup2 = pointers[pickup1];
                var pickup3 = pointers[pickup2];

               // ConsoleX.WriteLine($"pick up: {pickup1}, {pickup2}, {pickup3}");


                var glue = pointers[pickup3];
                pointers[currentcup] = glue;

                var destination = FindDestination3(currentcup, pickup1, pickup2, pickup3, maxindex);
                // ConsoleX.WriteLine($"destination: {destination}");
                glue = pointers[destination];
                pointers[destination] = pickup1;
                pointers[pickup3] = glue;
                currentcup = pointers[currentcup];
                move++;

                // ConsoleX.WriteLine();
            }

            var result = DictionaryToList(pointers);

            return result;

        }

        private static List<int> DictionaryToList(Dictionary<int, int> pointers)
        {
            var pointer = pointers.Keys.First();
            var start = pointer;
            var result = new List<int>();
            while (result.Count < pointers.Keys.Count)
            {
                result.Add(pointer);
                pointer = pointers[pointer];
            }

            return result;
        }

        private int FindDestination3(in int current, in int pickup1, in int pickup2, in int pickup3, in int max)
        {
            int next;
            if (current == 1)
                next = max;
            else
                next = (int)(current - 1);
            if (next == pickup1 || next == pickup2 || next == pickup3 )
            {
                next = FindDestination3(next, pickup1, pickup2, pickup3, max);
            }
            return next;
        }


        public int[] ParseInput()
        {
            return File.ReadAllLines($"Day23.txt").First().Select(c => Convert.ToInt32($"{c}")).ToArray();

        }
    }
}