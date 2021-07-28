using GameSystem.Models;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPointerClickHandler
    {
        public Piece Model { get => _model;
            internal set
            {
                if (_model != null) _model.PieceMoved -= ModelMoved;

                _model = value;

                if (_model != null) _model.PieceMoved += ModelMoved;
            }
        }

        private void ModelMoved(object sender, PieceMovedEventArgs e)
        {
            var worldPos = _positionHelper.ToWorldPosition(e.To.Position);
            transform.position = worldPos;
        }

        private Piece _model;

        [SerializeField]
        private bool _isPlayer;

        [SerializeField]
        private string _movementName = null;

        public bool IsPlayer => _isPlayer;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            var board = GameLoop.Instance;
            board.Select(Model);
            Debug.Log("hit");
        }

        private void OnDestroy()
        {
            Model = null;
        }
    }
    
}
