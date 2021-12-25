using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class RangeExtensions
    {
        public static IEnumerable<int> AsEnumerable(this Range range)
        {
            if (range.End.IsFromEnd)
                throw new InvalidOperationException("Range must have a concrete End value");
            if (range.End.Value >= range.Start.Value)
            {
                var index = range.Start.Value;
                do yield return index;
                while (index++ < range.End.Value);
            }
            else
            {
                var index = range.Start.Value;
                do yield return index;
                while (index-- > range.End.Value);
            }
        }
    }
}
