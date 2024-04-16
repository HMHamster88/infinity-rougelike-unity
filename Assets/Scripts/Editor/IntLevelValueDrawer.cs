using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(IntLevelValue))]
public class IngredientDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.LabelField(new Rect(position.x, position.y, 30, position.height), "Base");
        EditorGUI.PropertyField(new Rect(position.x + 30, position.y, 30, position.height), property.FindPropertyRelative("baseValue"), GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x + 60, position.y, 40, position.height), "Linear");
        EditorGUI.PropertyField(new Rect(position.x + 100, position.y, 30, position.height), property.FindPropertyRelative("linearFactor"), GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x + 130, position.y, 40, position.height), "Cubic");
        EditorGUI.PropertyField(new Rect(position.x + 170, position.y, 30, position.height), property.FindPropertyRelative("cubicFactor"), GUIContent.none);

        var value = (IntLevelValue)property.GetValue();

        EditorGUI.LabelField(new Rect(position.x + 200, position.y, position.width - 200, position.height), $"1: {value.GetValue(1)}, 10: {value.GetValue(10)}");

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}