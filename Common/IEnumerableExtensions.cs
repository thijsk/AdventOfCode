using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Common
{
    public static partial class IEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T PrevItem, T CurrentItem, T NextItem)> SlidingWindow<T>(this IEnumerable<T> source, T emptyValue = default)
        {
            using var iter = source.GetEnumerator();
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

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> source, int length)
        {
            var list = source as IList<T> ?? source.ToList();

            if (length == 1) return list.Select(t => new[] { t });

            return list.GetPermutations(length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPowerSet<T>(this IEnumerable<T> source)
        {
            var list = source as IList<T> ?? source.ToList();
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
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return Array.Empty<TResult>();

            var ret = selector(enumerator.Current);

            while (enumerator.MoveNext())
            {
                ret = ret.Intersect(selector(enumerator.Current));
            }

            return ret;
        }

        public static double? Median<T>(this IEnumerable<T> source) where T : INumber<T>
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
                source = source.Where(x => x != null);

            var list = source as IList<T> ?? source.ToList();
            var sortedList = list.OrderBy(n => n).ToList();
            int count = list.Count;
            if (count == 0)
                return null;

            int midpoint = count / 2;
            if (count % 2 == 0)
            {
                var sum = (sortedList.ElementAt(midpoint - 1) + sortedList.ElementAt(midpoint));
                return Convert.ToDouble(sum) / (2.0);
            }

            return Convert.ToDouble(sortedList.ElementAt(midpoint));
        }

        public static IEnumerable<(IList<T> first, IList<T> second)> GetAllBinaryPartitions<T>(this IEnumerable<T> source)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            var n = enumerable.Count();

            // Generate all possible combinations of indices
            for (int i = 0; i < (1 << n); i++)
            {
                List<T> subset1 = new List<T>();
                List<T> subset2 = new List<T>();

                // Split the items into the two subsets based on the combination of indices
                int j = 0;
                foreach (T item in enumerable)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        subset1.Add(item);
                    }
                    else
                    {
                        subset2.Add(item);
                    }
                    j++;
                }

                // Yield the partition
                yield return (subset1, subset2);
            }
        }

        public static IEnumerable<(IList<T> first, IList<T> second)> FilterReverseEqualPartitions<T>(this
            IEnumerable<(IList<T> first, IList<T> second)> source)
        {
            HashSet<int> seen = new();
            foreach (var tuple in source)
            {
                if (!seen.Contains(tuple.second.GetHashCodeOfList()))
                {
                    seen.Add(tuple.first.GetHashCodeOfList());
                    yield return tuple;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCodeOfList<T>(this IList<T> source)
        { 
            HashCode hash = new();
            for (int i = 0; i < source.Count; i++)
            {
                hash.Add(source[i]);
            }
            return hash.ToHashCode();
        }
        
    }
}
