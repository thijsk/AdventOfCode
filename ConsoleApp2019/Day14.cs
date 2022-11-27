using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2019
{
    class Day14 : IDay
    {
        private const string input1 = @"2 LFPRM, 4 GPNQ => 2 VGZVD
1 KXFHM, 14 SJLP => 8 MGRTM
2 HBXVT, 3 HNHC, 5 BDLV => 1 DKTW
2 MGRTM, 8 RVTB => 4 DFMW
2 SJLP => 9 PXTS
1 NXBG => 6 FXBXZ
32 LPSQ => 9 GSDXD
13 LZGTR => 4 ZRMJ
1 FTPQ, 16 CPCS => 5 HNHC
2 THQH, 2 NDJG, 5 MSKT => 4 LRZV
2 BDLV, 9 HBXVT, 21 NXBG => 7 PLRK
16 LNSKQ, 41 KXFHM, 1 DKTW, 1 NCPSZ, 3 ZCSB, 11 MGRTM, 19 WNJWP, 11 KRBG => 1 FUEL
5 FTPQ, 1 HBXVT => 4 BDLV
15 LSDX, 1 GFJW, 1 QDHJT => 4 NKHQV
9 CZHTP, 1 FRPTK => 6 SNBS
17 LFLVS, 2 WCFT => 8 KGJQ
6 CMHLP => 1 SJLP
144 ORE => 3 KQKXZ
3 GFJW, 1 RVTB, 1 GPNQ => 2 NXBG
4 BDLV => 5 CMHLP
2 LSDX => 1 LZGTR
156 ORE => 3 NDJG
136 ORE => 8 MSKT
4 BDLV, 1 NKHQV, 1 RVTB => 7 LNSKQ
1 LRZV, 3 WCFT => 2 HBXVT
5 KGJQ, 1 SWBSN => 7 QHFX
2 DQHBG => 4 LPSQ
6 GSDXD => 3 LSDX
11 RWLD, 3 BNKVZ, 4 PXTS, 3 XTRQC, 5 LSDX, 5 LMHL, 36 MGRTM => 4 ZCSB
8 CPCS => 2 FRPTK
5 NDJG => 3 WCFT
1 GDQG, 1 QHFX => 4 KXFHM
160 ORE => 3 THQH
20 GFJW, 2 DQHBG => 6 RVTB
2 FXBXZ, 1 WNJWP, 1 VGZVD => 5 RWLD
3 DQHBG => 7 SWBSN
7 QHFX => 8 CPCS
14 HBXVT => 3 VCDW
5 FRPTK => 7 NGDX
1 HWFQ => 4 LFLVS
2 CPCS => 6 ZTKSW
9 KGJQ, 8 ZTKSW, 13 BDLV => 6 GDQG
13 LMHL, 1 LZGTR, 18 BNKVZ, 11 VCDW, 9 DFMW, 11 FTPQ, 3 RWLD => 4 KRBG
1 XRCH => 7 GPNQ
3 WCFT => 9 DQHBG
1 FTPQ => 8 CZHTP
1 PBMR, 2 ZTKSW => 2 BNKVZ
2 PLRK, 3 CPCS => 8 ZSGBG
3 NGDX, 3 XRCH => 6 XTRQC
6 ZTKSW, 11 HNHC, 22 SNBS => 9 WNJWP
5 KQKXZ => 8 HWFQ
23 WCFT => 7 PBMR
1 LRZV, 1 QDHJT => 2 GFJW
1 ZSGBG, 5 CGTHV, 9 ZRMJ => 3 LMHL
1 DQHBG => 9 XRCH
1 GDQG, 17 RWLD, 2 KGJQ, 8 VCDW, 2 BNKVZ, 2 WNJWP, 1 VGZVD => 3 NCPSZ
19 SJLP, 3 ZTKSW, 1 CZHTP => 4 LFPRM
14 SNBS => 8 CGTHV
3 DQHBG, 4 WCFT => 1 FTPQ
3 MSKT, 3 NDJG => 5 QDHJT";

        private const string input2 = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL";

        private const string input3 = @"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL";

        private const string input = input1;

        private class Element
        {
            public string name;
            public int amount;

            public IEnumerable<(string, int)> inputs;

            public int OreNeeded(Dictionary<string, Element> d, Dictionary<string, int> w)
            {
                if (name == "ORE")
                    return amount;
                else
                {
                    return inputs.Sum(i =>
                    {
                        var e = d[i.Item1];
                        var amountneeded = (int)Math.Ceiling((double)(i.Item2 - w[e.name]) / e.amount);
                        var waste = (amountneeded * e.amount) - i.Item2;
                        w[e.name] += waste;

                        //Console.WriteLine($"{amount} {name} needs {amountneeded * e.amount} {e.name} with {waste} waste");
                        return Enumerable.Range(0, amountneeded).Sum((ei) =>
                        {
                            return e.OreNeeded(d, w);
                        });

                    });
                }
            }
        }



        public long Part1()
        {
            var d = new Dictionary<string, Element>();

            d.Add("ORE", new Element() { name = "ORE", amount = 1 });

            foreach (var line in input.Split(Environment.NewLine))
            {
                var splits = line.Split("=>");
                var estr = splits.Last().Trim().Split(' ');
                var element = new Element() { name = estr[1], amount = int.Parse(estr[0]) };
                var istr = splits.Reverse().Skip(1).First().Split(',');
                var li = new List<(string, int)>();
                foreach (var i in istr)
                {
                    var s = i.Trim().Split(' ');
                    li.Add((s[1], int.Parse(s[0])));
                }
                element.inputs = li;
                d.Add(element.name, element);
            }

            var waste = new Dictionary<string, int>();
            foreach (var e in d.Keys)
            {
                waste.Add(e, 0);
            }

            return d["FUEL"].OreNeeded(d, waste);
        }

        public long Part2()
        {
            var d = new Dictionary<string, Element>();

            d.Add("ORE", new Element() { name = "ORE", amount = 1 });

            foreach (var line in input.Split(Environment.NewLine))
            {
                var splits = line.Split("=>");
                var estr = splits.Last().Trim().Split(' ');
                var element = new Element() { name = estr[1], amount = int.Parse(estr[0]) };
                var istr = splits.Reverse().Skip(1).First().Split(',');
                var li = new List<(string, int)>();
                foreach (var i in istr)
                {
                    var s = i.Trim().Split(' ');
                    li.Add((s[1], int.Parse(s[0])));
                }
                element.inputs = li;
                d.Add(element.name, element);
            }

            var waste = new Dictionary<string, int>();
            foreach (var e in d.Keys)
            {
                waste.Add(e, 0);
            }

            long fuel = 0;
            long ore = 1000000000000;
            while (ore > 0)
            {
                ore -= (d["FUEL"].OreNeeded(d, waste));
                if (ore > 0)
                {
                    fuel++;
                }

                if (fuel % 1000 == 0)
                {
                    Console.WriteLine($"{fuel} {ore}");
                }

                if (waste.All(w => w.Value ==0))
                {
                    Console.WriteLine($"No more waste at {fuel}");
                }

            }
            return fuel;
        }
    }
}
