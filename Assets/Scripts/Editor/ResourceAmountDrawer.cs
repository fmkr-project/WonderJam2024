using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ResourceAmount))]
    public class ResourceAmountDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw the label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // Calculate rects
            Rect resourceRect = new Rect(position.x, position.y, position.width / 2, position.height);
            Rect quantityRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(resourceRect, property.FindPropertyRelative("Resource"), GUIContent.none);
            EditorGUI.PropertyField(quantityRect, property.FindPropertyRelative("Quantity"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}