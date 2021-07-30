using GameSystem.Models;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour
    {
        public Piece Model { get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.PieceMoved -= ModelMoved;
                    _model.EnemyTaken -= Modeltaken;
                }
                    
                _model = value;

                if (_model != null)
                {
                    _model.PieceMoved += ModelMoved;
                    _model.EnemyTaken += Modeltaken;
                }  
            }
        }

        private void Modeltaken(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void ModelMoved(object sender, PieceMovedEventArgs e)
        {
            var worldPos = _positionHelper.ToWorldPosition(e.To.Position);
            transform.position = new Vector3(worldPos.x, 1.25f, worldPos.z);
        }

        private Piece _model;

        [SerializeField]
        private bool _isPlayer;

        public bool IsPlayer => _isPlayer;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        private void OnDestroy()
        {
            Model = null;
        }
    }
    
}
