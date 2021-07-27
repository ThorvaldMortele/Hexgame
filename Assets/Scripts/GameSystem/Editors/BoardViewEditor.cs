using GameSystem.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BoardSystem.Editor
{
    [CustomEditor(typeof(BoardView))]
    public class BoardViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Board"))
            {
                var boardview = target as BoardView;

                var tileviewFactorySp = serializedObject.FindProperty("_tileviewFactory");
                var tileviewFactory = tileviewFactorySp.objectReferenceValue as TileViewFactory;

                var game = GameLoop.Instance;
                var board = game.Board;

                foreach (var tile in board.Tiles)
                {
                    tileviewFactory.CreateTileview(tile, boardview.transform);
                }
            }
        }
    }
}
