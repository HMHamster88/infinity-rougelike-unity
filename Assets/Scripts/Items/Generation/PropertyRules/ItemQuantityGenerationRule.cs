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
    public override void GenerateProperty(GameObject item, int level)
    {
        var property = item.AddComponent<ItemQuantity>();
        property.Quantity = quantity.GetRandomInclusive(level);
        property.SameItemIdentifier = sameItemIdentifier;
    }
}
