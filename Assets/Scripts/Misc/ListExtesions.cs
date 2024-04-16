using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtesions
{
    public static T GetRandomElement<T>(this IList<T> values)
    {
        return values[Random.Range(0, values.Count)];
    }
}
