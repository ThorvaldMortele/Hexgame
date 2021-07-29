using GameSystem.Utils;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace GameSystem.Editors
{
    [CustomEditor(typeof(CardView))]
    public class CardViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            var dragOnSurfacesSp = serializedObject.FindProperty("dragOnSurfaces");
            EditorGUILayout.PropertyField(dragOnSurfacesSp);

            var _movementNameSp = serializedObject.FindProperty("_movementName");
            var movementName = _movementNameSp.stringValue;

            var typeNames = MoveCommandProviderTypeHelper.FindMoveCommandProviderTypes();
            var selectedIdx = Array.IndexOf(typeNames, movementName);

            var newSelectedIdx = EditorGUILayout.Popup("Movement", selectedIdx, typeNames);

            if (selectedIdx != newSelectedIdx)
            {
                _movementNameSp.stringValue = typeNames[newSelectedIdx];
            }

            serializedObject.ApplyModifiedProperties(); // gaat de veranderingen in de properties updaten in de inspector, !MOET ALTIJD ER STAAN!
        }
    }
}
