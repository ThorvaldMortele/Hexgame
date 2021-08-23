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

        public override List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {
            var hoveredTile = GameLoop.Instance.HoveredTile;

            var validtiles = new MovementHelper(board, card)
                .GenerateTiles();

            if (new MovementHelper(board, card).East().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).East().GenerateTiles());
            }
            else if (new MovementHelper(board, card).NorthEast().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).NorthEast().GenerateTiles());
            }
            else if (new MovementHelper(board, card).NorthWest().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).NorthWest().GenerateTiles());
            }
            else if (new MovementHelper(board, card).West().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).West().GenerateTiles());
            }
            else if (new MovementHelper(board, card).SouthEast().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).SouthEast().GenerateTiles());
            }
            else if (new MovementHelper(board, card).SouthWest().GenerateTiles().Contains(hoveredTile))
            {
                validtiles.AddRange(new MovementHelper(board, card).SouthWest().GenerateTiles());
            }
            else
            {
                validtiles = new MovementHelper(board, card)
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
