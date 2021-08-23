using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private int _usedCardsPerTurn = 0;

        public override void OnEnter()
        {
            _usedCardsPerTurn = 0;
        }
    }
}
