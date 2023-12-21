using System.Numerics;
using System.Runtime.CompilerServices;

namespace Common;

public static class TupleExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T1, T2) Add<T1, T2>(this (T1 a, T2 b) first, (T1 a, T2 b) second)
        where T1 : INumber<T1> where T2 : INumber<T2>
    {
        return (first.a + second.a, first.b + second.b);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T, T) Add<T>(this (T a, T b) first, T second)
        where T : INumber<T>
    {
        return (first.a + second, first.b + second);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T1, T2) Multiply<T1, T2>(this (T1 a, T2 b) first, (T1 a, T2 b) second)
        where T1 : INumber<T1> where T2 : INumber<T2>
    {
        return (first.a * second.a, first.b * second.b);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T, T) CrossMultiply<T>(this (T a, T b) first, (T a, T b) second)
        where T : INumber<T>
    {
        return (first.a * second.b, first.b * second.a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T, T) Multiply<T>(this (T a, T b) first, T second)
        where T : INumber<T>
    {
        return (first.a * second, first.b * second);
    }
}