using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece> where TPiece : class, IPiece
    {
        List<Tile> Tiles(Board<TPiece> board, TPiece piece);

        void Execute(Board<TPiece> board, TPiece piece, Tile toTile);
    }
}
