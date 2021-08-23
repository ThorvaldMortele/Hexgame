using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.States
{
    public class FindActivePlayerState : GameStateBase
    {
        public override void OnEnter()
        {
            Debug.Log("findactiveplayer");

            StateMachine.MoveTo(GameStates.Play);
        }
    }
}
