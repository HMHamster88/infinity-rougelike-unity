using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot : ScriptableObject
{
    public enum Type
    {
        Bag,
        Hand,
        Head,
        Chest,
        Gloves,
        Boots,
        Amulet,
        Ring
    }

    public Type type;
    
    public Item Item;

    private static Sprite[] backgroundIcons;

    [CreateProperty]
    public Sprite BackgroundIcon
    {
        get 
        {
            if (backgroundIcons == null)
            {
                backgroundIcons = Resources.LoadAll<Sprite>("UI/Icons/ItemsSlots");
            }
            if (type == Type.Bag)
            {
                return null;
            }
            return backgroundIcons[(int)type - 1];
        }
    }

    [CreateProperty]
    public bool HasItem
    {
        get
        {
            return Item != null;
        }
    }

    [CreateProperty]
    public Sprite ItemSprite
    {
        get
        {
            if (Item == null)
            {
                return null;
            }
            var sprite = Item.Sprite;
            return sprite;
        }
    }

    [CreateProperty]
    public int? ItemQuantity
    {
        get
        {
            if (Item == null)
            {
                return null;
            }
            var quantityProperty = Item.GetComponent<ItemQuantity>();
            if (quantityProperty == null)
            {
                return null;
            }
            return quantityProperty.Quantity;
        }
    }

}
