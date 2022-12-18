using System.Linq;
using System.Numerics;

namespace Common
{
    public static class Math2
    {
        public static long GreatestCommonDivider(long a, long b)
        {
            // Euclidean algorithm
            long t;
            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return (a * b / GreatestCommonDivider(a, b));
        }

        public static long LeastCommonMultiple(params long[] args)
        {
            // Recursively iterate through pairs of arguments
            // i.e. lcm(args[0], lcm(args[1], lcm(args[2], args[3])))

            if (args.Length == 2)
            {
                return LeastCommonMultiple(args[0], args[1]);
            }

            if (args.Length > 2)
            {
                return LeastCommonMultiple(args[0], LeastCommonMultiple(args.Skip(1).ToArray()));
            }

            return args[0];
        }

        public static T Abs<T>(T value) where T : INumber<T>
        {
            if (value < T.Zero)
            {
                value = -value;
            }
            return value;
        }
    }
}
