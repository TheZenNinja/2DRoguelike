using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZenUtil
{
    [CustomPropertyDrawer(typeof(TimedInput))]
    [CanEditMultipleObjects]
    public class TimedInputDrawer : PropertyDrawer
    {
        //https://docs.unity3d.com/Manual/editor-PropertyDrawers.html
        //https://catlikecoding.com/unity/tutorials/editor/custom-data/

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int oldIndentLevel = EditorGUI.indentLevel;

            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            contentPosition.width /= 2;

            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("key"), GUIContent.none);
            contentPosition.x += contentPosition.width;
            contentPosition.x += 7;
            EditorGUIUtility.labelWidth = 14f;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("timeWindow"), new GUIContent("#"));

            EditorGUI.EndProperty();

            EditorGUI.indentLevel = oldIndentLevel;
        }
    }
}
