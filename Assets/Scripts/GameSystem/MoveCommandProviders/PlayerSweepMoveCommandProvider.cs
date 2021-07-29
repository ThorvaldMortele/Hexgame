using GameSystem.Models;
using GameSystem.Models.MoveCommands;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    public class PlayerSweepMoveCommandProvider : IMoveCommandProvider<Piece>
    {
        public static readonly string Name = "SweepMove";

        private List<IMoveCommand<Piece>> _commands = new List<IMoveCommand<Piece>>()
        {
            new PlayerMoveSweepCommand()
        };
        public List<IMoveCommand<Piece>> MoveCommands()
        {
            return _commands;
        }

    }
}
