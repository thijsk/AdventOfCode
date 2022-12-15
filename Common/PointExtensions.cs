using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class PointExtensions
    {
        public static int ManhattanDistanceTo(this Point<int> point, Point<int> other)
        {
            return Math.Abs(point.x - other.x) + Math.Abs(point.y - other.y);
        }
    }
}
