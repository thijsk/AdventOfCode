using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class Collection4D<TIndex, TValue> : IEnumerable<TValue>
    {
        private readonly SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>>> xdic;

        public Collection4D()
        {
            xdic = new SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>>>();
        }

        public TValue this[TIndex x, TIndex y, TIndex z, TIndex w]
        {
            get
            {
                return
                    xdic.TryGetValue(x, out var ydic) &&
                    ydic.TryGetValue(y, out var zdic) &&
                    zdic.TryGetValue(z, out var wdic) &&
                    wdic.TryGetValue(w, out var value)
                        ? value
                        : default(TValue);
            }
            set
            {
                if (!xdic.TryGetValue(x, out var ydic))
                {
                    xdic[x] = (ydic = new SortedDictionary<TIndex, SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>>());
                }

                if (!ydic.TryGetValue(y, out var zdic))
                {
                    ydic[y] = (zdic = new SortedDictionary<TIndex, SortedDictionary<TIndex, TValue>>());
                }

                if (!zdic.TryGetValue(z, out var wdic))
                {
                    zdic[z] = (wdic = new SortedDictionary<TIndex, TValue>());
                }

                wdic[w] = value;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var ydic in xdic.Values)
            {
                foreach (var zdic in ydic.Values)
                {
                    foreach (var wdic in zdic.Values)
                    {
                        foreach (var value in wdic.Values)
                        {
                            yield return value;
                        }
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
