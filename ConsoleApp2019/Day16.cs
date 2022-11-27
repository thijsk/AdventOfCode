using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2019
{
    class Day16 : IDay
    {
        private const string input = "59734319985939030811765904366903137260910165905695158121249344919210773577393954674010919824826738360814888134986551286413123711859735220485817087501645023012862056770562086941211936950697030938202612254550462022980226861233574193029160694064215374466136221530381567459741646888344484734266467332251047728070024125520587386498883584434047046536404479146202115798487093358109344892308178339525320609279967726482426508894019310795012241745215724094733535028040247643657351828004785071021308564438115967543080568369816648970492598237916926533604385924158979160977915469240727071971448914826471542444436509363281495503481363933620112863817909354757361550";
        private const string input2 = "12345678";

        public long Part1()
        {
            var inp = input.ToInts().ToArray();
            
            foreach (var phase in Enumerable.Range(1, 100))
            {
                var result = Enumerable.Range(1, inp.Count()).Select(position =>
                {
                    var pattern = GetPattern(position).ToList();
                    return TrimDigit(inp.Zip(pattern.AsCircular().Skip(1), (i, p) =>
                    {
                        return i * p;
                    }).Sum());
                }).ToArray();

                inp = result;
            }
            return long.Parse(string.Concat(inp.Take(8)));
        }

        private int TrimDigit(int digit)
        {
            //var lastDigit =  digit.ToString().ToCharArray().Last().ToString();
            //return int.Parse(lastDigit);
            return Math.Abs(digit) % 10;
        }

        private static IEnumerable<int> GetPattern(int position)
        {
            var basepattern = new[] { 0, 1, 0, -1 };
            return basepattern.SelectMany(d =>
            {
                return Enumerable.Range(1, position).Select(r => d);
            });
        }

        public long Part2()
        {
            var inp = input.ToInts().ToList().Repeat(10000).ToArray();
            var offset = int.Parse(string.Concat(input.Take(7)));

            foreach (var phase in Enumerable.Range(1, 100))
            {
                Console.WriteLine(phase);

                var result = Enumerable.Range(1, inp.Count()).Select(position =>
                {
                    if (position < (offset - 100)) 
                        return 0;
                    var pattern = GetPattern(position).Skip(1);
                    //if (pattern.Count() < inp.Count())
                    //    pattern = pattern.ToArray().AsCircular();
                    return TrimDigit(inp.Skip(position-1).Zip(pattern.Skip(position-1), (i, p) =>
                    {
                        return i * p;
                    }).Sum());
                }).ToArray();
                inp = result;
            }
            
            return long.Parse(string.Concat(inp.Take(8)));
        }
    }
}
