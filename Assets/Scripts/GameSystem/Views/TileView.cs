using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class TileView : MonoBehaviour
    {

        private Tile _model;

        //[SerializeField]
        //private Material _highlightMaterial = null;

        //[SerializeField]
        //private PositionHelper _positionHelper = null;

        //public Vector3 worldPosition;

        //private Material _originalMaterial;

        //private MeshRenderer _meshRenderer;

        //public Tile Model
        //{
        //    get => _model;
        //    set
        //    {
        //        if (_model != null)
        //        {
        //            _model.HighlightStatusChanged -= ModelHighlightStatusChanged;
        //        }

        //        _model = value;

        //        if (_model != null)
        //        {
        //            _model.HighlightStatusChanged += ModelHighlightStatusChanged;
        //        }
        //    }
        //}

        //private void Start()
        //{
        //    var board = GameLoop.Instance.Board;
        //    var boardPosition = _positionHelper.ToBoardPosition(transform.position);
        //    var tile = board.TileAt(boardPosition);

        //    Model = tile;

        //    worldPosition = transform.position;

        //    _meshRenderer = GetComponentInChildren<MeshRenderer>();
        //    _originalMaterial = _meshRenderer.sharedMaterial;

        //    //GameLoop.Instance.FindCardMovements();
        //}

        //private void ModelHighlightStatusChanged(object sender, System.EventArgs e)
        //{
        //    if (Model.IsHighlighted)
        //    {
        //        _meshRenderer.material = _highlightMaterial;
        //    }
        //    else
        //    {
        //        _meshRenderer.material = _originalMaterial;
        //    }
        //}

        internal Vector3 Size
        {
            set
            {
                transform.localScale = Vector3.one;

                (float w, float h) tuple = PointyDimension(1f);
                float width = tuple.w;
                float height = tuple.h;

                Vector3 size = GetComponentInChildren<MeshRenderer>().bounds.size;
                float xSize = width / size.x;

                float z1Size = size.z;
                float z2 = (height / z1Size);

                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
        }

        public static (float w, float h) PointyDimension(float size) => (Mathf.Sqrt(3f) * size, 2f * size);

        //private void OnDestroy()
        //{
        //    Model = null;
        //}
    }
}
