using System;
using UnityEngine;

[Serializable]
public class IntLevelValue
{
    [SerializeField]
    private int baseValue = 1;
    [SerializeField]
    private float linearFactor = 0;
    [SerializeField]
    private float cubicFactor = 0;

    public int GetValue(int level)
    {
        return baseValue + Mathf.RoundToInt(linearFactor * level) + Mathf.RoundToInt(cubicFactor * level * level);
    }
}
