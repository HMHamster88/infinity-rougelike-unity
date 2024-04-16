using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEx
{
    public static Vector2 Vector2()
    {
        var attackAngleRads = Random.Range(0, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(attackAngleRads), Mathf.Sin(attackAngleRads));
    }

    public static Vector2Int PointInRectInclusive(RectInt rect)
    {
        return new Vector2Int(Random.Range(rect.xMin, rect.xMax + 1), Random.Range(rect.yMin, rect.yMax + 1));
    }

    public static bool Chance(float chance)
    {
        return Random.Range(0.0f, 1.0f) <= chance;
    }

    public static List<T> GetWeightChances<T>(IList<T> chances, int count) where T : IChance
    {
        var totalChance = chances.Sum(chance => chance.Chance);
        var random = Random.Range(0, totalChance);
        return Enumerable.Range(0, count)
            .Select(attempt => getChace(chances, totalChance))
            .ToList();
    }

    private static T getChace<T>(IList<T> chances, float totalChance) where T : IChance
    {
        var chanceSum = 0.0f;
        var random = Random.Range(0, totalChance);
        for (int i = 0; i < chances.Count; i++)
        {
            var chance = chances[i];
            chanceSum += chance.Chance;
            if (random <= chanceSum)
            {
                return chance;
            }
        }
        return chances[0];
    }

}
