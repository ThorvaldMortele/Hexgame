using GameSystem.MoveCommands;
using GameSystem.Utils;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerMoveAxialCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "AxialMove";

        public PlayerMoveAxialCommandProvider() : base(new PlayerMoveAxialCommand())
        {
        }
    }
}
