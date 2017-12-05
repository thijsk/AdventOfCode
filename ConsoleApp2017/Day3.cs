using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApp
{
    public class Day3
    {
        private int input = 289326;
        //private int input = 1024;

        internal struct pos
        {
            internal int horizontal;
            internal int vertical;

            internal pos(int hor, int ver)
            {
                horizontal = hor;
                vertical = ver;
            }
        }

        public int Part1()
        {
            int left = 0;
            int right = 0;
            int top = 0;
            int bottom = 0;
            int horizontal = 0;
            int vertical = 0;
            int direction = 1;

            for (int i = 2; i <= input; i++)
            {
                switch (direction)
                {
                    case 1:
                        horizontal += 1;
                        if (horizontal > right)
                        {
                            right = horizontal;
                            direction = 2;
                        }
                        break;
                    case 2:
                        vertical += 1;
                        if (vertical > top)
                        {
                            top = vertical;
                            direction = 3;
                        }
                        break;
                    case 3:
                        horizontal -= 1;
                        if (horizontal < left)
                        {
                            left = horizontal;
                            direction = 4;
                        }
                        break;
                    case 4:
                        vertical -= 1;
                        if (vertical < bottom)
                        {
                            bottom = vertical;
                            direction = 1;
                        }
                        break;
                }

            }

            return Math.Abs(horizontal) + Math.Abs(vertical);



        }

        public int Part2()
        {
            int left = 0;
            int right = 0;
            int top = 0;
            int bottom = 0;
            int horizontal = 0;
            int vertical = 0;
            int direction = 1;

            Dictionary<pos, int> matrix = new Dictionary<pos, int>();

            matrix.Add(new pos(0,0), 1);

            for (int i = 1; i <= int.MaxValue-1; i++)
            {
                switch (direction)
                {
                    case 1:
                        horizontal += 1;
                        if (horizontal > right)
                        {
                            right = horizontal;
                            direction = 2;
                        }
                        break;
                    case 2:
                        vertical += 1;
                        if (vertical > top)
                        {
                            top = vertical;
                            direction = 3;
                        }
                        break;
                    case 3:
                        horizontal -= 1;
                        if (horizontal < left)
                        {
                            left = horizontal;
                            direction = 4;
                        }
                        break;
                    case 4:
                        vertical -= 1;
                        if (vertical < bottom)
                        {
                            bottom = vertical;
                            direction = 1;
                        }
                        break;
                }
                int n, ne, e, se, s, sw, w, nw;
                matrix.TryGetValue(new pos(horizontal + 0, vertical + 1), out n);
                matrix.TryGetValue(new pos(horizontal + 1, vertical + 1), out ne);
                matrix.TryGetValue(new pos(horizontal + 1, vertical + 0), out e);
                matrix.TryGetValue(new pos(horizontal - 1, vertical + 1), out se);
                matrix.TryGetValue(new pos(horizontal + 0, vertical - 1), out s);
                matrix.TryGetValue(new pos(horizontal - 1, vertical - 1), out sw);
                matrix.TryGetValue(new pos(horizontal - 1, vertical - 0), out w);
                matrix.TryGetValue(new pos(horizontal + 1, vertical - 1), out nw);
                var mySum = n + ne + e + se + s + sw + w + nw;

                Console.WriteLine($"{horizontal}, {vertical} : {mySum}");
                if (mySum > input)
                    return mySum;

                matrix.Add(new pos(horizontal, vertical), mySum);
            }
            return 0;
        }
    }
}
