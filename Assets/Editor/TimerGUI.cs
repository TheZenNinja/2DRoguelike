using UnityEngine;
using UnityEditor;

namespace ZenClasses
{
    [CanEditMultipleObjects]
    [CustomPropertyDrawer(typeof(Timer))]
    public class TimerGUI : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int oldIndentLevel = EditorGUI.indentLevel;

            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            contentPosition.width /= 2;

            EditorGUI.indentLevel = 0;
            EditorGUI.LabelField(contentPosition, "Timer Length:");
            contentPosition.x += contentPosition.width;
            contentPosition.x += 7;
            EditorGUIUtility.labelWidth = 14f;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("timerLength"), GUIContent.none);

            EditorGUI.EndProperty();

            EditorGUI.indentLevel = oldIndentLevel;
        }
    }
}