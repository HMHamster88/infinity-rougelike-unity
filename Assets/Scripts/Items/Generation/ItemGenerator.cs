
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
            .Select(i => i.GetComponent<Item>())
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
        var allChances = levelItemGenerationRules
            .Where(levelRule => levelRule.Level <= level)
            .SelectMany(levelRule => levelRule.DropChances)
            .ToList();
        return RandomEx.GetWeightChances(allChances, count)
            .Select(chance => chance.Rule.Generate(level))
            .Select(itemObject => itemObject.GetComponent<Item>())
            .ToList();
    }


}
