
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private LevelItemGenerationRule[] levelItemGenerationRules;

    private void OnEnable()
    {
        levelItemGenerationRules = Resources.LoadAll<LevelItemGenerationRule>("ScriptableObjects/LevelItemGenerationRules");
    }

    public List<Item> GenerateItems(int level, int attemps)
    {
        var levels = levelItemGenerationRules
            .Where(rule => rule.Level <= level)
            .ToList();
        return Enumerable.Range(0, attemps)
            .SelectMany(attemp => levels)
            .SelectMany(rule => rule.Generate(level))
            .ToList();
    }

    public List<Item> GenerateItems100Chance(int level, MinMaxInt count)
    {
        return GenerateItems100Chance(level, count.GetRandomInclusive());
    }

    public List<Item> GenerateItems100Chance(int level, int minCount, int maxCount)
    {
        var count = Random.Range(minCount, maxCount + 1);
        return GenerateItems100Chance(level, count);
    }

    public List<Item> GenerateItems100Chance(int level, int count)
    {
        var allChances = getAllLevelItemChances(level).ToList();
        return RandomEx.GetWeightChances(allChances, count)
            .Select(chance => chance.Rule.Generate(level))
            .ToList();
    }

    private IEnumerable<ItemDropChance> getAllLevelItemChances(int level)
    {
        return levelItemGenerationRules
            .Where(levelRule => levelRule.Level <= level)
            .SelectMany(levelRule => levelRule.DropChances)
            .ToList();
    }

    public List<Item> GenerateItems(int level, LootGenerationRule lootGenerationRule)
    {
        var allChances = getAllLevelItemChances(level)
            .Concat(lootGenerationRule.SpecialItems)
            .ToList();
        var itemsCount = lootGenerationRule.ItemsCount.GetRandomInclusive(level);

        var result = new List<Item>();
        for(var attempt  = 0; attempt < itemsCount; attempt++)
        {
            if(allChances.Count == 0)
            {
                return result;
            }
            var chance = RandomEx.GetWeightChance(allChances);
            var item = chance.Rule.Generate(level);
            result.Add(item);
            allChances.Remove(chance);
        }
        return result;
    }

}
