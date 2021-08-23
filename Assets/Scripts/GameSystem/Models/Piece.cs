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
        public event EventHandler EnemyTaken;

        public bool IsPlayer { get; }
        public bool IsActive { get; }

        public Piece(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        void IPiece.Moved(Tile fromTile, Tile toTile)
        {
            OnPieceMoved(new PieceMovedEventArgs(fromTile, toTile));
        }

        void IPiece.Taken()
        {
            OnEnemyTaken(EventArgs.Empty);
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs arg)
        {
            EventHandler<PieceMovedEventArgs> handler = PieceMoved;
            handler?.Invoke(this, arg);
        }

        protected virtual void OnEnemyTaken(EventArgs arg)
        {
            EventHandler handler = EnemyTaken;
            handler?.Invoke(this, arg);
        }
    }
}
