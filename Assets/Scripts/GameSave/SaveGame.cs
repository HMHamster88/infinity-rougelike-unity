using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveGame
{
    public string Name = "SaveGame";
    public List<ItemSlot> PlayerItems;
    public EquipmentSaveData PlayerEquipment;
}
