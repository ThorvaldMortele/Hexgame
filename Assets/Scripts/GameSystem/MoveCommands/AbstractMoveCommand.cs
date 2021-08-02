using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractMoveCommand : IMoveCommand<Piece, Card>
    {
        public void Execute(Board<Piece, Card> board, Card card, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var playerTile = GameLoop.Instance.FindPlayerTile();

            board.Move(playerTile, toTile);
        }

        public abstract List<Tile> Tiles(Board<Piece, Card> board, Card card);
    }
}
