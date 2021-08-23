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
        public const string Name = MoveNames.Teleport;

        public override List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {
            List<Tile> NeighbourStrategy(Tile centerTile) => Neighbours(centerTile, board);

            float DistanceStrategy(Tile fromTile, Tile toTile) => Distance(fromTile, toTile);

            var bfs = new BreadthFirstAreaSearch<Tile>(NeighbourStrategy, DistanceStrategy);

            var validtiles = new MovementHelper(board, card)
                .GenerateTiles();

            validtiles = bfs.Area(GameLoop.Instance.FindPlayerTile(), 9f);

            card.MoveTiles = validtiles;
            return validtiles;
        }

        private List<Tile> Neighbours(Tile tile, Board<Piece, Card> board)
        {
            var neighbours = new List<Tile>();

            var position = tile.Position;

            var northEast = position;
            northEast.Y -= 1;
            northEast.Z += 1;
            var northEastTile = board.TileAt(northEast);
            if (northEastTile != null && board.PieceAt(northEastTile) == null)
                neighbours.Add(northEastTile);

            var northWest = position;
            northWest.X -= 1;
            northWest.Z += 1;
            var northWestTile = board.TileAt(northWest);
            if (northWestTile != null && board.PieceAt(northWestTile) == null)
                neighbours.Add(northWestTile);

            var east = position;
            east.X += 1;
            east.Y -= 1;
            var eastTile = board.TileAt(east);
            if (eastTile != null && board.PieceAt(eastTile) == null)
                neighbours.Add(eastTile);

            var west = position;
            west.X -= 1;
            west.Y += 1;
            var westTile = board.TileAt(west);
            if (westTile != null && board.PieceAt(westTile) == null)
                neighbours.Add(westTile);

            var southEast = position;
            southEast.X += 1;
            southEast.Z -= 1;
            var southEastTile = board.TileAt(southEast);
            if (southEastTile != null && board.PieceAt(southEastTile) == null)
                neighbours.Add(southEastTile);

            var southWest = position;
            southWest.Y += 1;
            southWest.Z -= 1;
            var southWestTile = board.TileAt(southWest);
            if (southWestTile != null && board.PieceAt(southWestTile) == null)
                neighbours.Add(southWestTile);


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
