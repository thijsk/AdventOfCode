using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ConsoleApp2015
{


    public class Day4
    {
        string input = @"iwrupvqb";

        public int Part1()
        {
            var md5 = MD5.Create();

            int number = 0;
            while (number < int.MaxValue )
            {
                byte[] str = Encoding.ASCII.GetBytes(input + number);
                var hash = md5.ComputeHash(str);

                var hexhash = BitConverter.ToString(hash);

                if (hexhash.StartsWith("00-00-0"))
                    return number;

                number++;

            }
            return 0;
        }

        public int Part2()
        {
            var md5 = MD5.Create();

            int number = 0;
            while (number < int.MaxValue)
            {
                byte[] str = Encoding.ASCII.GetBytes(input + number);
                var hash = md5.ComputeHash(str);

                var hexhash = BitConverter.ToString(hash);

                if (hexhash.StartsWith("00-00-00"))
                    return number;

                number++;

            }
            return 0;
        }
    }
}
