using GameSystem.Models;
using MoveSystem;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.MoveCommandProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<Piece, Card>
    {
        public AbstractMoveCommandProvider(params IMoveCommand<Piece, Card>[] commands)
        {
            _commands = commands.ToList();
        }

        private List<IMoveCommand<Piece, Card>> _commands;

        public List<IMoveCommand<Piece, Card>> MoveCommands()
        {
            return _commands;
        }
    }
}
