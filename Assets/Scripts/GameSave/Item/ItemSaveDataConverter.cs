using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


public class ItemSaveDataConverter
{
    public ItemsBagSaveData ToItemsBagSaveData(ItemsBag itemsBag)
    {
        return new ItemsBagSaveData
        {
            ItemsSlots = itemsBag.itemsSlots.Select(ToItemSlotSaveData).ToList()
        };
    }

    public ItemSlotSaveData ToItemSlotSaveData(ItemSlot itemSlot)
    {
        return new ItemSlotSaveData
        {
            Type = itemSlot.type,
            Item = ToItemSaveData(itemSlot.Item)
        };
    }

    public ItemSaveData ToItemSaveData(Item item)
    {
        if (item == null)
        {
            return null;
        }
        return new ItemSaveData
        {
            GenerationRulePath = AssetDatabase.GetAssetPath(item.generationRule).Replace("Assets/Resources/", "").Replace(".asset", ""),
            ItemProperties = item.GetComponents<ItemProperty>().Select(ToItemPropertySaveData).ToList()
        };
    }

    ItemPropertySaveData ToItemPropertySaveData(ItemProperty itemProperty)
    {
        return new ItemPropertySaveData()
        {
            Type = itemProperty.GetType().ToString()
        };
    }

    public Item ToItem(ItemSaveData itemSaveData)
    {
        if (itemSaveData == null)
        {
            return null;
        }
        var itemGeneratiobRule = Resources.Load<ItemGenerationRule>(itemSaveData.GenerationRulePath);
        var itemGameObject = new GameObject();
        var item = itemGameObject.AddComponent<Item>();
        item.generationRule = itemGeneratiobRule;
        item.Name = itemGeneratiobRule.ItemName;
        item.Sprite = itemGeneratiobRule.Sprite;
        return item;
    }

    public ItemSlot ToItemSlot(ItemSlotSaveData itemSlotSaveData)
    {
        if (itemSlotSaveData == null)
        {
            return null;
        }
        var itemSlot = ScriptableObject.CreateInstance<ItemSlot>();
        itemSlot.type = itemSlotSaveData.Type;
        itemSlot.Item = ToItem(itemSlotSaveData.Item);
        return itemSlot;
    }

    public void FillItemsBag(ItemsBag itemsBag, ItemsBagSaveData itemsBagSaveData)
    {
        itemsBag.itemsSlots = itemsBagSaveData.ItemsSlots.Select(ToItemSlot).ToList();
    }

}

