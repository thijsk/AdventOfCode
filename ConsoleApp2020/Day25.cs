using System;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day25 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var cardSubjectNo = input[0];
            var doorSubjectNo = input[1];

            var cardLoops = SearchLoop(cardSubjectNo);
            var doorLoops = SearchLoop(doorSubjectNo);


            ConsoleX.WriteLine($"{cardLoops}, {doorLoops}");

            var cardKey = Loop(doorSubjectNo, cardLoops);
            var doorKey = Loop(cardSubjectNo, doorLoops);

            ConsoleX.WriteLine($"{cardKey}, {doorKey}");

            return cardKey;
        }

        //Set the value to itself multiplied by the subject number.
        //Set the value to the remainder after dividing the value by 20201227.
        private long Loop(long subjectNr, long loops)
        {
            long value = 1;
            for (long loop = 1; loop <= loops; loop++)
            {
                value = value * subjectNr;
                value = value % 20201227;
            }

            return value;
        }

        private long SearchLoop(long resultSubjectNr)
        {
            long subjectNr = 7;
            long value = 1;
            for (long loop = 1; loop <= long.MaxValue; loop++)
            {
                value = value * subjectNr;
                value = value % 20201227;
                if (value == resultSubjectNr)
                    return loop;
            }

            return value;
        }

        public long Part2()
        {
            var input = ParseInput();

            return 0;
        }

        public long[] ParseInput()
        {
            return File.ReadAllLines($"Day25.txt").Select(line => Convert.ToInt64(line)).ToArray();

        }
    }
}