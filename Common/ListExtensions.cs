using System;
using System.Collections.Generic;

namespace Common;

public static class ListExtensions
{
    private static readonly Random _random = new();

    public static T Random<T>(this List<T> list)
    {
        return list[_random.Next(list.Count)];
    }
}