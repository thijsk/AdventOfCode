using System;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day17 : IDay
    {
        public long Part1()
        {
            var input = ParseInput3d();
            input.Print();
            for (var cycle = 1; cycle <= 6; cycle++)
            {
                Console.WriteLine($"Cycle {cycle}");
                var i = input.Iterate();
              //  i.Print();
                input = i;
            }

            return input.CountActive();
        }

        public long Part2()
        {
            var input = ParseInput4d();
            input.Print();
            for (var cycle = 1; cycle <= 6; cycle++)
            {
                Console.WriteLine($"Cycle {cycle}");
                var i = input.Iterate();
//                i.Print();
                input = i;
            }

            return input.CountActive();
        }

        public Game3d ParseInput3d()
        {
            var result = new Game3d();
            int z = 0;
            int x = 0;

            foreach (var line in File.ReadAllLines($"Day17.txt"))
            {
                int y = 0;
                foreach (var value in line)
                {
                    result.SetPoint(x, y, z, value == '#');
                    y++;
                }

                x++;
            }

            return result;
        }

        public Game4d ParseInput4d()
        {
            var result = new Game4d();
            int w = 0;
            int z = 0;
            int x = 0;

            foreach (var line in File.ReadAllLines($"Day17.txt"))
            {
                int y = 0;
                foreach (var value in line)
                {
                    result.SetPoint(x, y, z, w, value == '#');
                    y++;
                }

                x++;
            }

            return result;
        }




    }

    class Game3d
    {
        private Collection3D<int, bool> _grid = new Collection3D<int, bool>();
        private int _zmin = 0;
        private int _zmax = 0;
        private int _ymin = 0;
        private int _ymax = 0;
        private int _xmin = 0;
        private int _xmax = 0;

        public Game3d()
        {
        }

        public void Print()
        {
            for (var z = _zmin; z <= _zmax; z++)
            {
                Console.WriteLine($"Z={z}");
                for (var x = _xmin; x <= _xmax; x++)
                {
                    for (var y = _ymin; y <= _ymax; y++)
                    {
                        var value = _grid[x, y, z];
                        Console.Write(value ? '#' : '.');
                    }

                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public void SetPoint(int x, int y, int z, bool value)
        {
            _zmin = Math.Min(_zmin, z);
            _zmax = Math.Max(_zmax, z);
            _ymin = Math.Min(_ymin, y);
            _ymax = Math.Max(_ymax, y);
            _xmin = Math.Min(_xmin, x);
            _xmax = Math.Max(_xmax, x);

            _grid[x, y, z] = value;
        }

        public Game3d Iterate()
        {
            var _copy = new Game3d();

            for (var z = _zmin - 1; z <= _zmax + 1; z++)
            {
                for (var x = _xmin - 1; x <= _xmax + 1; x++)
                {
                    for (var y = _ymin - 1; y <= _ymax + 1; y++)
                    {
                        var newValue = ApplyRules(x, y, z);
                        _copy.SetPoint(x, y, z, newValue);
                    }
                }
            }
            return _copy;
        }

        private bool ApplyRules(in int xp, int yp, in int zp)
        {
            var value = _grid[xp, yp, zp];

            int neighbors = 0;
            for (var z = zp - 1; z <= zp + 1; z++)
            {
                for (var x = xp - 1; x <= xp + 1; x++)
                {
                    for (var y = yp - 1; y <= yp + 1; y++)
                    {
                        if (y == yp && x == xp && z == zp)
                        {

                        }
                        else
                        {
                            neighbors += _grid[x, y, z] ? 1 : 0;
                        }


                    }
                }
            }

            var result = false;

            //If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active.Otherwise, the cube becomes inactive.
            if (value)
            {
                if (neighbors == 2 || neighbors == 3)
                {
                    result = true;
                }
            }
            else //If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
            {
                if (neighbors == 3)
                {
                    result = true;
                }
            }

            return result;
        }

        public long CountActive()
        {
            return _grid.Count(v => v);
        }
    }

    class Game4d
    {
        private Collection4D<int, bool> _grid = new Collection4D<int, bool>();
        private int _wmin = 0;
        private int _wmax = 0;
        private int _zmin = 0;
        private int _zmax = 0;
        private int _ymin = 0;
        private int _ymax = 0;
        private int _xmin = 0;
        private int _xmax = 0;

        public Game4d()
        {
        }

        public void Print()
        {
            for (var w = _wmin; w <= _wmax; w++)
            {
                Console.WriteLine($"W={w}");
                for (var z = _zmin; z <= _zmax; z++)
                {
                    Console.WriteLine($"Z={z}");
                    for (var x = _xmin; x <= _xmax; x++)
                    {
                        for (var y = _ymin; y <= _ymax; y++)
                        {
                            var value = _grid[x, y, z, w];
                            Console.Write(value ? '#' : '.');
                        }

                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
        }

        public void SetPoint(int x, int y, int z, int w, bool value)
        {
            _wmin = Math.Min(_wmin, w);
            _wmax = Math.Max(_wmax, w);
            _zmin = Math.Min(_zmin, z);
            _zmax = Math.Max(_zmax, z);
            _ymin = Math.Min(_ymin, y);
            _ymax = Math.Max(_ymax, y);
            _xmin = Math.Min(_xmin, x);
            _xmax = Math.Max(_xmax, x);

            _grid[x, y, z, w] = value;
        }

        public Game4d Iterate()
        {
            var _copy = new Game4d();

            for (var w = _wmin - 1; w <= _wmax + 1; w++)
            {
                for (var z = _zmin - 1; z <= _zmax + 1; z++)
                {
                    for (var x = _xmin - 1; x <= _xmax + 1; x++)
                    {
                        for (var y = _ymin - 1; y <= _ymax + 1; y++)
                        {
                            var newValue = ApplyRules(x, y, z, w);
                            _copy.SetPoint(x, y, z, w, newValue);
                        }
                    }
                }
            }
            return _copy;
        }

        private bool ApplyRules(in int xp, int yp, in int zp, in int wp)
        {
            var value = _grid[xp, yp, zp, wp];

            int neighbors = 0;
            for (var w = wp - 1; w <= wp + 1; w++)
            {
                for (var z = zp - 1; z <= zp + 1; z++)
                {
                    for (var x = xp - 1; x <= xp + 1; x++)
                    {
                        for (var y = yp - 1; y <= yp + 1; y++)
                        {
                            if (y == yp && x == xp && z == zp && w == wp)
                            {

                            }
                            else
                            {
                                neighbors += _grid[x, y, z, w] ? 1 : 0;
                            }
                        }
                    }
                }
            }

            var result = false;

            //If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active.Otherwise, the cube becomes inactive.
            if (value)
            {
                if (neighbors == 2 || neighbors == 3)
                {
                    result = true;
                }
            }
            else //If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
            {
                if (neighbors == 3)
                {
                    result = true;
                }
            }

            return result;
        }

        public long CountActive()
        {
            return _grid.Count(v => v);
        }
    }


}