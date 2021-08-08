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
    public class PlayerMoveTeleportCommand : AbstractMoveCommand
    {
        public const string Name = "TeleportMove";

        public override List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {
            List<Tile> NeighbourStrategy(Tile centerTile) => Neighbours(centerTile, board);

            float DistanceStrategy(Tile fromTile, Tile toTile) => Distance(fromTile, toTile);

            var bfs = new BreadthFirstAreaSearch<Tile>(NeighbourStrategy, DistanceStrategy);

            var validtiles = new MovementHelper(board, card)
                .GenerateTiles();

            validtiles = bfs.Area(GameLoop.Instance.FindPlayerTile(), 9f);

            foreach (Tile validtile in validtiles.ToList())  //this gets rid of all the tiles in which there's a piece
            {
                if (board.PieceAt(validtile) != null)   //we cant get rid of them in the neighbours method since it will not check certain tiles if we do that
                {
                    validtiles.Remove(validtile);
                }
            }

            card.MoveTiles = validtiles;
            return validtiles;
        }

        private List<Tile> Neighbours(Tile tile, Board<Piece, Card> board)
        {
            var neighbours = new List<Tile>();
            var position = tile.Position;

            var upRightPosition = position;
            upRightPosition.Z += 1;
            upRightPosition.X = 0;
            upRightPosition.Y = -upRightPosition.X - upRightPosition.Z;
            var upRightTile = board.TileAt(upRightPosition);
            if (upRightTile != null)
            {
                neighbours.Add(upRightTile);
            }

            var downRightPosition = position;
            downRightPosition.Z -= 1;
            downRightPosition.X += 1;
            downRightPosition.Y = -downRightPosition.X - downRightPosition.Z;
            var downRightTile = board.TileAt(downRightPosition);
            if (downRightTile != null)
            {
                neighbours.Add(downRightTile);
            }

            var rightPosition = position;
            rightPosition.X += 1;
            rightPosition.Z = 0;
            rightPosition.Y = -rightPosition.X - rightPosition.Z;
            var rightTile = board.TileAt(rightPosition);
            if (rightTile != null)
            {
                neighbours.Add(rightTile);
            }

            var upLeftPosition = position;
            upLeftPosition.Z += 1;
            upLeftPosition.X -= 1;
            upLeftPosition.Y = -upLeftPosition.X - upLeftPosition.Z;
            var upLeftTile = board.TileAt(upLeftPosition);
            if (upLeftTile != null)
            {
                neighbours.Add(upLeftTile);
            }

            var leftPosition = position;
            leftPosition.X -= 1;
            leftPosition.Z = 0;
            leftPosition.Y = -leftPosition.X - leftPosition.Z;
            var leftTile = board.TileAt(leftPosition);
            if (leftTile != null)
            {
                neighbours.Add(leftTile);
            }

            var downLeftPosition = position;
            downLeftPosition.Z -= 1;
            downLeftPosition.X = 0;
            downLeftPosition.Y = -downLeftPosition.X - downLeftPosition.Z;
            var downLeftTile = board.TileAt(downLeftPosition);
            if (downLeftTile != null)
            {
                neighbours.Add(downLeftTile);
            }

            return neighbours;
        }

        private float Distance(Tile fromTile, Tile toTile)
        {
            return (Math.Abs(fromTile.Position.X - toTile.Position.X) +
                 Math.Abs(fromTile.Position.Y - toTile.Position.Y) +
                 Math.Abs(fromTile.Position.Z - toTile.Position.Z)) / 2;
        }
    }
}
