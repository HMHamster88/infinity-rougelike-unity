using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContainerGenerationChance
{
    [Range(0.0f, 1.0f)]
    public float PerRoomChance = 0.1f;
    public LootGenerationRule LootGenerationRule;
    public GameObject ContainerPrefab;
}
