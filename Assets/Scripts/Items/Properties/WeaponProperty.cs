using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProperty : ItemProperty
{
    public float AttacksPerSecond = 2;
    public List<AudioClip> AttackSounds;

    public abstract void Attack(GameObject attacker, Vector2 targetPoint, Item weaponItem);
}
