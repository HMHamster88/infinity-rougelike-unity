using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class Item : INamed
{
    public LocalizedString Name;
    public ItemSlot.Type SlotType = ItemSlot.Type.Bag;
    public Sprite Sprite;
    public ItemGenerationRule generationRule;
    public List<ItemProperty> ItemProperties = new();

    [CreateProperty]
    public int? Quantity
    {
        get
        {
            var quantityProperty = GetProperty<ItemQuantity>();
            if (quantityProperty == null)
            {
                return null;
            }
            return quantityProperty.Quantity;
        }
    }

    string INamed.Name => Name.GetLocalizedString();

    public T GetProperty<T>() where T : ItemProperty
    {
        return (T) ItemProperties.FirstOrDefault(prop => prop is T);
    }

    public IEnumerable<T> GetProperties<T>() where T : ItemProperty
    {
        return ItemProperties.Where(prop => prop is T).Cast<T>();
    }
}
