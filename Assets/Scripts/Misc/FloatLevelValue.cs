using System;
using UnityEngine;

[Serializable]
public class FloatLevelValue
{
    [SerializeField]
    private float baseValue = 1;
    [SerializeField]
    private float linearFactor = 0;
    [SerializeField]
    private float cubicFactor = 0;

    public float GetValue(int level)
    {
        return baseValue + linearFactor * level + cubicFactor * level * level;
    }
}
