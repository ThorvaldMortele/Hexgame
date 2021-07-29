using BoardSystem;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models.MoveCommands
{
    public class PlayerMoveSweepCommand : IMoveCommand<Piece>
    {
        public List<Tile> Tiles(Board<Piece> board, Piece piece)
        {
            var validtiles = new List<Tile>();

            return validtiles;
        }

        public void Execute(Board<Piece> board, Piece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var fromTile = board.TileOf(piece);

            board.Move(fromTile, toTile);
        }
    }

}
