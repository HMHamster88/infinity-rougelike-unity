using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsBag : MonoBehaviour
{
    public List<ItemSlot> itemsSlots = new List<ItemSlot> ();
    public int initialSlotsCount = 16;

    public IEnumerable<ItemSlot> FilledSlots
    {
        get
        {
            return itemsSlots.Where(slot => slot.HasItem);
        }
    }

    public IEnumerable<ItemSlot> FreeSlots
    {
        get
        {
            return itemsSlots.Where(slot => !slot.HasItem);
        }
    }

    public IEnumerable<Item> AllItems
    {
        get
        {
            return itemsSlots.Where(slot => slot.HasItem).Select(slot => slot.Item);
        }
    }

    private void OnEnable()
    {
        itemsSlots.AddRange(Enumerable.Range(0, initialSlotsCount).Select(x => ItemSlot.CreateInstance<ItemSlot>()));
    }

    public void LayItems(List<Item> items)
    {
        foreach (var (itemSlot, item) in itemsSlots.Where(slot => slot.Item == null).Zip(items, (x, y) => (x, y)))
        {
            itemSlot.Item = item;
        }
    }

    public bool FindQuantitySlot(ItemQuantity itemQuantity, out ItemSlot slot)
    {
        slot = FilledSlots.Where(slot =>
        {
            if (slot.Item.TryGetComponent<ItemQuantity>(out var quantity))
            {
                return quantity.SameItem(itemQuantity);
            }
            return false;
        })
            .FirstOrDefault();
        return slot != null;
    }

    public void LayFromBag(ItemsBag source)
    {
        foreach(var sourceSlot in source.FilledSlots) 
        {
            var sourceItem = sourceSlot.Item;
            if (sourceItem.TryGetComponent<ItemQuantity>(out var sourceQuantity) && FindQuantitySlot(sourceQuantity, out var targetSlot))
            {
                var targetQuantity = targetSlot.Item.GetComponent<ItemQuantity>();
                targetQuantity.Quantity += sourceQuantity.Quantity;
                sourceSlot.Item = null;
                Destroy(sourceItem);
            }
            else
            {
                var freeSlot = FreeSlots.FirstOrDefault();
                if (freeSlot != null)
                {
                    freeSlot.Item = sourceItem;
                    sourceSlot.Item = null;
                }
            }
        }
    }
}