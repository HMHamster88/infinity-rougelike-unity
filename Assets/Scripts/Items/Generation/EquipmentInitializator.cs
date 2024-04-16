using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EquipmentInitializator : InjectComponentBehaviour
{
    [SerializeField]
    private EquipmentGenerationRule equipmentGenerationRule = new EquipmentGenerationRule();
    [SerializeField]
    private int level = 1;
    [GetComponent]
    private Equipment equipment;
    
    void Start()
    {
        if (equipment != null)
        {
            equipmentGenerationRule.Generate(equipment, level);
        }
    }

}
