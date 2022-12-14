using System.Collections.Generic;

namespace Common
{
    public readonly struct Line<T>
    {
        public readonly Point<T> start;
        public readonly Point<T> end;

        public Line (Point<T> start, Point<T> end)
        {
            this.start = start;
            this.end = end;
        }
       
        public override string ToString()
        {
            return $"{start}  -> {end}";
        }
    }
}
