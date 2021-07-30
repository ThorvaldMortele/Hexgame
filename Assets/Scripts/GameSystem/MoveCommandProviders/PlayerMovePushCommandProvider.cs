using GameSystem.MoveCommands;
using GameSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerMovePushCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "PushMove";

        public PlayerMovePushCommandProvider() : base(new PlayerMovePushCommand())
        {
        }
    }
}
