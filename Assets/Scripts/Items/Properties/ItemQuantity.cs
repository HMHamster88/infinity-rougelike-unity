using UnityEngine;

public class ItemQuantity : ItemProperty
{
    public int Quantity = 1;
    public string SameItemIdentifier;

    protected override string descriptionKey => "item_quantity";

    public bool SameItem(ItemQuantity other)
    {
        if (SameItemIdentifier == null)
        {
            return false;
        }
        return SameItemIdentifier == other.SameItemIdentifier;
    }
}
