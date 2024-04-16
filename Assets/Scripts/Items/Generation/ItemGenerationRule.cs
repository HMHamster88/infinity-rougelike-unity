using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ItemGenerationRule", menuName = "Scriptable Objects/Item Generation Rule")]
public class ItemGenerationRule : ScriptableObject
{
    [SerializeField]
    private LocalizedString itemName;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private ItemSlot.Type slotType;
    [SerializeField]
    private List<ItemPropertyChance> itemProperties = new List<ItemPropertyChance>();

    public LocalizedString ItemName { get => itemName; }
    public Sprite Sprite { get => sprite; }
    public ItemSlot.Type SlotType { get => slotType; }

    public GameObject Generate(int level)
    {
        var itemGameObject = new GameObject();
        itemGameObject.name = this.name;
        var itemComponent = itemGameObject.AddComponent<Item>();
        itemComponent.generationRule = this;
        itemComponent.Name = itemName;
        itemComponent.SlotType = slotType;
        itemComponent.Sprite = sprite;
        itemProperties.ForEach(itemProperty => itemProperty.GenerateProperty(itemGameObject, level));
        return itemGameObject;
    }
}
