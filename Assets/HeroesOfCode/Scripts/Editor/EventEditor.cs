using UnityEngine;
using UnityEditor;
namespace Maryan.HeroesOfCode
{
    [CustomEditor(typeof(GameEvent))]
    public sealed class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent gameEvent = target as GameEvent;
            if(GUILayout.Button("Raise"))
            {
                gameEvent.Raise();
            }
        }
    }
}