using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CustomEditor(typeof(Grid))]
    public sealed class GridEditor : Editor
    {
        private Grid Target
        {
            get { return target as Grid; }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("If you change the grid size, you will lose values of the tiles!", MessageType.Warning);
            base.OnInspectorGUI();
            if(Target.IsShowNumericMap)
            {
                for(int y = 0; y < Target.Height; y++)
                {
                    GUILayout.BeginHorizontal();
                    for(int x = 0; x < Target.Width; x++)
                    {
                        Target.Map[x, y].Value = EditorGUILayout.IntField(Target.Map[x, y].Value, GUILayout.Width(20));
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
    }
}