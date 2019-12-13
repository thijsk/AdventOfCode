using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day14 : IDay
    {
        private string input = @"Vixen can fly 8 km/s for 8 seconds, but then must rest for 53 seconds.
Blitzen can fly 13 km/s for 4 seconds, but then must rest for 49 seconds.
Rudolph can fly 20 km/s for 7 seconds, but then must rest for 132 seconds.
Cupid can fly 12 km/s for 4 seconds, but then must rest for 43 seconds.
Donner can fly 9 km/s for 5 seconds, but then must rest for 38 seconds.
Dasher can fly 10 km/s for 4 seconds, but then must rest for 37 seconds.
Comet can fly 3 km/s for 37 seconds, but then must rest for 76 seconds.
Prancer can fly 9 km/s for 12 seconds, but then must rest for 97 seconds.
Dancer can fly 37 km/s for 1 seconds, but then must rest for 36 seconds.";

        class Reindeer
        {
            private string _name;
            private int _speed;
            private int _active;
            private int _sleep;
            private int _travelled;
            private bool _isActive;

            public Reindeer(string name, int speed, int active, int sleep)
            {
                this._name = name;
                this._speed = speed;
                this._active = active;
                this._sleep = sleep;
                _isActive = true;
                _travelled = 0;
            }

            private int _currentTick = 0;
            public int Distance => _travelled;
            public string Name => _name;

            public int Points;

            public void Tick()
            {
                _currentTick++;

            

                if (_currentTick > _active)
                    _isActive = false;
                if (_currentTick > _active + _sleep)
                {
                    _currentTick = 1;
                    _isActive = true;
                }

                if (_isActive)
                    _travelled += _speed;




            }

        }


        public long Part1()
        {
//            input = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
//Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";
            var herd = Parse();

            foreach (var deer in herd)
            {
                for (int i = 1; i <= 2503; i++)
                {
                    deer.Tick();
                    //Console.WriteLine($"{i} {deer.Name} {deer.Distance}");
                }
                Console.WriteLine($"{deer.Name} {deer.Distance}");
            }

            var winner = herd.Max(d => d.Distance);

            return winner;
        }

        public long Part2()
        {
            //            input = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
            //Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";
            var herd = Parse();

            for (int i = 1; i <= 2503; i++)
            {
                foreach (var deer in herd)
                  {
               
                    deer.Tick();
                    //Console.WriteLine($"{i} {deer.Name} {deer.Distance}");
                }

                var lead = herd.Max(h => h.Distance);

                foreach (var head in herd.Where(d => d.Distance == lead))
                {
                    head.Points++;
                }


            }

            var winner = herd.Max(d => d.Points);

            return winner;
        }

        private IEnumerable<Reindeer> Parse()
        {
            var result = new List<Reindeer>();
            foreach (var deertxt in input.Split('\n'))
            {
                //Dancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.";
                var parts = deertxt.Split(' ');
                var deer = new Reindeer(parts[0], int.Parse(parts[3]), int.Parse(parts[6]), int.Parse(parts[13]));
                result.Add(deer);
            }
            return result;
        }
      
    }
}
