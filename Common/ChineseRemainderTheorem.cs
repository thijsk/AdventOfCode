using System.Linq;
using System.Numerics;

namespace Common
{
    public static class ChineseRemainderTheorem
    {
        public static BigInteger Solve(int[] n, int[] a)
        {
            BigInteger prod = n.Aggregate(new BigInteger(1), (i, j) => i * j);
            BigInteger p;
            BigInteger sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static BigInteger ModularMultiplicativeInverse(BigInteger a, BigInteger mod)
        {
            BigInteger b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }

}
