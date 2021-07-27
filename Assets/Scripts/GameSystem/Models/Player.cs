

using BoardSystem;
using System.Collections.Generic;

namespace GameSystem.Models
{
    public class Player : Piece
    {
        protected override List<Tile> FindValidTiles()
        {
            var validTiles = new List<Tile>();
            return validTiles;
        }
    }
}
