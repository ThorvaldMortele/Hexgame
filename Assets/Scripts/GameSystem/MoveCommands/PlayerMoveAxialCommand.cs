using BoardSystem;
using GameSystem.Models;
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
    public class PlayerMoveAxialCommand : AbstractMoveCommand
    {
        public const string Name = "AxialMove";

        public override List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile)
        {
            var validtiles = new List<Tile>();

            return validtiles;
        }
    }
}
