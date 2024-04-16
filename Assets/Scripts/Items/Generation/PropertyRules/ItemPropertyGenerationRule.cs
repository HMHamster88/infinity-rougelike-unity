using System;
using UnityEngine;

[Serializable]
public abstract class ItemPropertyGenerationRule
{
    public abstract void GenerateProperty(GameObject item, int level);
}
