#if false
using System;
using UnityEditor;
using UnityEngine;

namespace UnityConversion.Editor
{
    [CustomPropertyDrawer(typeof(FixedEnvironmentGroupFlagAttribute), true)]
    public class FixedEnvironmentGroupFlagDrawer: PropertyDrawer
    {
        private static Type _enumType = null;
        
        public static void SetGroupFlagEnum(Type enumType)
        {
            _enumType = enumType;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_enumType == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            var index = property.intValue;
            var value = (Enum)Enum.ToObject(_enumType, index);
            
            EditorGUI.BeginChangeCheck();
            var enumValue = EditorGUI.EnumPopup(position, label, value);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = Convert.ToInt32(enumValue);
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
#endif