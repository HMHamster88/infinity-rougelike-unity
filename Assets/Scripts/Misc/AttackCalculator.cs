using Unity.VisualScripting;
using UnityEngine;

public class AttackCalculator
{
    public static void ApplyAttack(GameObject attacker, GameObject target, GameObject weapon)
    {
        // calculate weapon Damage
        var weaponDamages = weapon.GetComponents<DamageItemProperty>();
        var damage = new ComplexDamage();
        foreach (var weponDamage in weaponDamages)
        {
            damage.AddDamage(weponDamage.GetRandomDamage());
        }
        // apply modificators from equipment and buffs of attacker 
        var damageReceiver = target.GetComponent<DamageReceiver>();
        damageReceiver.ReceiveDamage(damage);
    }
}
