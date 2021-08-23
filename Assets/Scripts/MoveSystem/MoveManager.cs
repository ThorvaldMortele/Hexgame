using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoveSystem
{
    public class MoveManager<TPiece, TCard> where TPiece : class, IPiece
                                            where TCard  : class, ICard
    {
        private Dictionary<string, IMoveCommand<TPiece, TCard>> _commands = new Dictionary<string, IMoveCommand<TPiece, TCard>>();
        private Dictionary<TCard, string> _playerMovements = new Dictionary<TCard, string>();

        private IMoveCommand<TPiece, TCard> _activeCommand;
        private Board<TPiece, TCard> _board;

        private List<Tile> _validTiles = new List<Tile>();

        public MoveManager(Board<TPiece, TCard> board)
        {
            _board = board;
        }

        public void Register(string name, IMoveCommand<TPiece, TCard> command)
        {
            if (_commands.ContainsKey(name)) return;

            _commands.Add(name, command);
        }

        public void Register(TCard card, string name)
        {
            if (_playerMovements.ContainsKey(card)) return;

            _playerMovements.Add(card, name);
        }

        public IMoveCommand<TPiece, TCard> Provider(TCard card)
        {
            if (_playerMovements.TryGetValue(card, out var name))   //checks for a corresponding piece with a move
            {
                if (_commands.TryGetValue(name, out var moveCommand)) return moveCommand;  //checks if a name is associated with a move
            }
            return null;
        }

        public void Activate(TCard card, Tile hoveredTile)
        {
            _activeCommand = Provider(card);
            _validTiles = _activeCommand.Tiles(_board, card, hoveredTile);
        }

        public void Execute(TCard card, Tile tile)
        {
            if (_validTiles.Contains(tile))
            {
                var moveCommand = _activeCommand;

                if (moveCommand != null)
                {
                    moveCommand.Execute(_board, card, tile);

                    _activeCommand = null;
                }
            }
        }

        public void Deactivate()
        {
            _validTiles.Clear();
            _activeCommand = null;
        }

        public List<Tile> Tiles()
        {
            return _validTiles;
        }
    }
}
