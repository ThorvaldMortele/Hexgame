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

        //private void MovePlayer(Board<Piece, Card> board, Tile toTile)
        //{
        //    var playerTile = GameLoop.Instance.FindPlayerTile();

        //    board.Move(playerTile, toTile);
        //}

        //private void TakeEnemies(Board<Piece, Card> board, Card card)
        //{
        //    foreach (Tile tile in card.MoveTiles)
        //    {
        //        if (board.PieceAt(tile) != null)
        //        {
        //            board.Take(tile);
        //        }
        //    }
        //}

        //private void PushEnemies(Board<Piece, Card> board, Card card)
        //{
        //    var playerTile = GameLoop.Instance.FindPlayerTile();
        //    foreach (var tile in card.MoveTiles)
        //    {
        //        var toPiece = board.PieceAt(tile);
        //        if (toPiece != null)
        //        {
        //            if (!toPiece.IsPlayer)
        //            {
        //                Position moveDirection = new Position
        //                    (
        //                    tile.Position.X - playerTile.Position.X,
        //                    tile.Position.Y - playerTile.Position.Y,
        //                    tile.Position.Z - playerTile.Position.Z
        //                    );
        //                Position NewTile = new Position
        //                    (
        //                    tile.Position.X + moveDirection.X,
        //                    tile.Position.Y + moveDirection.Y,
        //                    tile.Position.Z + moveDirection.Z
        //                    );

        //                foreach (var tileCheck in board.Tiles)
        //                {
        //                    if (tileCheck.Position.X == NewTile.X && tileCheck.Position.Y == NewTile.Y && tileCheck.Position.Z == NewTile.Z)
        //                    {
        //                        board.Move(tile, tileCheck);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public abstract List<Tile> Tiles(Board<Piece, Card> board, Card card, Tile hoveredTile, Tile fromTile);
    }
}
