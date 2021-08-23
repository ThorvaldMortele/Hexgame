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
        private List<PieceView> _players = new List<PieceView>();
        private int _idx = 0;

        public FindActivePlayerState(Board<Piece, Card> board, List<PieceView> players)
        {
            _board = board;
            _players = players;
        }

        public override void OnEnter()
        {
            var Players = GameLoop.Instance.UpdatePlayersList();

            //if (_idx > Players.Count)
            //{
            //    _idx = 0;
            //}

            _players[_idx].IsActive = false;

            _idx++;

            _players[_idx].IsActive = true;

            //als iemand dood gaat verwijder hem uit de lijst

            StateMachine.MoveTo(GameStates.Play);
        }
    }
}
