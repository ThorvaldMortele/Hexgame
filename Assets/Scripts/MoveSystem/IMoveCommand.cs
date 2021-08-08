using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece, TCard> where TPiece : class, IPiece
                                                 where TCard  : class, ICard
    {
        List<Tile> Tiles(Board<TPiece, TCard> board, TCard card);

        void Execute(Board<TPiece, TCard> board, TCard card, Tile toTile);

    }
}
