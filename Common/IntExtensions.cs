using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Common
{
    public static class IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> ToDigits(this int number)
        {
            return number.ToString().ToInts();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this int i, int lowerbound, int upperbound)
        {
            return i >= lowerbound && i <= upperbound;
        }
    }
}
