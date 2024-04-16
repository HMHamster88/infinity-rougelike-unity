using UnityEngine;

public class Damage
{
    public DamageType Type;
    public float Value;

    public Damage(DamageType type, float value)
    {
        this.Type = type;
        this.Value = value;
    }

}
