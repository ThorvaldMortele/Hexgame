using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Tile   //is made public so its accessible outside the boardsystem assembly
    {
        public Position Position { get; }

        public Tile (int x, int y)
        {
            Position = new Position { X = x, Y = y};
        }
    }
}
