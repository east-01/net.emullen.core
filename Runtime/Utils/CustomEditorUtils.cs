using UnityEngine;

namespace EMullen.Core 
{
    public class CustomEditorUtils 
    {
        public static void CreateBigHeader(string text) 
        {
            GUILayout.Label($"<b><color=white>{text}</color></b>", BigHeaderStyle);
        }

        public static void CreateHeader(string text) 
        {
            GUILayout.Label($"<b><color=white>{text}</color></b>", HeaderStyle);
        }

        public static void CreateNote(string text) 
        {
            GUILayout.Label($"<i><color=#a7abb0>{text}</color></i>", NoteStyle, new GUILayoutOption[] {GUILayout.Width(400f)});
        }

        public static GUIStyle BigHeaderStyle { get {
            return new() {
                richText = true,
                margin = new RectOffset(3, 10, 0, 10),
                fontSize = 15
            };
        } }

        public static GUIStyle HeaderStyle { get {
            return new() {
                richText = true,
                margin = new RectOffset(3, 0, 0, 0)
            };
        } }

        public static GUIStyle NoteStyle { get {
            return new() {
                richText = true,
                margin = new RectOffset(5, 0, 0, 0),
                wordWrap = true
            };
        } }
    }
}