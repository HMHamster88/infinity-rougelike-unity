using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class Item : INamed
{
    [JsonIgnore]
    public LocalizedString Name;
    [JsonIgnore]
    public ItemSlot.Type SlotType = ItemSlot.Type.Bag;
    [JsonIgnore]
    public Sprite Sprite;
    [JsonConverter(typeof(ItemGenerationRuleConverter))]
    public ItemGenerationRule generationRule;
    public List<ItemProperty> ItemProperties = new();

    [JsonIgnore]
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

    [JsonIgnore]
    string INamed.Name => Name.GetLocalizedString();

    public T GetProperty<T>() where T : ItemProperty
    {
        return (T) ItemProperties.FirstOrDefault(prop => prop is T);
    }

    public IEnumerable<T> GetProperties<T>() where T : ItemProperty
    {
        return ItemProperties.Where(prop => prop is T).Cast<T>();
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        Name = generationRule.ItemName;
        Sprite = generationRule.Sprite;
        SlotType = generationRule.SlotType;
        foreach (var prop in ItemProperties)
        {
            prop.GenerationRule = generationRule.GetItemPropertyGenerationRule(prop.GenerationRule.ID);
            prop.SetDataFromRule();
        }
    }
}
