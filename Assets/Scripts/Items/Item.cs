using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;

public class Item : MonoBehaviour, INamed
{
    public LocalizedString Name;
    public ItemSlot.Type SlotType = ItemSlot.Type.Bag;
    public Sprite Sprite;
    public ItemGenerationRule generationRule;

    [CreateProperty]
    public int? Quantity
    {
        get
        {
            var quantityProperty = GetComponent<ItemQuantity>();
            if (quantityProperty == null)
            {
                return null;
            }
            return quantityProperty.Quantity;
        }
    }

    string INamed.Name => Name.GetLocalizedString();
}
