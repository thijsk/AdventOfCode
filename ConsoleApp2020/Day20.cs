using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;

namespace ConsoleApp2020
{
    class Day20 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            input.ForEach(i =>
            {
                ConsoleX.WriteLine(i);
                ConsoleX.WriteLine("Edges:");
                i.GetEdges().ForEach(e => ConsoleX.WriteLine(e));
                ConsoleX.WriteLine();
            });


            long result = 1;
            input.ForEach(p =>
            {
                var myEdges = p.GetEdges().Union(p.GetEdges().Select(e => e.Backwards()));
                var otherEdges = input.Where(i => i.Id != p.Id).SelectMany(i => i.GetEdges()).Distinct().ToList();
                var matches = myEdges.Count(e => otherEdges.Contains(e));
               if (matches == 2)
               {
                   ConsoleX.WriteLine($"{p.Id} : {matches}");
                   result *= p.Id;
               }
            });

            return result;
        }

        public long Part2()
        {
            var input = ParseInput();

            return 0;
        }

        public List<Tile> ParseInput()
        {
            var pieces = new List<Tile>();
            Tile piece = null;
            foreach (var line in File.ReadAllLines($"Day20.txt"))
            {
                if (line.StartsWith("Tile"))
                {
                    var id = Convert.ToInt32(line.Replace("Tile ", "").Replace(":", ""));
                    piece = new Tile(id);
                    pieces.Add(piece);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    piece?.LoadLine(line);
                }
            }
            pieces.ForEach(p => p.Initialize());
            return pieces;
        }
    }

    class Tile
    {
        public int Id => _id;

        private int _id;
        private List<string> _lines;
        private char[,] _grid;

        public Tile(int id)
        {
            _id = id;
            _lines = new List<string>();
        }

        public void LoadLine(string line)
        {
            _lines.Add(line);
        }

        public void Initialize()
        {
            _grid = new char[_lines[0].Length, _lines.Count];
            var row = 0;
            foreach (var line in _lines)
            {
                var col = 0;
                foreach (var chr in line)
                {
                    _grid[row, col] = chr;
                    col++;
                }

                row++;
            }

            _lines.Clear();
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"Piece {_id}:{Environment.NewLine}");
            for (int row = 0; row <= NrRows; row++)
            {
                for (int col = 0; col <= NrCols; col++)
                {
                    result.Append(_grid[row, col]);
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public int NrCols => _grid.GetUpperBound(1);

        public int NrRows => _grid.GetUpperBound(0);

        public List<string> GetEdges()
        {
            var north = String.Concat(GetArea(0, 0, 0, NrCols, false));
            var west = String.Concat(GetArea(0, NrRows, NrCols, NrCols, false));
            var south = String.Concat(GetArea(NrRows, NrRows, 0, NrCols, true));
            var east = String.Concat(GetArea(0, NrRows, 0, 0, true));

            return new List<string>() { north, west, south, east};
        }

        private List<char> GetArea(int rowstart, int rowend, int colstart, int colend, bool reverse)
        {
            var result = new List<char>();
            for (int row = rowstart; row <= rowend; row++)
            {
                for (int col = colstart; col <= colend; col++)
                {
                    result.Add(_grid[row, col]);
                }
            }

            if (reverse)
            {
                result.Reverse();
            }
            return result;
        }
    }
}