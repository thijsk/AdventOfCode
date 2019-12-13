using Common;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2019
{
    class Day4 : IDay
    {
        public int Part1()
        {
            var ok = 0;
            for (var number = 138307; number <= 654504; number++)
            {
                var d = ToDigits(number);
                if (d[0] <= d[1] &&
                    d[1] <= d[2] &&
                    d[2] <= d[3] &&
                    d[3] <= d[4] &&
                    d[4] <= d[5] &&
                    (d[0] == d[1] ||
                     d[1] == d[2] ||
                     d[2] == d[3] ||
                     d[3] == d[4] ||
                     d[4] == d[5])
                    )
                {
                    ok++;
                }
            }
            return ok;
        }

        private static int[] ToDigits(int number)
        {
            return number.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        public int Part2()
        {
            var ok = 0;
            for (var number = 138307; number <= 654504; number++)
            {
                var d = ToDigits(number);
                if (d[0] <= d[1] &&
                    d[1] <= d[2] &&
                    d[2] <= d[3] &&
                    d[3] <= d[4] &&
                    d[4] <= d[5] &&
                    ((d[0] == d[1] && d[1] != d[2])||
                     (d[1] == d[2] && d[0] != d[1] && d[2] != d[3])||
                     (d[2] == d[3] && d[1] != d[2] && d[3] != d[4]) ||
                     (d[3] == d[4] && d[2] != d[3] && d[4] != d[5]) ||
                     (d[4] == d[5] && d[3] != d[4])
                     )
                    )
                {
                    ok++;
                }
            }
            return ok;
        }
    }
}
