using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public struct Position
    {
        //public int X; //it makes a copy and doesnt change that actual value, therefore its useless to make it a property
        //public int Y;

        public int X;
        public int Y;
        public int Z;

        public int Q => X;
        public int R => Z;

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position(int q, int r)
        {
            X = q;
            Z = r;
            Y = -X - Z;
        }
    }
}
