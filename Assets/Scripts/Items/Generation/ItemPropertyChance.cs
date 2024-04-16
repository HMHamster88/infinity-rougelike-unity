using System;
using UnityEngine;

[Serializable]
public class ItemPropertyChance
{
    [Range(0.0F, 1.0F)]
    [SerializeField]
    private float chance = 0;
    [SerializeReference]
    private ItemPropertyGenerationRule rule = new ItemDamagePropertyGenerationRule();

    public float Chance { get => chance; }
    public ItemPropertyGenerationRule Rule { get => rule; }

    public void GenerateProperty(GameObject item, int level)
    {
        if (UnityEngine.Random.Range(0, 1) <= chance) 
        {
            rule.GenerateProperty(item, level);
        }
    }
}
