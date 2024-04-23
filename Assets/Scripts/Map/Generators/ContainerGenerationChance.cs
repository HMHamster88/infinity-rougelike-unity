using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[System.Serializable]
public class ContainerGenerationChance: IChance
{
    [Range(0.0F, 1.0F)]
    [SerializeField]
    private float chance;
    public float Chance { get => chance; }
    public LootGenerationRule LootGenerationRule;
    public GameObject ContainerPrefab;
}
