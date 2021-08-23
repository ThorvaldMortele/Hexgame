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
        private Board<Piece, Card> _board;
        private int _idx = 0;

        public FindActivePlayerState(Board<Piece, Card> board)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            GameLoop.Instance.UpdatePlayersList();
            var Players = GameLoop.Instance.Players;

            //if (_idx > Players.Count-1)
            //{
            //    _idx = 0;
            //}

            Players[_idx].IsActive = false;

            _idx++;

            if (_idx > Players.Count)
            {
                _idx = 0;
            }

            Players[_idx].IsActive = true;

            foreach (var player in Players)
            {
                if (player.IsActive) player.Model.IsHighlighted = true;
                else player.Model.IsHighlighted = false;
            }

            //als iemand dood gaat verwijder hem uit de lijst

            StateMachine.MoveTo(GameStates.Play);
        }
    }
}
