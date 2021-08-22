using GameSystem.Models;
using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models.MoveCommands
{
    public class MovementHelper
    {
        public delegate bool Validator(Board<Piece, Card> board, Card card, Tile toTile);

        private Board<Piece, Card> _board;

        private Card _card;
        private List<Tile> _tiles = new List<Tile>();


        public MovementHelper(Board<Piece, Card> board, Card Card)
        {
            _board = board;
            _card = Card;
        }

        public MovementHelper NorthWest(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 0, 1, steps, validators);
        }

        public MovementHelper North(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, -1, 2, steps, validators);
        }

        public MovementHelper West(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 1, 0, steps, validators);
        }

        public MovementHelper SouthWest(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, 1, -1, steps, validators);
        }

        public MovementHelper South(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, 1, -2, steps, validators);
        }

        public MovementHelper SouthEast(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, 0, -1, steps, validators);
        }

        public MovementHelper East(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, -1, 0, steps, validators);
        }
        public MovementHelper NorthEast(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, -1, 1, steps, validators);
        }


        public MovementHelper Collect(int x, int y, int z, int steps = int.MaxValue, params Validator[] validators)
        {
            Position MoveNext(Position position)
            {
                position.X += x;
                position.Z += z;
                position.Y += y;

                return position;
            }

            var playerTile = GameLoop.Instance.FindPlayerTile();

            var startPosition = playerTile.Position;

            var nextPosition = MoveNext(startPosition);

            int currentStep = 0;

            while (currentStep < steps)
            {
                var nextTile = _board.TileAt(nextPosition);
                if (nextTile == null)
                {
                    break;
                }
                var nextPiece = _board.PieceAt(nextTile);

                if ((validators.All(v => v(_board, _card, nextTile))))
                    _tiles.Add(nextTile);

                nextPosition = MoveNext(nextPosition);
                currentStep++;
            }

            return this;
        }

        public List<Tile> GenerateTiles()
        {
            return new List<Tile>(_tiles);
        }

        //public static bool CanCapture(Board<Piece, Card> board, Piece Piece, Tile toTile)
        //{
        //    var other = board.PieceAt(toTile);
        //    return other != null && other != Piece;
        //}

        //public static bool IsEmpty(Board<Piece, Card> board, Piece Piece, Tile toTile)
        //{
        //    var other = board.PieceAt(toTile);
        //    return other == null;
        //}
    }
}
