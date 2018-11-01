using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public sealed class ArmyEditor : ScriptableObjectEditor<Army>
    {
        private readonly string HelpText = "The size of the army can not be more than {0} squads!";
        private Editor[] _editors;
        private string _text;

        public override void Init()
        {
            base.Init();
            InitializeWarningMessage();
            _editors = new Editor[list.Count];
        }

        private void InitializeWarningMessage()
        {
            var list = Resources.FindObjectsOfTypeAll<ArenaStartPoints>().ToList();
            if(list.Count > 0)
            {
                int minSize = list[0].Count;
                foreach(var asset in list)
                {
                    if(asset.Count < minSize)
                    {
                        minSize = asset.Count;
                    }
                }
                _text = string.Format(HelpText, minSize);
            }
        }

        public override void Draw()
        {
            DrawHelpMessage();
            base.Draw();
        }

        private void DrawHelpMessage()
        {
            EditorGUILayout.HelpBox(_text, MessageType.Warning);
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
