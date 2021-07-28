using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Models
{
    public abstract class Piece : MonoBehaviour, IPointerClickHandler
    {
        private List<Tile> _validTiles = new List<Tile>();

        protected abstract List<Tile> FindValidTiles();

        [SerializeField]
        private PositionHelper _positionHelper;

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Deselect()
        {
            var board = GameLoop.Instance.Board;
            board.UnHighlight(_validTiles);

            _validTiles.Clear();
        }

        public void Select()
        {
            GameLoop.Instance.Select(this);

            _validTiles = FindValidTiles();

            var board = GameLoop.Instance.Board;
            board.UnHighlight(_validTiles);
        }

        public bool Move(Tile toTile)
        {
            var board = GameLoop.Instance.Board;

            if (!_validTiles.Contains(toTile)) return false;

            var piece = board.PieceAt(toTile);
            if (piece != null)
            {
                //piece.Capture();
                board.Take(toTile);
            }

            var fromTile = board.TileOf(this);

            board.Move(fromTile, toTile);

            transform.position = _positionHelper.ToWorldPosition(toTile.Position);

            return true;

        }
    }
}
