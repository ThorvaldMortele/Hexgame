using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Board<TPiece> where TPiece : class, IPiece
    {
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();   //a board has a list of tiles, in this case hexagons
                                                                                        //dictionary is used so you dont have to cycle over all tiles
                                                                                        //now u can get the tile based on the pos (the key)

        private List<TPiece> _values = new List<TPiece>();  //the pieces and tiles are split into keys and values because a dictionary can only go in one way
        private List<Tile> _keys = new List<Tile>();        //and we need it to work in 2 ways

        public readonly int Radius;

        public List<Tile> Tiles => _tiles.Values.ToList();

        public Board(int radius)
        {
            Radius = radius;

            InitTiles();
        }

        public Tile TileAt(Position position)   //a way of getting a tile based on the position provided in the parameter
        {
            if (_tiles.TryGetValue(position, out Tile tile)) //returns a value if theres one else it a null value in the parameter, is most performant
                return tile;

            return null;
        }

        public TPiece PieceAt(Tile tile)  //find the piece at a specified tile
        {
            var idx = _keys.IndexOf(tile);  //get the index of the tile in the keys list

            if (idx == -1) return default(TPiece);   //als de idx -1 is betekent het dat er geen tile in de lijst zat

            return _values[idx];       //then get the corresponding piece with the same index
        }

        public Tile TileOf(TPiece piece) //find the tile that the specified piece is on
        {
            var idx = _values.IndexOf(piece);   //get the index of the piece in its list

            if (idx == -1) return null;         //if theres no piece, return null

            return _keys[idx];                  //return the tile with the corresponding piece idx

        }

        public TPiece Take(Tile fromTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1) return default(TPiece); //if idx == -1 that means there was nothing on that spot (no tile)

            var piece = _values[idx];

            _values.RemoveAt(idx);  //remove the piece from the piecelist
            _keys.RemoveAt(idx);    //remove the tile from the tilelist

            return piece;
        }

        public void Move(Tile fromTile, Tile toTile)
        {
            var idx = _keys.IndexOf(fromTile);  //get the fromtile index in the list
            if (idx == -1) return;

            var toPiece = PieceAt(toTile);
            if (toPiece != null) return;    //if theres already something on toTile dont go there since u cant

            _keys[idx] = toTile;

            var piece = _values[idx];
            piece.Moved(fromTile, toTile);
        }

        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile)) return;

            if (_values.Contains(piece)) return;

            _keys.Add(toTile);
            _values.Add(piece);
        }

        private void InitTiles()
        {
            for (int q = -Radius; q <= Radius; q++)
            {
                int r1 = Math.Max(-Radius, -q - Radius);
                int r2 = Math.Min(Radius, -q + Radius);

                for (int r = r1; r <= r2; r++)
                {
                    var toBeAddedPos = new Position { X = q, Y = r, Z = -q - r };
                    var toBeAddedTile = new Tile(new Position(q, r, -q - r));

                    _tiles.Add(toBeAddedPos, toBeAddedTile);
                }
            }
        }

        public void Highlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = true;
            }
        }

        public void UnHighlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = false;
            }
        }
    }
}
