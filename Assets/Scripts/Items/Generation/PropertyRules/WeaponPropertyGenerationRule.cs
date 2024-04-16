using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponPropertyGenerationRule : ItemPropertyGenerationRule
{
    [SerializeField]
    protected float AttacksPerSecond = 2;
    [SerializeField]
    protected List<AudioClip> attackSounds;

    protected void SetParentProps(WeaponProperty weaponProperty)
    {
        weaponProperty.AttackSounds = attackSounds;
        weaponProperty.AttacksPerSecond = AttacksPerSecond;
    }
}
