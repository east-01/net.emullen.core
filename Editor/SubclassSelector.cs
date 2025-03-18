using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EMullen.Core.Editor 
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        private static Dictionary<Type, Type[]> subclassCache = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (SubclassSelectorAttribute)base.attribute;
            var baseType = attribute.BaseType;

            // Retrieve subclasses from cache or compute them if not cached
            if (!subclassCache.TryGetValue(baseType, out var subclasses))
            {
                subclasses = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(assembly => assembly.GetTypes())
                                .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract)
                                .ToArray();
                subclassCache[baseType] = subclasses;
            }

            // Get the current selected subclass name
            string currentSubclass = property.stringValue;

            // Create a list of names for the dropdown
            // Sort names by standard Name, for same name variables
            List<string> subclassNames = subclasses.Select(t => t.FullName).ToList();
            subclassNames.Sort();
            string[] subclassNamesArr = subclassNames.ToArray();
            string[] showSubClassNames = (string[]) subclassNamesArr.Clone();
            for(int i = 0; i < showSubClassNames.Length-1; i++) {
                string[] curr = showSubClassNames[i].Split('.');
                string[] next = showSubClassNames[i+1].Split('.');

                List<string> builder = new();
                for(int j = curr.Length-1; j >= 0; j--) {
                    builder.Add(curr[j]);
                    if(j > next.Length-1 || curr[j] != next[j])
                        break;
                }
                showSubClassNames[i] = string.Join(".", builder);               
            }
            int currentIndex = Array.IndexOf(subclassNamesArr, currentSubclass);

            List<string> strippedNames = subclassNames.Select(fullName => fullName.Split(".")[^1]).ToList();
            for(int i = 0; i < strippedNames.Count; i++) {
                string currName = strippedNames[i];
                for(int j = 0; j < strippedNames.Count; j++) {
                    if(j == i)
                        continue;
                    
                    string targName = strippedNames[j];
                    if(currName == targName) {
                        
                    }
                }
            }

            // Draw the dropdown
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, showSubClassNames);

            // If a new subclass is selected, update the string value
            if (newIndex != currentIndex)
            {
                property.stringValue = subclassNames[newIndex];
            }
        }
    } 
}  