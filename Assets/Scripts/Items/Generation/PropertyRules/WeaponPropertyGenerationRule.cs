using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPropertyGenerationRule : ItemPropertyGenerationRule
{
    [SerializeField]
    protected float attacksPerSecond = 2;
    [SerializeField]
    public List<AudioClip> AttackSounds;

    protected void SetParentProps(WeaponProperty weaponProperty)
    {
        weaponProperty.AttackSounds = AttackSounds;
        weaponProperty.AttacksPerSecond = attacksPerSecond;
        weaponProperty.GenerationRule = this;
    }
}
