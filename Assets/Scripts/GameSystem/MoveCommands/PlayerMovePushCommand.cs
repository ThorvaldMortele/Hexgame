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
        public override List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile)
        {

            //if hoveredtile is northwest and in validtiles then show the other 2

            //if hoveredtiles is east and in validtiles then show the other 2

            //...

            var validTiles = new MovementHelper(board, card)
                .NorthEast(1)
                .East(1)
                .SouthEast(1)
                .SouthWest(1)
                .West(1)
                .NorthWest(1)
                .GenerateTiles();

            if (!validTiles.Contains(hoveredTile))
            {
                return validTiles;
            }
            else if (validTiles.Contains(hoveredTile))


            return validTiles;
        }

    }
}
