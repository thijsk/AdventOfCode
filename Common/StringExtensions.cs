using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace Common
{
    public static class StringExtensions
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }

        public static string ReplaceAtIndex(this string str, int index, int length, string value)
        {
            var aStringBuilder = new StringBuilder(str);
            aStringBuilder.Remove(index, length);
            aStringBuilder.Insert(index, value);
            return aStringBuilder.ToString();
        }

        public static IEnumerable<int> ToInts(this string str)
        {
            return str.ToCharArray().Select(c => int.Parse(c.ToString()));
        }

        public static string Backwards(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string Sort(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Sort(charArray);
            return new string(charArray);
        }

        public static string Repeat(this string value, int times)
        {
            return string.Concat(Enumerable.Repeat(value, times));
        }

        public static string[] SplitOnNewlines(this string value)
        {
            var separator = new string[] { "\r\n", "\r", "\n" };
            return value.Split(separator, StringSplitOptions.None);
        }

        public static JsonNode AsJson(this string value)
        {
            return JsonNode.Parse(value);
        }
    }
}
