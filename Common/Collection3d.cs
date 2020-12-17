using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Collection3D<TIndex, TValue> : IEnumerable<TValue>
    {
        private readonly SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>> xdic;

        public Collection3D()
        {
            xdic = new SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>>();
        }

        public TValue this[TIndex x, TIndex y, TIndex z]
        {
            get
            {
                return
                    xdic.TryGetValue(x, out var ydic) &&
                    ydic.TryGetValue(y, out var zdic) &&
                    zdic.TryGetValue(z, out var value)
                        ? value
                        : default(TValue);
            }
            set
            {
                if (!xdic.TryGetValue(x, out var ydic))
                {
                    xdic[x] = (ydic = new SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>());
                }

                if (!ydic.TryGetValue(y, out var zdic))
                {
                    ydic[y] = (zdic = new SortedDictionary<TIndex, TValue>());
                }

                zdic[z] = value;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var ydic in xdic.Values)
            {
                foreach (var zdic in ydic.Values)
                {
                    foreach (var value in zdic.Values)
                    {
                        yield return value;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
