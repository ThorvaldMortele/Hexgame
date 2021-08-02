using BoardSystem;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models
{
    public class CardDroppedEventArgs : EventArgs
    {
        public Tile DroppedTile { get; }

        public CardDroppedEventArgs(Tile droppedTile)
        {
            DroppedTile = droppedTile;
        }
    }

    public class Card : ICard
    {
        public event EventHandler<CardDroppedEventArgs> CardDropped;

        public Card()
        {
        }

        void ICard.CanDrop(Tile droppedTile)
        {
            OnCardDropped(new CardDroppedEventArgs(droppedTile));
        }

        private void OnCardDropped(CardDroppedEventArgs cardDroppedEventArgs)
        {
            EventHandler<CardDroppedEventArgs> handler = CardDropped;
            handler?.Invoke(this, cardDroppedEventArgs);
        }
    }
}
