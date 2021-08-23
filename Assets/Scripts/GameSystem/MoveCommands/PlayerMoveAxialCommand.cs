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
    public class PlayerMoveAxialCommand : AbstractMoveCommand
    {
        public const string Name = MoveNames.Axial;

        public override List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile)
        {
            var validtiles = new MovementHelper(board)
                .GenerateTiles();

            if (new MovementHelper(board).East().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).East().GenerateTiles());
            }
            else if (new MovementHelper(board).NorthEast().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).NorthEast().GenerateTiles());
            }
            else if (new MovementHelper(board).NorthWest().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).NorthWest().GenerateTiles());
            }
            else if (new MovementHelper(board).West().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).West().GenerateTiles());
            }
            else if (new MovementHelper(board).SouthEast().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).SouthEast().GenerateTiles());
            }
            else if (new MovementHelper(board).SouthWest().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board).SouthWest().GenerateTiles());
            }
            else
            {
                validtiles = new MovementHelper(board)
                    .East()
                    .NorthEast()
                    .NorthWest()
                    .West()
                    .SouthWest()
                    .SouthEast()
                    .GenerateTiles();
            }

            card.MoveTiles = validtiles;
            return validtiles;
        }
    }
}
