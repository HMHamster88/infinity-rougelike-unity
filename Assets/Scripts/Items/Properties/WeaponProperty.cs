using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProperty : MonoBehaviour
{
    public float AttacksPerSecond = 2;
    public List<AudioClip> AttackSounds;

    public abstract void Attack(GameObject attacker, Vector2 targetPoint);
}
