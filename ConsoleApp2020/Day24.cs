using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day24 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var map = RunInput(input);

            return map.Values.Count(v => v);
        }

        private Dictionary<Hex, bool> RunInput(List<List<Direction>> input)
        {
            var map = new Dictionary<Hex, bool>();
            foreach (var track in input)
            {
                var from = new Hex(0, 0, 0);
                foreach (var direction in track)
                {
                    var to = Hex.Neighbor(@from, (int)direction);
                    @from = to;
                }

                FlipHex(map, @from);
            }

            return map;
        }

        private void FlipHex(Dictionary<Hex, bool> map, Hex hex)
        {
            if (map.ContainsKey(hex))
            {
                map[hex] = !map[hex];
            }
            else
            {
                map.Add(hex, true);
            }
        }

        private bool GetHex(Dictionary<Hex, bool> map, Hex hex)
        {
            if (map.ContainsKey(hex))
            {
                return map[hex];
            }

            return false;
        }

        private Direction StringToDirection(string value)
        {
            Direction result;
            Direction.TryParse(value, out result);
            return result;
        }

        enum Direction
        {
            //e, se, sw, w, nw, and ne
            e = 0,
            se = 1,
            sw = 2,
            w = 3,
            nw = 4,
            ne = 5
        }

        public long Part2()
        {
            var input = ParseInput();

            var map = RunInput(input);

            var alldirections = new[] {0, 1, 2, 3, 4, 5};


            var blacktiles = map.Where(kv => kv.Value).Select(kv => kv.Key).ToHashSet();

            for (int day = 1; day <= 100; day++)
            {
                var newblacktiles = new HashSet<Hex>(blacktiles.Count);

                foreach (var black in blacktiles)
                {
                    var blackneighbors = FindNeighbors(black).Count(n => blacktiles.Contains(n));
                    if (blackneighbors != 0 && blackneighbors <= 2)
                    {
                        newblacktiles.Add(black);
                    }
                }

                var whitetiles = blacktiles.SelectMany(FindNeighbors).Distinct();
                foreach (var white in whitetiles)
                {
                    var blackneighbors = FindNeighbors(white).Count(n => blacktiles.Contains(n));
                    if (blackneighbors == 2)
                    {
                        newblacktiles.Add(white);
                    }
                }

                blacktiles = newblacktiles;
                Console.WriteLine($"Day {day} : {blacktiles.Count}");
            }

            return blacktiles.Count();
        }

        private static readonly int[] alldirections =  { 0, 1, 2, 3, 4, 5 };

        public long Part2a()
        {
            var input = ParseInput();

            var map = RunInput(input);

            for (int day = 1; day <= 100; day++)
            {
                var nextmap = new Dictionary<Hex, bool>(map.Count);
                var minq = map.Keys.Min(h => h.q);
                var maxq = map.Keys.Max(h => h.q);
                var minr = map.Keys.Min(h => h.r);
                var maxr = map.Keys.Max(h => h.r);
                var mins = map.Keys.Min(h => h.s);
                var maxs = map.Keys.Max(h => h.s);

                for (int q = minq - 1; q <= maxq + 1; q++)
                    for (int r = minr - 1; r <= maxr + 1; r++)
                        for (int s = mins - 1; s <= maxs + 1; s++)
                        {
                            var current = new Hex(q, r, s);
                            var mycolor = GetHex(map, current);

                            var neighbors = FindNeighbors(current).Select(h => GetHex(map, h)).Count(n => n);
                            if (mycolor)
                            {
                                if (neighbors == 0 || neighbors > 2)
                                {
                                    mycolor = false;
                                }
                            }
                            else
                            {
                                if (neighbors == 2)
                                {
                                    mycolor = true;
                                }
                            }
                            nextmap.Add(current, mycolor);
                        }

                map = nextmap;
                var black = map.Values.Count(v => v);
                Console.WriteLine($"Day {day} : {black}");
            }

            return map.Values.Count(v => v);
        }

        private static Hex[] FindNeighbors(Hex current)
        {
            var result = new Hex[6];
            foreach (var direction in alldirections)
            {
                var neighbor = Hex.Neighbor(current, direction);
                result[direction] = neighbor;
            }

            return result;
        }

        private List<List<Direction>> ParseInput()
        {
            //e, se, sw, w, nw, and ne
            var result = new List<List<Direction>>();
            foreach (var line in File.ReadAllLines($"Day24.txt"))
            {
                var directions = new List<string>();
                char tmp = ' ';
                foreach (var c in line)
                {
                    if (tmp == ' ')
                    {
                        if (c == 'e' || c == 'w')
                        {
                            directions.Add($"{c}");
                        }
                        else
                        {
                            tmp = c;
                        }
                    }
                    else
                    {
                        directions.Add($"{tmp}{c}");
                        tmp = ' ';
                    }
                }
                result.Add(directions.Select(StringToDirection).ToList());
            }

            return result;
        }
    }
}