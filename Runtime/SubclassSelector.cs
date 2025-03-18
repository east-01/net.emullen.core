using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EMullen.Core 
{
    // This class is just a marker for the custom property drawer
    public class SubclassSelectorAttribute : PropertyAttribute
    {
        public System.Type BaseType { get; }

        public SubclassSelectorAttribute(System.Type baseType)
        {
            BaseType = baseType;
        }
    }
}  