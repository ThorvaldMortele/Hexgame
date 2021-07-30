using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Material _highlightMaterial = null;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        public Vector3 worldPosition;

        private Material _originalMaterial;

        private MeshRenderer _meshRenderer;

        private Tile _model;

        public Tile Model
        {
            get => _model;  //if we are getting the value, return the _model value
            set             //if we are setting the value...
            {
                if (_model != null)
                {
                    _model.HighlightStatusChanged -= ModelHighlightStatusChanged;   //if the model exists, remove the subscription, so that the previous model 
                }                                                                   //no longer gets updates from this view

                _model = value;

                if (_model != null)                                                 //now assign the model value
                {
                    _model.HighlightStatusChanged += ModelHighlightStatusChanged;
                }
            }
        }

        private void Start()
        {
            var board = GameLoop.Instance.Board;
            var boardPosition = _positionHelper.ToBoardPosition(transform.position);
            var tile = board.TileAt(boardPosition);

            Model = tile;

            worldPosition = transform.position;

            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;

            //GameLoop.Instance.FindCardMovements();
        }

        private void ModelHighlightStatusChanged(object sender, EventArgs e)
        {
            if (Model.IsHighlighted) _meshRenderer.material = _highlightMaterial;

            else _meshRenderer.material = _originalMaterial;
        }

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

        private void OnDestroy()
        {
            Model = null;   //this makes it so we dont call a method on a view that has been destroyed already 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (GameLoop.Instance.SelectedCard != null) //only works in the beginning
            {
                var board = GameLoop.Instance.Board;

                var _hoveredTile = GameLoop.Instance.GetHoveredTile(board, _positionHelper, this.transform);    //get the new one

                //if its not part of the validtiles list
                board.HighlightOne(_hoveredTile);   //highlight the new one
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            var board = GameLoop.Instance.Board;
            var _hoveredTile = GameLoop.Instance.GetHoveredTile(board, _positionHelper, this.transform);

            GameLoop.Instance.SelectActivate(_hoveredTile);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var board = GameLoop.Instance.Board;
            var _hoveredTile = GameLoop.Instance.GetHoveredTile(board, _positionHelper, this.transform);

            board.UnHighlightOne(_hoveredTile);     //unhighlight the previous tile
        }
    }
}
