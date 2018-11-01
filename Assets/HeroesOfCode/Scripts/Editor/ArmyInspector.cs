using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CustomEditor(typeof(Army))]
    public sealed class ArmyInspector : Editor
    {
        private Army Target
        {
            get { return target as Army; }
        }
        private ReorderableList _list;

        private void OnEnable()
        {
            var squads = serializedObject.FindProperty("_squads");
            _list = new ReorderableList(serializedObject, squads, true, true, true, true);
            _list.drawElementCallback += DrawElement;
            _list.drawHeaderCallback += DrawHeader;
        }

        private void OnDisable()
        {
            _list.drawElementCallback -= DrawElement;
            _list.drawHeaderCallback -= DrawHeader;
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        private void DrawHeader(Rect rect)
        {
            GUI.Label(rect, "Squads");
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUILayout.BeginHorizontal();
            var leftRect = new Rect(rect.x, (int)rect.y, rect.width / 2, rect.height - 5);
            var rightRect = new Rect(leftRect.x + rect.width / 2, leftRect.y, leftRect.width, leftRect.height);
            EditorGUI.PropertyField(leftRect, element.FindPropertyRelative("_unit"), GUIContent.none);
            EditorGUI.PropertyField(rightRect, element.FindPropertyRelative("_count"));
            EditorGUILayout.EndHorizontal();
        }
    }
}
