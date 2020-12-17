using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class IntExtensions
    {
        public static IEnumerable<int> ToDigits(this int number)
        {
            return number.ToString().ToInts();
        }

        public static bool Between(this int i, int lowerbound, int upperbound)
        {
            return i >= lowerbound && i <= upperbound;
        }
    }
}
