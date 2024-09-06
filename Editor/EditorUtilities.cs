using System.Text.RegularExpressions;
using UnityEngine;

namespace EMullen.Core 
{
    public class EditorUtilities 
    {
        public static string FormatEnum(string enumString)
        {
            string formattedString = enumString.Replace("_", " ");
            formattedString = Regex.Replace(formattedString, @"\b(\w)", m => m.Value.ToUpper());
            return formattedString;
        }    

        public static void CreateHeader(string text) 
        {
            GUILayout.Label($"<b><color=white>{text}</color></b>", HeaderStyle);
        }

        public static void CreateNote(string text) 
        {
            GUILayout.Label($"<i><color=#a7abb0>{text}</color></i>", NoteStyle);
        }

        public static GUIStyle HeaderStyle { get {
            return new() {
                richText = true,
                margin = new RectOffset(3, 0, 0, 0)
            };
        } }

        public static GUIStyle NoteStyle { get {
            return new() {
                richText = true,
                margin = new RectOffset(5, 0, 0, 0)
            };
        } }
    }
}