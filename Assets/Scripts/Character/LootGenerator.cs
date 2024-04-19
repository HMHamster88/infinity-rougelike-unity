using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    public ItemGenerator ItemGenerator;
    public LootGenerationRule LootGenerationRule;

    public List<Item> Generate(int level)
    {
        return ItemGenerator.GenerateItems(level, LootGenerationRule);
    }
}
