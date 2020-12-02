using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return list.GetPermutations(length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();
            return Enumerable.Range(0, 1 << list.Count)
                .Select(s => Enumerable.Range(0, list.Count).Where(i => (s & (1 << i)) != 0).Select(i => list[i]));
        }

        public static IEnumerator<T> GetCircularEnumerator<T>(this IEnumerable<T> t)
        {
            return new CircularEnumerator<T>(t.GetEnumerator());
        }
    }
}
