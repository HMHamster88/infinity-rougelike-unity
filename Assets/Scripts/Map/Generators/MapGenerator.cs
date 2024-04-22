using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapGenerator: ScriptableObject
{
    [Range(0, 10000)]
    public int Width = 64 + 1;
    [Range(0, 10000)]
    public int Height = 64 + 1;

    public int MinimalLevel = 0;
    public abstract void GenerateMap(MapBehaviour mapBehaviour, int levels);
}
