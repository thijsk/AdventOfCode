using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2017
{
    class Day13 : IDay
    {
        private string input = @"0: 4
1: 2
2: 3
4: 5
6: 8
8: 6
10: 4
12: 6
14: 6
16: 8
18: 8
20: 6
22: 8
24: 8
26: 8
28: 12
30: 12
32: 9
34: 14
36: 12
38: 12
40: 12
42: 12
44: 10
46: 12
48: 12
50: 10
52: 14
56: 12
58: 14
62: 14
64: 14
66: 12
68: 14
70: 14
72: 17
74: 14
76: 14
80: 20
82: 14
90: 24
92: 14
98: 14";

        class FirewallLayer
        {
            public int Number;
            public int Range;
            private int _position = 1;
            private int _increment = -1;

            public void Reset()
            {
                _position = 1;
                _increment = -1;
            }

            public FirewallLayer Clone()
            {
                return new FirewallLayer()
                {
                    Number = this.Number,
                    Range = this.Range
                };
            }

            public void Step()
            {
                if (IsInTopPosition() || IsInBottomPosition())
                    _increment = _increment * -1;
                _position += _increment;
            }

            public bool IsInTopPosition() => _position == 1;

            public bool IsInBottomPosition() => _position == Range;
        }

        public long Part1()
        {
            var layers = ParseInput();
            return GetScore(layers);
        }

        private int GetScore(Dictionary<int, FirewallLayer> layers, int delay = 0, bool earlyout = false)
        {
           
            for (int i = 0; i < delay; i++)
            {
                foreach (var l in layers.Values)
                {
                    l.Step();
                }
            }

            var finish = layers.Values.Max(l => l.Number);
            var score = 0;

            for (var depth = 0; depth <= finish; depth++)
            {
                if (layers.TryGetValue(depth, out var fw))
                {
                    if (fw.IsInTopPosition())
                    {
                        if (earlyout)
                            return int.MaxValue;
                        score += depth * fw.Range;
                    }
                }

                foreach (var l in layers.Values)
                {
                    l.Step();
                }
            }

            return score;
        }

        private Dictionary<int, FirewallLayer> ParseInput()
        {
            var result = new Dictionary<int, FirewallLayer>();
            foreach (var line in input.Split('\n').Select(l => l.Trim().Split(':').Select(i => i.Trim()).ToArray()))
            {
                var fw = new FirewallLayer
                {
                    Number = int.Parse(line[0]),
                    Range = int.Parse(line[1])
                };

                result.Add(fw.Number, fw);
            }
            return result;
        }

        public long Part2()
        {
           

            var score = int.MaxValue;
            var delay = 0;
            bool go = true;
            while (go)
            {
                Parallel.ForEach(Enumerable.Range(delay, 10), d =>
                {
                    var layers = ParseInput();
                    for (var i = d*1000; i <= (d*1000)+1000; i++)
                    {
                        score = GetScore(layers, i, true);
                        if (score == 0)
                        {
                            Console.WriteLine(i);
                            go = false;
                        }
                    }
                });
                delay += 1000;
            }
            return 0;
        }
    }
}
