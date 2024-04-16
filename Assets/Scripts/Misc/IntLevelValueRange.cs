using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class IntLevelValueRange
{
    [SerializeField]
    private IntLevelValue min = new IntLevelValue();
    [SerializeField]
    private IntLevelValue max = new IntLevelValue();

    public int GetMin(int value)
    {
        return min.GetValue(value);
    }
    public int GetMax(int level)
    {
        return max.GetValue(level);
    }

    public int GetRandomInclusive(int level)
    {
        return UnityEngine.Random.Range(GetMin(level), GetMax(level));
    }
}
