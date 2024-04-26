using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Overlays;

[Serializable]
public class EquipmentSaveData
{
    public ItemSlot LeftHand = new ();
    public ItemSlot RightHand = new ();
    public ItemSlot Chest = new ();
    public ItemSlot Head = new ();
    public ItemSlot Legs = new ();
    public ItemSlot Gloves = new ();

    public ItemSlot LeftRing = new ();
    public ItemSlot RightRing = new ();
    public ItemSlot Amulet = new ();

    [JsonIgnore]
    private IEnumerable<ItemSlot> AllItemSlots
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

    public static EquipmentSaveData FromEquipment(Equipment equipment)
    {
        var saveData = new EquipmentSaveData();

        foreach(var (saveSlot, equipmentSlot) in saveData.AllItemSlots.Zip(equipment.AllItemSlots, (x, y) => (x, y)))
        {
            saveSlot.Item = equipmentSlot.Item;
            saveSlot.type = equipmentSlot.type;
        }
        return saveData;
    }

    public void SetItems(Equipment equipment)
    {
        foreach (var (saveSlot, equipmentSlot) in AllItemSlots.Zip(equipment.AllItemSlots, (x, y) => (x, y)))
        {
            equipmentSlot.Item = saveSlot.Item;
        }
    }
}

