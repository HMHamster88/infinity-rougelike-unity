using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ItemPropertyChance))]
public class ItemPropertyChanceDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var chance = new PropertyField(property.FindPropertyRelative("chance"));
        var ruleProperty = property.FindPropertyRelative("rule");

        var subclassTypes = Assembly
            .GetAssembly(typeof(ItemPropertyGenerationRule))
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ItemPropertyGenerationRule)))
            .ToList();

        var names = subclassTypes.Select(type => type.Name).ToList();
        names.Add("None");

        var ruleValue = ruleProperty.GetValue();

        var defaultValue = ruleValue != null ? ruleValue.GetType().Name : "None";

        var typeDropDown = new DropdownField("Rule Type", names, defaultValue);
        typeDropDown.RegisterValueChangedCallback(evt => {
            var index = names.IndexOf(evt.newValue);
            if (index <= subclassTypes.Count)
            {
                var type = subclassTypes[index];
                ruleProperty.SetValue(type.Instantiate());
            }
        });

        var rule = new PropertyField(ruleProperty);

        container.Add(chance);

        container.Add(typeDropDown);

        container.Add(rule);

        return container;
    }
}
