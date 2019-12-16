using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp2019
{
    class Day12 : IDay
    {
        private const string input1 = @"<x=0, y=6, z=1>
<x=4, y=4, z=19>
<x=-11, y=1, z=8>
<x=2, y=19, z=15>";

        private const string input2 = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

        private const string input = input1;

        private class Moon
        {
            public Point3<int> position;
            public Point3<int> velocity;

            public Point3<int> startposition;
            public Point3<int> startvelocity;

            public int PotentialEnergy()
            {
                return Math.Abs(position.x) + Math.Abs(position.y) + Math.Abs(position.z);
            }

            public int KineticEnergy()
            {
                return Math.Abs(velocity.x) + Math.Abs(velocity.y) + Math.Abs(velocity.z);
            }

            public bool AtStartX()
            {
                return position.x == startposition.x && velocity.x == startvelocity.x;
            }

            public bool AtStartY()
            {
                return position.y == startposition.y && velocity.y == startvelocity.y;
            }
            public bool AtStartZ()
            {
                return position.z == startposition.z && velocity.z == startvelocity.z;
            }

            public new string ToString()
            {
                return $"{position.x},{position.y},{position.z}";
            }

            internal void UpdateVelocity(Moon other)
            {
                velocity = new Point3<int>(
                    velocity.x + VelocityChange(other.position.x, position.x),
                    velocity.y + VelocityChange(other.position.y, position.y),
                    velocity.z + VelocityChange(other.position.z, position.z)
                    );
            }

            internal void UpdatePosition()
            {
                position = new Point3<int>(
                    position.x + velocity.x,
                    position.y + velocity.y,
                    position.z + velocity.z
                    );
            }

            private int VelocityChange(int other, int mine)
            {
                if (other > mine)
                {
                    return 1;
                }
                else if (other < mine)
                {
                    return -1;
                }
                return 0;
            }
        }

        public long Part1()
        {
            var moons = ParseInput();
            var permutations = moons.GetPermutations(2);

            for (int step = 0; step < 1000; step++)
            {
                //Console.WriteLine($"Step {step+1}");

                foreach (var permutation in permutations)
                {
                    var moon1 = permutation.First();
                    var moon2 = permutation.Last();
                    moon1.UpdateVelocity(moon2);
                }
                foreach (var moon in moons)
                {
                    moon.UpdatePosition();
                }

                //foreach (var moon in moons)
                //{
                //    Console.WriteLine($"pos=<x={moon.position.x}, y={moon.position.y}, z={moon.position.z}>, vel=<x={moon.velocity.x}, y={moon.velocity.y}, z={moon.velocity.z}>");
                //}
            }

            return moons.Sum(m =>
            {
                return m.KineticEnergy() * m.PotentialEnergy();
            });
        }

        private IEnumerable<Moon> ParseInput()
        {
            var moons = new List<Moon>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var moon = new Moon();
                var x = Regex.Match(line, @"x=(-?\d*)").Groups[1];
                var y = Regex.Match(line, @"y=(-?\d*)").Groups[1];
                var z = Regex.Match(line, @"z=(-?\d*)").Groups[1];
                var position = new Point3<int>(int.Parse(x.Value), int.Parse(y.Value), int.Parse(z.Value));
                moon.position = position;
                moon.startposition = moon.position;
                moon.startvelocity = moon.velocity;
                moons.Add(moon);
            }
            return moons;
        }

        public long Part2()
        {
            var moons = ParseInput();
            var permutations = moons.GetPermutations(2);

            int periodx = -1;
            int periody = -1;
            int periodz = -1;

            int step = 0;

            while (periodx == -1 || periody == -1 || periodz == -1)
            {
                step++;
               
                foreach (var permutation in permutations)
                {
                    var moon1 = permutation.First();
                    var moon2 = permutation.Last();
                    moon1.UpdateVelocity(moon2);
                }
                foreach (var moon in moons)
                {
                    moon.UpdatePosition();
                }

                if (moons.All(m => m.AtStartX()) && periodx == -1)
                {
                    periodx = step;
                }


                if (moons.All(m => m.AtStartY()) && periody == -1)
                {
                    periody = step;
                }

                if (moons.All(m => m.AtStartZ()) && periodz == -1)
                {
                    periodz = step;
                }

                //foreach (var moon in moons)
                //{
                //    Console.WriteLine($"pos=<x={moon.position.x}, y={moon.position.y}, z={moon.position.z}>, vel=<x={moon.velocity.x}, y={moon.velocity.y}, z={moon.velocity.z}>");
                //}
            }
            Console.WriteLine($"{periodx} {periody} {periodz}");
            Math2.LeastCommonMultiple(4, 4, 5);

            return Math2.LeastCommonMultiple(periodx, periody, periodz);
        }
    }
}
