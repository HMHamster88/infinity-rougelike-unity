using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ListExtesions
{
    public static T GetRandomElement<T>(this IList<T> values)
    {
        return values[Random.Range(0, values.Count)];
    }

    public static T GetRandomElement<T>(this HashSet<T> values)
    {
        return values.ElementAt(Random.Range(0, values.Count));
    }
}
