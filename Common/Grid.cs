using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Grid<T> : Dictionary<Point<int>, T>
    {
        public T this[int x, int y]
        {
            get
            {
                this.TryGetValue((x, y), out T value);
                return value;
            }
            set
            {
                this.AddOrSet((x, y), value);
            }
        }
    }
}
