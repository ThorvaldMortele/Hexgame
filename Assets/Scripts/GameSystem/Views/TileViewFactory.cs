using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/Tileview Factory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField]
        private TileView _tileView = null;

        [SerializeField]
        private PositionHelper _positionHelper;

        public TileView CreateTileview(/*Board board, */Tile tile, Transform parent)
        {
            var position = _positionHelper.ToWorldPosition(/*board, */tile.Position);

            var tileView = GameObject.Instantiate(_tileView, position, Quaternion.identity, parent);

            tileView.Size = _positionHelper.TileSize;
            tileView.name = $"Tile{tile.Position.X} {tile.Position.Y} {tile.Position.Z}";


            return tileView;
        }
    }
}
