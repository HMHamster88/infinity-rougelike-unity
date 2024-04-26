using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProperty : ItemProperty
{
    public float AttacksPerSecond = 2;
    [JsonIgnore]
    public List<AudioClip> AttackSounds;

    public abstract void Attack(GameObject attacker, Vector2 targetPoint, Item weaponItem);

    public override void SetDataFromRule()
    {
        var generationRule = (WeaponPropertyGenerationRule) GenerationRule;
        AttackSounds = generationRule.AttackSounds;
    }
}
