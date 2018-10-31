using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class ScriptableObjectEditor<T> : IScriptableObjectEditor<T> where T : ScriptableObject
    {
        public string Name
        {
            get { return typeof(T).Name; }
        }

        private List<T> _list = new List<T>();
        private List<bool> _isExpand = new List<bool>();

        public void Init()
        {
            _list.Clear();
            _list = Resources.FindObjectsOfTypeAll<T>().ToList();
            _isExpand = Enumerable.Repeat(false, _list.Count).ToList();
        }

        public virtual void Draw()
        {
            EditorGUILayout.Separator();
            for(int i = 0; i < _list.Count; i++)
            {
                EditorGUILayout.BeginVertical((GUIStyle)"HelpBox");
                _isExpand[i] = ExtendedEditorGUILayout.BoldFoldout(_isExpand[i], _list[i].name);
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel++;
                if(_isExpand[i])
                {
                    EditorGUILayout.BeginVertical((GUIStyle)"Box");
                    var editor = Editor.CreateEditor(_list[i]);
                    editor.OnInspectorGUI();
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}
