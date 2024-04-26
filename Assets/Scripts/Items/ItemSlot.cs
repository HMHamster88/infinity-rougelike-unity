using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Properties;
using UnityEngine;

public class ItemSlot : FixedObject
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

    [JsonConverter(typeof(StringEnumConverter))]
    public Type type;
    
    public Item Item;

    private static Sprite[] backgroundIcons;

    [JsonIgnore]
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

    [JsonIgnore]
    [CreateProperty]
    public bool HasItem
    {
        get
        {
            return Item != null;
        }
    }

    [JsonIgnore]
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

    [JsonIgnore]
    [CreateProperty]
    public int? ItemQuantity
    {
        get
        {
            if (Item == null)
            {
                return null;
            }
            var quantityProperty = Item.GetProperty<ItemQuantity>();
            if (quantityProperty == null)
            {
                return null;
            }
            return quantityProperty.Quantity;
        }
    }

}
