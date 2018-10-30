using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class GameEditor : EditorWindow
    {
        private List<Army> _armies = new List<Army>();
        private Vector2 _scrollPosition = Vector2.zero;
        private readonly string HelpText = "Max army size is: 7 squads!";

        [MenuItem(EditorUtils.AssetsMenu + nameof(GameEditor))]
        private static void CreateWindow()
        {
            var window = CreateInstance<GameEditor>();
            string title = nameof(GameEditor);
            window.titleContent = new GUIContent(title);
            window.ShowUtility();
        }

        private void OnFocus()
        {
            _armies.Clear();
            _armies = Resources.FindObjectsOfTypeAll<Army>().ToList();
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUILayout.HelpBox(HelpText, MessageType.Warning);
            EditorGUILayout.Separator();
            for(int i = 0; i < _armies.Count; i++)
            {
                var origFontStyle = EditorStyles.label.fontStyle;
                EditorStyles.foldout.fontStyle = FontStyle.Bold;
                EditorGUILayout.BeginVertical((GUIStyle)"HelpBox");
                _armies[i].IsShowInEditor = EditorGUILayout.Foldout(_armies[i].IsShowInEditor, _armies[i].name);
                EditorGUILayout.EndVertical();
                EditorStyles.foldout.fontStyle = origFontStyle;
                EditorGUI.indentLevel++;
                if(_armies[i].IsShowInEditor)
                {
                    EditorGUILayout.BeginVertical((GUIStyle)"Box");
                    var editor = Editor.CreateEditor(_armies[i]);
                    editor.OnInspectorGUI();
                    EditorGUILayout.EndVertical();
                }
               
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
