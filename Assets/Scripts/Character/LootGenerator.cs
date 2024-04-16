using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    public ItemGenerator ItemGenerator;
    public int MaxItemsCount = 1;

    public List<Item> Generate(int level)
    {
        return ItemGenerator.GenerateItems100Chance(level, Random.Range(0, MaxItemsCount + 1));
    }
}
