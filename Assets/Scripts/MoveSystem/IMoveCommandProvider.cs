using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public interface IMoveCommandProvider<TPiece, TCard> where TPiece : class, IPiece
                                                         where TCard  : class, ICard
    {
        List<IMoveCommand<TPiece, TCard>> MoveCommands();
    }
}
