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
    public class PlayerMoveSweepCommand : AbstractMoveCommand
    {
        public const string Name = MoveNames.Sweep;
        public override List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile)
        {

            var validtiles = new MovementHelper(board)
                .GenerateTiles();

            if (new MovementHelper(board).East(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .East(1)
                    .NorthEast(1)
                    .SouthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board).NorthEast(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .East(1)
                    .NorthWest(1)
                    .NorthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board).NorthWest(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .West(1)
                    .NorthWest(1)
                    .NorthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board).West(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .West(1)
                    .NorthWest(1)
                    .SouthWest(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board).SouthEast(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .East(1)
                    .SouthWest(1)
                    .SouthEast(1)
                    .GenerateTiles());
            }
            else if (new MovementHelper(board).SouthWest(1).GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board)
                    .West(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .GenerateTiles());
            }
            else
            {
                validtiles = new MovementHelper(board)
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
