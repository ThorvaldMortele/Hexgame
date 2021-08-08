using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public interface ICard
    {
        void CanDrop(Tile droppedTile);

    }
}
