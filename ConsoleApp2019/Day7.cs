using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2019
{
    class Day7 : IDay
    {
        private const string input = "3,8,1001,8,10,8,105,1,0,0,21,38,59,84,93,110,191,272,353,434,99999,3,9,101,5,9,9,1002,9,5,9,101,5,9,9,4,9,99,3,9,1001,9,3,9,1002,9,2,9,101,4,9,9,1002,9,4,9,4,9,99,3,9,102,5,9,9,1001,9,4,9,1002,9,2,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1002,9,2,9,4,9,99,3,9,1002,9,5,9,101,4,9,9,102,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,99";
        //private const string input = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
        //private const string input = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
        //private const string input = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

        public long Part1()
        {
            long[] intcode = input.Split(',').Select(i => long.Parse(i)).ToArray();

            long maxPhaseSetting = 0;
            int[] phaseSettings = { 0, 1, 2, 3, 4 };
            foreach (var phaseSetting in phaseSettings.GetPermutations(phaseSettings.Length))
            {
                var computerA = new IntCodeComputer(intcode);
                var computerB = new IntCodeComputer(intcode);
                var computerC = new IntCodeComputer(intcode);
                var computerD = new IntCodeComputer(intcode);
                var computerE = new IntCodeComputer(intcode);
                
                var ps = phaseSetting.ToArray();

                var inA = new BlockingCollection<long>();
                var inB = new BlockingCollection<long>();
                var inC = new BlockingCollection<long>();
                var inD = new BlockingCollection<long>();
                var inE = new BlockingCollection<long>();
                var result = new BlockingCollection<long>();

                inA.Add(ps[0]);
                inA.Add(0);
                inB.Add(ps[1]);
                inC.Add(ps[2]);
                inD.Add(ps[3]);
                inE.Add(ps[4]);

                computerA.Execute(inA, inB);
                computerB.Execute(inB, inC);
                computerC.Execute(inC, inD);
                computerD.Execute(inD, inE);
                computerE.Execute(inE, result);
                
                maxPhaseSetting = Math.Max(maxPhaseSetting, result.Take());
            }

            return maxPhaseSetting;   
        }

        public long Part2()
        {
            long[] intcode = input.Split(',').Select(i => long.Parse(i)).ToArray();

        

            long maxPhaseSetting = 0;
            int[] phaseSettings = { 5, 6, 7, 8, 9 };
            foreach (var phaseSetting in phaseSettings.GetPermutations(phaseSettings.Length))
            {
                var computerA = new IntCodeComputer(intcode);
                var computerB = new IntCodeComputer(intcode);
                var computerC = new IntCodeComputer(intcode);
                var computerD = new IntCodeComputer(intcode);
                var computerE = new IntCodeComputer(intcode);

                var ps = phaseSetting.ToArray();

                var inA = new BlockingCollection<long>();
                var inB = new BlockingCollection<long>();
                var inC = new BlockingCollection<long>();
                var inD = new BlockingCollection<long>();
                var inE = new BlockingCollection<long>();

                inA.Add(ps[0]);
                inB.Add(ps[1]);
                inC.Add(ps[2]);
                inD.Add(ps[3]);
                inE.Add(ps[4]);

                var tA = Task.Run(() => computerA.Execute(inA, inB));
                var tB = Task.Run(() => computerB.Execute(inB, inC));
                var tC = Task.Run(() => computerC.Execute(inC, inD));
                var tD = Task.Run(() => computerD.Execute(inD, inE));
                var tE = Task.Run(() => computerE.Execute(inE, inA));

                inA.Add(0);

                Task.WaitAll(tA, tB, tC, tD, tE);


                maxPhaseSetting = Math.Max(maxPhaseSetting, inA.Take());
            }

            return maxPhaseSetting;
        }
    }
}
