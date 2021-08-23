using GameSystem.Models;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour
    {
        [SerializeField]
        private Material _highlightMaterial = null;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        private Material _originalMaterial;

        private MeshRenderer _meshRenderer;

        private Piece _model;

        public bool IsActive;


        public Piece Model { get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.HighlightStatusChanged -= ModelHighlightStatusChanged;
                    _model.PieceMoved -= ModelMoved;
                    _model.EnemyTaken -= Modeltaken;
                }
                    
                _model = value;

                if (_model != null)
                {
                    _model.HighlightStatusChanged += ModelHighlightStatusChanged;
                    _model.PieceMoved += ModelMoved;
                    _model.EnemyTaken += Modeltaken;
                }  
            }
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;
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

        private void ModelHighlightStatusChanged(object sender, EventArgs e)
        {
            if (_meshRenderer != null && _originalMaterial != null)
            {
                if (Model.IsHighlighted) _meshRenderer.material = _highlightMaterial;

                else _meshRenderer.material = _originalMaterial;
            }
        }

        private void OnDestroy()
        {
            Model = null;
        }
    }
    
}
