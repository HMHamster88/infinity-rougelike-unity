using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterGenerationChance
{
    [Range(0.0f, 1.0f)]
    public float PerRoomChance = 0.1f;
    public CharacterGenerationRule ChraracterGenerationRule;
}
