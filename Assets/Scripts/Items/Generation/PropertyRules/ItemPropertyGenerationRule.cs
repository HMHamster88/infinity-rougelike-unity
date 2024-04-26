using System;
using UnityEngine;

[Serializable]
public abstract class ItemPropertyGenerationRule
{
    public abstract ItemProperty GenerateProperty(int level);
}
