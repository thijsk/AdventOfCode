using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class LineExtensions
    {
        public static bool IsHorizontal<T>(this Line<T> line)
        {
            return line.start.x.Equals(line.end.x);
        }

        public static bool IsVertical<T>(this Line<T> line)
        {
            return line.start.y.Equals(line.end.y);
        }
    }
}
