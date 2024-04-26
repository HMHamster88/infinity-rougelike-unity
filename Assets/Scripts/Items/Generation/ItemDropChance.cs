using System;
using UnityEngine;

[Serializable]
public class ItemDropChance : IChance
{
    [Range(0.0F, 1.0F)]
    [SerializeField]
    private float chance;
    [SerializeField]
    private ItemGenerationRule rule;

    public float Chance { get => chance; }
    public ItemGenerationRule Rule { get => rule; }

    public Item Generate(int level)
    {
        if (UnityEngine.Random.Range(0, 1.0f) <= chance)
        {
            return rule.Generate(level);
        }
        return null;
    }
}
