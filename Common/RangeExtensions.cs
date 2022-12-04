using System;
using System.Collections.Generic;

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

        public static RangeEnumerator GetEnumerator(this Range range)
        {
            return new RangeEnumerator(range);
        }

        public ref struct RangeEnumerator
        {
            private int _current;
            private int _end;

            public int Current => _current;

            public bool MoveNext()
            {
                _current++;
                return _current <= _end;
            }

            public RangeEnumerator(Range range)
            {
                if (range.End.IsFromEnd)
                    throw new InvalidOperationException("Range must have a concrete End value");

                _current = range.Start.Value - 1;
                _end = range.End.Value;
            }
        }

        public static bool Contains(this Range first, Range second)
        {
            return (second.Start.Value >= first.Start.Value) && (second.End.Value <= first.End.Value);
        }

        public static bool Overlaps(this Range first, Range second)
        {
            return (second.Start.Value <= first.End.Value) && (first.Start.Value <= second.End.Value);
        }
    }
}
