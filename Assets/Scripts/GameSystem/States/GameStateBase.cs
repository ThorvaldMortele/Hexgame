using BoardSystem;
using GameSystem.Models;
using GameSystem.Views;
using StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.States
{
    public class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { set; get; }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void LoadCards() { }

        public virtual void OnEnterTile(Tile hoveredTile) { }
        public virtual void OnDropTile() { }
        public virtual void OnCardBeginDrag(Card modelCard) { }
        public virtual void OnEndCardDrag(List<TileView> tiles, GameObject cardObj) { }
            
    }
}
