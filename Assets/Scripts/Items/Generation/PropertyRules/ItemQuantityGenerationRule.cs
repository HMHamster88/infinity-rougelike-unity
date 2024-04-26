using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ItemQuantityGenerationRule : ItemPropertyGenerationRule
{
    [SerializeField]
    private IntLevelValueRange quantity = new();
    [SerializeField]
    private string sameItemIdentifier;
    public override ItemProperty GenerateProperty(int level)
    {
        var property = ScriptableObject.CreateInstance<ItemQuantity>();
        property.Quantity = quantity.GetRandomInclusive(level);
        property.SameItemIdentifier = sameItemIdentifier;
        return property;
    }
}
