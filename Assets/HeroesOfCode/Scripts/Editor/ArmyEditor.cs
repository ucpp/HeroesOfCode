using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class ArmyEditor : ScriptableObjectEditor<Army>
    {
        private readonly string HelpText = "The size of the army can not be more than 7 squads!";
        private Editor[] _editors;

        public override void Init()
        {
            base.Init();
            _editors = new Editor[list.Count];
        }

        public override void Draw()
        {
            DrawHelpMessage();
            base.Draw();
        }
        
        private void DrawHelpMessage()
        {
            EditorGUILayout.HelpBox(HelpText, MessageType.Warning);
            EditorGUILayout.Separator();
        }

        protected override void DrawValuesBox(int index)
        {
            EditorGUILayout.BeginVertical((GUIStyle)"Box");
            Editor.CreateCachedEditor(list[index], typeof(ArmyInspector), ref _editors[index]);
            _editors[index].OnInspectorGUI();
            EditorGUILayout.EndVertical();
        }
    }
}
