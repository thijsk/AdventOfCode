using System;
using System.Linq;
using System.Text;
using Common;

namespace ConsoleApp2017
{
    class Day10 : IDay
    {
        private string input = @"189,1,111,246,254,2,0,120,215,93,255,50,84,15,94,62";
        public long Part1()
        {

            int position = 0;
            int skipsize = 0;

          //  input = @"3,4,1,5";
            var chainlength = 256;
            var chain = GenerateChain(chainlength);

            var lengths = input.Split(',').Select(int.Parse);
            foreach (var length in lengths)
            {
                var replacement = chain.ToArray();
                for (int p = position, i =0; p < position + length; p++, i++)
                {
                    var safeop = (p + length - 1 - (2*i)) % chainlength;
                    var safep = p % chainlength;
                    replacement[safep]= chain[safeop];
                    replacement[safeop] = chain[safep];
                }

                chain = replacement.ToArray();

                position += length + skipsize;
                skipsize++;
            }

            return chain[0] * chain[1];
        }

        private int[] GenerateChain(int length)
        {
            var result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = i;
            }
            return result;
        }

        public long Part2()
        {

            int position = 0;
            int skipsize = 0;

            //  input = @"3,4,1,5";
            var chainlength = 256;
            var chain = GenerateChain(chainlength);

            var additionalinput = "17,31,73,47,23".Split(',').Select(int.Parse);

            var lengths = Encoding.ASCII.GetBytes(input).Select(b => (int) b).ToList();
            lengths.AddRange(additionalinput);

            for (var round = 1; round <= 64; round++)
            {
               // Console.WriteLine(round);
                foreach (var length in lengths)
                {
                    var replacement = chain.ToArray();
                    for (int p = position, i = 0; p < position + length; p++, i++)
                    {
                        var safeop = (p + length - 1 - (2 * i)) % chainlength;
                        var safep = p % chainlength;
                        replacement[safep] = chain[safeop];
                        replacement[safeop] = chain[safep];
                    }

                    chain = replacement.ToArray();

                    position += length + skipsize;
                    skipsize++;
                }
            }

            var sparsehash = chain.ToArray();

            
            var xorhash = new byte[16];

            for (int xornum = 0; xornum < 16; xornum++)
            {
                byte xor = 0;
                for (int xorpos = 0; xorpos < 16; xorpos++)
                {
                    int pos = (xornum * 16) + xorpos;
                    xor ^= (byte) sparsehash[pos];
                }
                xorhash[xornum] = xor;
            }


            string result = ToHexHash(xorhash);
            Console.WriteLine(result);
            return 0;
        }

        private static string ToHexHash(byte[] ba)
        {
            return string.Concat(ba.Select(b => b.ToString("X2")).ToArray()).ToLower();
        }
    }
}
