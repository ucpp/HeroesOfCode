using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public sealed class GameEditor : EditorWindow
    {
        private List<IScriptableObjectEditor<ScriptableObject>> _editors;
        private Vector2 _scrollPosition = Vector2.zero;
        private string activeEditor = string.Empty;

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
            _editors = new List<IScriptableObjectEditor<ScriptableObject>>();
            _editors.Add(new ArmyEditor());
            _editors.Add(new ScriptableObjectEditor<Unit>());

            foreach(var editor in _editors)
            {
                if(string.IsNullOrEmpty(activeEditor))
                {
                    activeEditor = editor.Name;
                }
                editor.Init();
            }
        }

        private void OnGUI()
        {
            DrawHeader();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            DrawEditors();
            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();
            foreach(var editor in _editors)
            {
                bool isSelect = activeEditor == editor.Name;
                isSelect = GUILayout.Toggle(isSelect, editor.Name, EditorStyles.miniButtonMid);
                if(isSelect)
                {
                    activeEditor = editor.Name;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawEditors()
        {
            foreach(var editor in _editors)
            {
                if(activeEditor.Equals(editor.Name))
                {
                    editor.Draw();
                }
            }
        }
    }
}
