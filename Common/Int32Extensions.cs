using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Int32Extensions
    {
        public static bool Between(this int i, int lowerbound, int upperbound)
        {
            return i >= lowerbound && i <= upperbound;
        }
    }
}
