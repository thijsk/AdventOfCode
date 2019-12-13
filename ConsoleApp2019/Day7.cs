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

        public int Part1()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();

            var computerA = new IntCodeComputer(intcode);
            var computerB = new IntCodeComputer(intcode);
            var computerC = new IntCodeComputer(intcode);
            var computerD = new IntCodeComputer(intcode);
            var computerE = new IntCodeComputer(intcode);

            int maxPhaseSetting = 0;
            int[] phaseSettings = { 0, 1, 2, 3, 4 };
            foreach (var phaseSetting in phaseSettings.GetPermutations(phaseSettings.Length))
            {
                var ps = phaseSetting.ToArray();

                var inA = new BlockingCollection<int>();
                var inB = new BlockingCollection<int>();
                var inC = new BlockingCollection<int>();
                var inD = new BlockingCollection<int>();
                var inE = new BlockingCollection<int>();

                inA.Add(ps[0]);
                inA.Add(0);
                inB.Add(ps[1]);
                inC.Add(ps[2]);
                inD.Add(ps[3]);
                inE.Add(ps[4]);

                var phaseResult = 0;
                computerA.Execute((p) => inA.Take(), (p, o) => inB.Add(o));
                computerB.Execute((p) => inB.Take(), (p, o) => inC.Add(o));
                computerC.Execute((p) => inC.Take(), (p, o) => inD.Add(o));
                computerD.Execute((p) => inD.Take(), (p, o) => inE.Add(o));
                computerE.Execute((p) => inE.Take(), (p, o) => phaseResult = o);
                
                maxPhaseSetting = Math.Max(maxPhaseSetting, phaseResult);
            }

            return maxPhaseSetting;   
        }

        public int Part2()
        {
            int[] intcode = input.Split(',').Select(i => int.Parse(i)).ToArray();

            var computerA = new IntCodeComputer(intcode);
            var computerB = new IntCodeComputer(intcode);
            var computerC = new IntCodeComputer(intcode);
            var computerD = new IntCodeComputer(intcode);
            var computerE = new IntCodeComputer(intcode);

            int maxPhaseSetting = 0;
            int[] phaseSettings = { 5, 6, 7, 8, 9 };
            foreach (var phaseSetting in phaseSettings.GetPermutations(phaseSettings.Length))
            {
                var ps = phaseSetting.ToArray();

                var inA = new BlockingCollection<int>();
                var inB = new BlockingCollection<int>();
                var inC = new BlockingCollection<int>();
                var inD = new BlockingCollection<int>();
                var inE = new BlockingCollection<int>();

                inA.Add(ps[0]);
                inB.Add(ps[1]);
                inC.Add(ps[2]);
                inD.Add(ps[3]);
                inE.Add(ps[4]);

                var phaseResult = 0;
                var tA = Task.Run(() => computerA.Execute((p) => inA.Take(), (p, o) => inB.Add(o)));
                var tB = Task.Run(() => computerB.Execute((p) => inB.Take(), (p, o) => inC.Add(o)));
                var tC = Task.Run(() => computerC.Execute((p) => inC.Take(), (p, o) => inD.Add(o)));
                var tD = Task.Run(() => computerD.Execute((p) => inD.Take(), (p, o) => inE.Add(o)));
                var tE = Task.Run(() => computerE.Execute((p) => inE.Take(), (p, o) => { phaseResult = o; inA.Add(o); }));

                inA.Add(0);

                Task.WaitAll(tA, tB, tC, tD, tE);


                maxPhaseSetting = Math.Max(maxPhaseSetting, phaseResult);
            }

            return maxPhaseSetting;
        }
    }
}
