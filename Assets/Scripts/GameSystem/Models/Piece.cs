using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Models
{
    public class PieceMovedEventArgs : EventArgs
    {
        public Tile From { get; }
        public Tile To { get; }
        public PieceMovedEventArgs(Tile from, Tile to)
        {
            From = from;
            To = to;
        }
    }

    public class Piece : IPiece
    {
        public event EventHandler<PieceMovedEventArgs> PieceMoved;

        public bool IsPlayer { get; }

        public Piece(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        void IPiece.Moved(Tile fromTile, Tile toTile)
        {
            OnPieceMoved(new PieceMovedEventArgs(fromTile, toTile));
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs arg)
        {
            EventHandler<PieceMovedEventArgs> handler = PieceMoved;
            handler?.Invoke(this, arg);
        }
    }
}
