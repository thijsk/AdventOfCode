using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<(T PrevItem, T CurrentItem, T NextItem)>  SlidingWindow<T>(this IEnumerable<T> source, T emptyValue = default)
        {
            using (var iter = source.GetEnumerator())
            {
                if (!iter.MoveNext())
                    yield break;
                var prevItem = emptyValue;
                var currentItem = iter.Current;
                while (iter.MoveNext())
                {
                    var nextItem = iter.Current;
                    yield return (prevItem, currentItem, nextItem);
                    prevItem = currentItem;
                    currentItem = nextItem;
                }
                yield return (prevItem, currentItem, emptyValue);
            }
        }

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

        public static IEnumerable<T> AsCircular<T>(this IEnumerable<T> t)
        {
            return new CircularEnumerable<T>(t);
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> t, int times)
        {
            return new RepeatEnumerable<T>(t, times);
        }

        public static IEnumerable<TResult> IntersectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return new TResult[0];

                var ret = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    ret = ret.Intersect(selector(enumerator.Current));
                }

                return ret;
            }
        }

        public static double? Median<TColl, TValue>(
            this IEnumerable<TColl> source,
            Func<TColl, TValue> selector)
        {
            return source.Select<TColl, TValue>(selector).Median();
        }

        public static double? Median<T>(
            this IEnumerable<T> source)
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
                source = source.Where(x => x != null);

            int count = source.Count();
            if (count == 0)
                return null;

            source = source.OrderBy(n => n);

            int midpoint = count / 2;
            if (count % 2 == 0)
                return (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0;
            else
                return Convert.ToDouble(source.ElementAt(midpoint));
        }
    }
}
