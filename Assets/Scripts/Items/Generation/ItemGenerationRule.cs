using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ItemGenerationRule", menuName = "Scriptable Objects/Item Generation Rule")]
public class ItemGenerationRule : ScriptableObject
{
    [SerializeField]
    public string ID = Guid.NewGuid().ToString();
    [SerializeField]
    private LocalizedString itemName;
    [SerializeField]
    [JsonIgnore]
    private Sprite sprite;
    [SerializeField]
    private ItemSlot.Type slotType;
    [SerializeField]
    private List<ItemPropertyChance> itemProperties = new List<ItemPropertyChance>();

    public LocalizedString ItemName { get => itemName; }
    public Sprite Sprite { get => sprite; }
    public ItemSlot.Type SlotType { get => slotType; }

    public Item Generate(int level)
    {
        return new Item
        {
            generationRule = this,
            Name = itemName,
            SlotType = slotType,
            Sprite = sprite,
            ItemProperties = itemProperties.Select(prop => prop.Rule.GenerateProperty(level)).ToList()
        };
    }

    public ItemPropertyGenerationRule GetItemPropertyGenerationRule(string id)
    {
        return itemProperties.Select(chance => chance.Rule).FirstOrDefault(rule => rule.ID == id);
    }
}
