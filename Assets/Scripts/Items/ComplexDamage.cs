using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class ComplexDamage
{
    private Dictionary<DamageType, Damage> damages = new Dictionary<DamageType, Damage>();

    public void AddDamage(Damage damage)
    {
        if (damages.ContainsKey(damage.Type)) 
        {
            damages[damage.Type].Value += damage.Value;
        }
        else
        {
            damages.Add(damage.Type, damage);
        }
    }

    public void SetDamage(Damage damage)
    {
        damages[damage.Type] = damage;
    }

    public Damage GetDamage(DamageType type)
    {
        return damages[type];
    }

    public float GetOveralDamage()
    {
        return damages.Values.Select(damage => damage.Value).Sum();
    }
}
