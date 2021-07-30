using GameSystem.Models;
using GameSystem.Models.MoveCommands;
using GameSystem.MoveCommands;
using GameSystem.Utils;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerSweepMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "SweepMove";

        public PlayerSweepMoveCommandProvider() : base(new PlayerMoveSweepCommand())
        {
        }
    }
}
