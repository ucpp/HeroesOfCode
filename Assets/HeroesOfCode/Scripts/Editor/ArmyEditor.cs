using UnityEditor;

namespace Maryan.HeroesOfCode
{
    public class ArmyEditor : ScriptableObjectEditor<Army>
    {
        private readonly string HelpText = "The size of the army can not be more than 7 squads!";

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
    }
}
