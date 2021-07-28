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

        IMoveCommandProvider<TPiece> Provider(TPiece piece)
        {
            if (_pieceMovements.TryGetValue(piece, out var name))
            {
                if (_providers.TryGetValue(name, out var moveCommandProvider)) return moveCommandProvider;
            }

            return null;
        }
    }
}
