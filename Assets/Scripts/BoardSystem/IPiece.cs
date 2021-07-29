using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public interface IPiece
    {
        void Moved(Tile fromTile,Tile toTile);
        void Taken();
    }
}
