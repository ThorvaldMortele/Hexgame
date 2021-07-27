using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/Tileview Factory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField]
        private TileView _tileView = null;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        public TileView CreateTileview(Tile tile, Transform parent)
        {
            var position = _positionHelper.ToWorldPosition(tile.Position);

            var tileView = GameObject.Instantiate(_tileView, position, Quaternion.identity, parent);

            tileView.Size = _positionHelper.TileSize;
            tileView.name = $"Tile{tile.Position.X} {tile.Position.Y} {tile.Position.Z}";

            //tileView.Model = tile;

            return tileView;
        }
    }
}
