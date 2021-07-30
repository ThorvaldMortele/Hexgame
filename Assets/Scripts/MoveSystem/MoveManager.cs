using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public class MoveManager<TPiece, TCard> where TPiece : class, IPiece
                                            where TCard  : class, ICard
    {
        private Dictionary<string, IMoveCommand<TPiece, TCard>> _providers = new Dictionary<string, IMoveCommand<TPiece, TCard>>();
        private Dictionary<TCard, string> _pieceMovements = new Dictionary<TCard, string>();

        private IMoveCommand<TPiece, TCard> _activeProvider;
        private Board<TPiece, TCard> _board;

        private List<Tile> _validTiles = new List<Tile>();

        public MoveManager(Board<TPiece, TCard> board)
        {
            _board = board;
        }

        public void Register(string name, IMoveCommand<TPiece, TCard> command)
        {
            if (_providers.ContainsKey(name)) return;

            _providers.Add(name, command);
        }

        public void Register(TCard card, string name)
        {
            if (_pieceMovements.ContainsKey(card)) return;

            _pieceMovements.Add(card, name);
        }

        public IMoveCommand<TPiece, TCard> Provider(TCard card)
        {
            if (_pieceMovements.TryGetValue(card, out var name))   //checks for a corresponding piece with a move
            {
                if (_providers.TryGetValue(name, out var moveCommand)) return moveCommand;  //checks if a name is associated with a move
            }
            return null;
        }

        public void Activate(TCard card)
        {
            _activeProvider = Provider(card);
            _validTiles = _activeProvider.Tiles(_board, card);
        }

        public void Execute(TCard card, Tile tile)
        {
            if (_validTiles.Contains(tile))
            {
                var moveCommand = _activeProvider;

                if (moveCommand != null)
                {
                    moveCommand.Execute(_board, card, tile);

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
