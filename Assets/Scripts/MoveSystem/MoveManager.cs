using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public class MoveManager<TPiece> where TPiece : class, IPiece
    {
        private Dictionary<string, IMoveCommandProvider<TPiece>> _providers = new Dictionary<string, IMoveCommandProvider<TPiece>>();
        private Dictionary<TPiece, string> _pieceMovements = new Dictionary<TPiece, string>();

        private IMoveCommandProvider<TPiece> _activeProvider;
        private Board<TPiece> _board;

        private List<Tile> _validTiles = new List<Tile>();

        public MoveManager(Board<TPiece> board)
        {
            _board = board;
        }

        public void Register(string name, IMoveCommandProvider<TPiece> provider)
        {
            if (_providers.ContainsKey(name)) return;

            _providers.Add(name, provider);
        }

        public void Register(TPiece piece, string name)
        {
            if (_pieceMovements.ContainsKey(piece)) return;

            _pieceMovements.Add(piece, name);
        }

        public IMoveCommandProvider<TPiece> Provider(TPiece piece)
        {
            if (_pieceMovements.TryGetValue(piece, out var name))   //checks for a corresponding piece with a move
            {
                if (_providers.TryGetValue(name, out var moveCommandProvider)) return moveCommandProvider;  //checks if a name is associated with a move
            }

            return null;
        }

        public void Activate(TPiece piece)
        {
            _activeProvider = Provider(piece);
            _validTiles = _activeProvider.MoveCommands().SelectMany((command) => command.Tiles(_board, piece)).ToList();  //find the valid tiles for it
            //TODO: call something to find the validtiles for that move
        }

        public void Execute(TPiece piece, Tile tile)
        {
            if (_validTiles.Contains(tile))
            {
                var moveCommand = _activeProvider.MoveCommands().Find((command) => command.Tiles(_board, piece).Contains(tile));

                if (moveCommand != null)
                {
                    moveCommand.Execute(_board, piece, tile);

                    _activeProvider = null;
                }
            }
        }

        public void Deactivate()
        {
            _validTiles.Clear();
            _activeProvider = null;
        }

        public List<Tile> Tiles()
        {
            return _validTiles;
        }
    }
}
