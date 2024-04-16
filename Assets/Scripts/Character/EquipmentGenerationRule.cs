using System;
using UnityEngine;

[Serializable]
public class EquipmentGenerationRule
{
    [SerializeField]
    private ItemGenerationRule leftHand;
    [SerializeField]
    private ItemGenerationRule rightHand;
    [SerializeField]
    private ItemGenerationRule chest;
    [SerializeField]
    private ItemGenerationRule head;
    [SerializeField]
    private ItemGenerationRule legs;
    [SerializeField]
    private ItemGenerationRule gloves;

    [SerializeField]
    private ItemGenerationRule leftRing;
    [SerializeField]
    private ItemGenerationRule rightRing;
    [SerializeField]
    private ItemGenerationRule amulet;

    public void Generate(Equipment equipment, int level)
    {
        generate(equipment.LeftHand, leftHand, level);
        generate(equipment.RightHand, rightHand, level);
        generate(equipment.Chest, chest, level);
        generate(equipment.Head, head, level);  
        generate(equipment.Legs, legs, level);
        generate(equipment.Gloves, gloves, level);
        generate(equipment.LeftRing, leftRing, level);
        generate(equipment.RightRing, rightRing, level);
        generate(equipment.Amulet, amulet, level);
    }

    private void generate(ItemSlot slot, ItemGenerationRule itemGenerationRule, int level)
    {
        if (itemGenerationRule != null)
        {
            slot.Item = itemGenerationRule.Generate(level).GetComponent<Item>();
        }
    }
}