using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ItemDropChance))]
public class ItemDropChanceDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var chance = new PropertyField(property.FindPropertyRelative("chance"));
        var rule = new PropertyField(property.FindPropertyRelative("rule"));

        container.Add(chance);
        container.Add(rule);

        return container;
    }
}
