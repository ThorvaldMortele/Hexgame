using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameSystem.Utils.BoardExtension;
using UnityEngine;
using GameSystem.Utils;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "defaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private Vector3 _tileSize = Vector3.one;

        public Vector3 TileSize => _tileSize;

        public Position ToBoardPosition(Board board, Vector3 worldPosition)
        {
            var boardSize = Vector3.Scale(board.AsVector3(), TileSize);

            var boardOffset = (TileSize - boardSize) / 2;
            boardOffset.y = -TileSize.y / 2;

            var offset = worldPosition - boardOffset;
            var boardPosition = new Position { X = (int)(offset.x / TileSize.x), Y = (int)(offset.z / TileSize.z) };

            return boardPosition;
        }

        public Vector3 ToWorldPosition(Board board, Position boardPosition)
        {
            var boardSize = Vector3.Scale(board.AsVector3(), TileSize);

            var boardOffset = (TileSize - boardSize) / 2;
            boardOffset.y = -TileSize.y / 2;

            var tilePos = boardOffset + Vector3.Scale(boardPosition.AsVector3(), TileSize);

            return tilePos;
        }
    }
}
