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
        //public T this[Point<int> p]
        //{
        //    get => this[p.x, p.y];
        //    set => this[p.x, p.y] = value;
        //}


        public T this[int x, int y]
        {
            get => this[(x, y)];
            set => this[(x,y)] = value;
        }
    }
}
