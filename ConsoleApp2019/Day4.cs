using Common;
using System.Linq;

namespace ConsoleApp2019
{
    class Day4 : IDay
    {
        public long Part1()
        {
            var ok = 0;
            for (var number = 138307; number <= 654504; number++)
            {
                var d = number.ToDigits().ToArray();
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

        

        public long Part2()
        {
            var ok = 0;
            for (var number = 138307; number <= 654504; number++)
            {
                var d = number.ToDigits().ToArray();
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
