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
            GameLoop.Instance.HoveredTile = this.Model; //assign the hoveredtile

            var board = GameLoop.Instance.Board;    //get the board

            var oldValidTiles = GameLoop.Instance.MoveManager.Tiles(); //assign the validtiles in this variable

            board.UnHighlightAll(oldValidTiles);   //unhighlight them all

            var selectedcard = GameLoop.Instance.SelectedCard;  //get the selected card and load the valid tiles

            if (selectedcard != null)
            {
                GameLoop.Instance.MoveManager.Activate(selectedcard);

                var newValidTiles = GameLoop.Instance.MoveManager.Tiles(); //assign the validtiles in this variable

                if (GameLoop.Instance.SelectedCard != null && newValidTiles.Contains(GameLoop.Instance.HoveredTile)) //if the hoveredtile is part of the validtiles list
                {
                    if (!GameLoop.Instance.SelectedCard.MoveName.Equals("TeleportMove"))
                    {
                        board.HighlightAll(newValidTiles);   //highlight the new one
                    }
                    else
                    {
                        board.HighlightOne(GameLoop.Instance.HoveredTile);
                    }
                }

            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameLoop.Instance.SelectActivate(GameLoop.Instance.HoveredTile);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var board = GameLoop.Instance.Board;

            board.UnHighlightOne(GameLoop.Instance.HoveredTile);     //unhighlight the previous tile
        }
    }
}
