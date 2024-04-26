using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public ItemSlot LeftHand;
    public ItemSlot RightHand;
    public ItemSlot Chest;
    public ItemSlot Head;
    public ItemSlot Legs;
    public ItemSlot Gloves;

    public ItemSlot LeftRing;
    public ItemSlot RightRing;
    public ItemSlot Amulet;

    public IEnumerable<ItemSlot> AllItemSlots
    {
        get
        {
            yield return LeftHand;
            yield return RightHand;
            yield return Head;
            yield return Chest;
            yield return Legs;
            yield return LeftRing;
            yield return RightRing;
            yield return Amulet;
            yield return Gloves;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {   
        LeftHand = createItemsSlot(ItemSlot.Type.Hand);
        RightHand = createItemsSlot(ItemSlot.Type.Hand);
        Chest = createItemsSlot(ItemSlot.Type.Chest);
        Head = createItemsSlot(ItemSlot.Type.Head);
        Legs = createItemsSlot(ItemSlot.Type.Boots);
        LeftRing = createItemsSlot(ItemSlot.Type.Ring);
        RightRing = createItemsSlot(ItemSlot.Type.Ring);
        Amulet = createItemsSlot(ItemSlot.Type.Amulet);
        Gloves = createItemsSlot(ItemSlot.Type.Gloves);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static ItemSlot createItemsSlot(ItemSlot.Type type)
    {
        var slot = new ItemSlot();
        slot.type = type;
        return slot;
    }
}
