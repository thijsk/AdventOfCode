using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class IDictionaryExtensions
    {
        public static void AddOrIncrement<TKey>(this Dictionary<TKey, long> dictionary, TKey key, long value) where TKey : notnull
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
            if (!dictionary.TryAdd(key, value))
            {
                dictionary[key] = value;
            }
        }
    }
}
