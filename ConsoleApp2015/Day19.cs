using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day19 : IDay
    {
        private string inputreplacements = @"Al => ThF
Al => ThRnFAr
B => BCa
B => TiB
B => TiRnFAr
Ca => CaCa
Ca => PB
Ca => PRnFAr
Ca => SiRnFYFAr
Ca => SiRnMgAr
Ca => SiTh
F => CaF
F => PMg
F => SiAl
H => CRnAlAr
H => CRnFYFYFAr
H => CRnFYMgAr
H => CRnMgYFAr
H => HCa
H => NRnFYFAr
H => NRnMgAr
H => NTh
H => OB
H => ORnFAr
Mg => BF
Mg => TiMg
N => CRnFAr
N => HSi
O => CRnFYFAr
O => CRnMgAr
O => HP
O => NRnFAr
O => OTi
P => CaP
P => PTi
P => SiRnFAr
Si => CaSi
Th => ThCa
Ti => BP
Ti => TiTi
e => HF
e => NAl
e => OMg";

        private string inputmolecule =
                @"CRnCaCaCaSiRnBPTiMgArSiRnSiRnMgArSiRnCaFArTiTiBSiThFYCaFArCaCaSiThCaPBSiThSiThCaCaPTiRnPBSiThRnFArArCaCaSiThCaSiThSiRnMgArCaPTiBPRnFArSiThCaSiRnFArBCaSiRnCaPRnFArPMgYCaFArCaPTiTiTiBPBSiThCaPTiBPBSiRnFArBPBSiRnCaFArBPRnSiRnFArRnSiRnBFArCaFArCaCaCaSiThSiThCaCaPBPTiTiRnFArCaPTiBSiAlArPBCaCaCaCaCaSiRnMgArCaSiThFArThCaSiThCaSiRnCaFYCaSiRnFYFArFArCaSiRnFYFArCaSiRnBPMgArSiThPRnFArCaSiRnFArTiRnSiRnFYFArCaSiRnBFArCaSiRnTiMgArSiThCaSiThCaFArPRnFArSiRnFArTiTiTiTiBCaCaSiRnCaCaFYFArSiThCaPTiBPTiBCaSiThSiRnMgArCaF"
            ;

        public long Part1()
        {
            var replacements = inputreplacements.Split('\n').Select(i => i.Trim().Split(" => "));

            var results = new List<string>();

            foreach (var replacement in replacements)
            {
                results.AddRange(inputmolecule.AllIndexesOf(replacement[0]).Select(i =>
                    inputmolecule.ReplaceAtIndex(i, replacement[0].Length, replacement[1])));
            }

            return results.Distinct().Count();
        }

        public long Part2()
        {
            var replacements = inputreplacements.Split('\n').Select(i => i.Trim().Split(" => "));

            var shortest = int.MaxValue;
            var shortestfoundcounter = 0;

            while (shortestfoundcounter < 10)
            {
                var counter = 0;
                var result = inputmolecule;
                var lastLength = inputmolecule.Length;
                while (result != "e")
                {
                    foreach (var replacement in replacements.OrderBy(o => Guid.NewGuid()).ToList())
                    {
                        var index = result.IndexOf(replacement[1]);
                        if (index != -1)
                        {
                            counter ++;
                            result = result.ReplaceAtIndex(index, replacement[1].Length, replacement[0]);
                        }
                    }

                    if (lastLength == result.Length)
                    {
                        counter = 0;
                        lastLength = inputmolecule.Length;
                        result = inputmolecule;
                    }
                    else
                    {
                        lastLength = result.Length;
                    }
                }

                shortest = Math.Min(counter, shortest);
                if (shortest == counter)
                {
                    Console.WriteLine(shortest + "...");
                    shortestfoundcounter++;
                }

            }
            return shortest;
        }
    }
}
