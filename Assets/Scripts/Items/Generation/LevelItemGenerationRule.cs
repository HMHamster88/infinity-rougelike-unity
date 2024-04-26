using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItemGenerationRule", menuName = "Scriptable Objects/Level Item Generation Rule")]
public class LevelItemGenerationRule : ScriptableObject
{
    [SerializeField]
    private int level;
    [SerializeField]
    private List<ItemDropChance> dropChances = new List<ItemDropChance>();

    public int Level { get => level; }
    public List<ItemDropChance> DropChances { get => dropChances; }

    public IEnumerable<Item> Generate(int level) 
    {
        return dropChances
            .Select(dropChance => dropChance.Generate(level))
            .Where(item => item != null);
    }
}
