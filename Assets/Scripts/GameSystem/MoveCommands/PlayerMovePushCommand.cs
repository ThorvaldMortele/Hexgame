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
        public const string Name = MoveNames.Push;
        public override List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {

            var hoveredTile = GameLoop.Instance.HoveredTile;

            var validtiles = new MovementHelper(board, card)
                .GenerateTiles();

            if (new MovementHelper(board, card).East(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .East(1)
                    .NorthEast(1)
                    .SouthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board, card).NorthEast(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .East(1)
                    .NorthWest(1)
                    .NorthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board, card).NorthWest(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .West(1)
                    .NorthWest(1)
                    .NorthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board, card).West(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .West(1)
                    .NorthWest(1)
                    .SouthWest(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board, card).SouthEast(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .East(1)
                    .SouthWest(1)
                    .SouthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board, card).SouthWest(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card)
                    .West(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .GenerateTiles());
            }
            else
            {
                validtiles = new MovementHelper(board, card)
                    .East(1)
                    .NorthEast(1)
                    .NorthWest(1)
                    .West(1)
                    .SouthWest(1)
                    .SouthEast(1)
                    .GenerateTiles();
            }

            card.MoveTiles = validtiles;
            return validtiles;
        }

    }
}
