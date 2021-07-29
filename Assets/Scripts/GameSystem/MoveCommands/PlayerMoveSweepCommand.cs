using BoardSystem;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models.MoveCommands
{
    public class PlayerMoveSweepCommand : IMoveCommand<Piece, Card>
    {
        public List<Tile> Tiles(Board<Piece, Card> board, Card card)
        {
            var validtiles = new List<Tile>();

            return validtiles;
        }

        public void Execute(Board<Piece, Card> board, Card card, Tile toTile)   //pretty much take and move or just move
        {
            var toPiece = board.PieceAt(toTile);
            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var playerTile = GameLoop.Instance.FindPlayerTile();

            board.Move(playerTile, toTile);
        }
    }
}
