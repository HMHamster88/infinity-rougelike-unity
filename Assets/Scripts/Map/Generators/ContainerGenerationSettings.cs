using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContainerGenerationSettings
{
    [Range(0.0f, 1.0f)]
    public float PerRoomChance = 0.1f;
    public MinMaxInt ItemsCount;
    public List<GameObject> ContainersPrefabs = new List<GameObject>();
}
