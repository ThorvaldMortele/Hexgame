using BoardSystem;
using GameSystem.Models;
using GameSystem.Views;
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
        private int _idx = 0;

        public override void OnEnter()
        {
            GameLoop.Instance.UpdatePlayersList();
            var Players = GameLoop.Instance.Players;

            if (Players.Count == 1 && _idx == Players.Count - 1 || _idx == Players.Count)
            {
                StateMachine.MoveTo(GameStates.Play);
            }
            else
            {
                foreach (var player in Players)
                {
                    if (player.IsActive) player.IsActive = false;
                }

                _idx++;

                if (_idx >= Players.Count)
                {
                    _idx = 0;
                }

                Players[_idx].IsActive = true;

                foreach (var player in Players)
                {
                    if (player.IsActive) player.Model.IsHighlighted = true;
                    else player.Model.IsHighlighted = false;
                }

                StateMachine.MoveTo(GameStates.Play);
            }
        }
    }
}
