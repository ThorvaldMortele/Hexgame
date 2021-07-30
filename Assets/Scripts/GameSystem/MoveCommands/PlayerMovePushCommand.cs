using BoardSystem;
using GameSystem.Models;
using GameSystem.Models.MoveCommands;
using GameSystem.Utils;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommands
{
    [MoveCommand(Name)]
    public class PlayerMovePushCommand : AbstractMoveCommand
    {
        public const string Name = "PushMove";
        public override List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {
            var validTiles = new MovementHelper(board, card)
                .NorthEast(1)
                .East(1)
                .SouthEast(1)
                .GenerateTiles();

            return validTiles;
        }

    }
}
