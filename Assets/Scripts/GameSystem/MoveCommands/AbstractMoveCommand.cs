using BoardSystem;
using GameSystem.Models;
using GameSystem.Utils;
using MoveSystem;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractMoveCommand : IMoveCommand<Piece, Card>
    {
        public virtual void Execute(Board<Piece, Card> board, Card card, Tile toTile)
        {
            var playerTile = GameLoop.Instance.FindPlayerTile();

            board.Move(playerTile, toTile);
        }

        public abstract List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile, Tile fromTile);
    }
}
