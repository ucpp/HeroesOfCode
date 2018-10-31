using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public static class ExtendedEditorGUILayout
    {
        public static bool BoldFoldout(bool value, string text)
        {
            var origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.foldout.fontStyle = FontStyle.Bold;
            value = EditorGUILayout.Foldout(value, text);
            EditorStyles.foldout.fontStyle = origFontStyle;
            return value;
        }
    }
}
