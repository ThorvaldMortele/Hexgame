using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "defaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private Vector3 _tileSize = Vector3.one;

        public Vector3 TileSize => _tileSize;

        public Position ToBoardPosition(Vector3 worldPosition)
        {
            var q = (Mathf.Sqrt(3) / 3f * worldPosition.x - 1f / 3f * worldPosition.z);
            var r = (2f / 3f * worldPosition.z);

            var (x, y, z) = CubeRound(q, -q - r, r);

            var axialPosition = new Position((int)x, (int)z);

            return axialPosition;
        }

        public Vector3 ToWorldPosition(Position boardPosition)
        {
            var tilePosition = new Vector3
            {
                x = (Mathf.Sqrt(3) * boardPosition.Q + Mathf.Sqrt(3) / 2f * boardPosition.R),
                z = (3f / 2f * boardPosition.R)
            };

            return tilePosition;
        }

        public static (float x, float y, float z) CubeRound(float x, float y, float z)
        {
            var rx = Mathf.Round(x);
            var ry = Mathf.Round(y);
            var rz = Mathf.Round(z);

            var x_diff = Mathf.Abs(rx - x);
            var y_diff = Mathf.Abs(ry - y);
            var z_diff = Mathf.Abs(rz - z);

            if (x_diff > y_diff && x_diff > z_diff)
                rx = -ry - rz;
            else if (y_diff > z_diff)
                ry = -rx - rz;
            else
                rz = -rx - ry;

            return (rx, ry, rz);
        }
    }
}
