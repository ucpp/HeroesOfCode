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

        protected List<T> list = new List<T>();
        private List<bool> _isExpand = new List<bool>();

        public virtual void Init()
        {
            list.Clear();
            list = Resources.FindObjectsOfTypeAll<T>().ToList();
            _isExpand = Enumerable.Repeat(false, list.Count).ToList();
        }

        public virtual void Draw()
        {
            EditorGUILayout.Separator();
            for(int i = 0; i < list.Count; i++)
            {
                EditorGUILayout.BeginVertical((GUIStyle)"HelpBox");
                _isExpand[i] = ExtendedEditorGUILayout.BoldFoldout(_isExpand[i], list[i].name);
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel++;
                if(_isExpand[i])
                {
                    EditorGUILayout.BeginVertical((GUIStyle)"Box");
                    DrawValuesBox(i);
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel--;
            }
        }

        protected virtual void DrawValuesBox(int index)
        {
            var editor = Editor.CreateEditor(list[index]);
            editor.OnInspectorGUI();
        }
    }
}
