using BoardSystem;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models.Moves
{
    public class PlayerMoveSweep : IMoveCommand<Piece>
    {
        public List<Tile> Tiles(Board<Piece> board, Piece piece)
        {
            var validtiles = new List<Tile>();
            //validtiles.Add(new Tile(new Position { X = 0, Y = 2, Z = -2 }));

            return validtiles;
        }

        public void Move(Board<Piece> board, Piece piece,Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var fromTile = board.TileOf(piece);

            board.Move(fromTile, toTile);
        }

        public void Execute(Board<Piece> board, Piece piece, Tile toTile)
        {
            throw new NotImplementedException();
        }
    }

}
