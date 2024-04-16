using UnityEngine;

public class DamageItemProperty : ItemProperty
{
    public float MinValue = 1;
    public float MaxValue = 5;
    public DamageType Type = DamageType.Physical;

    protected override string descriptionKey => "item_damage";

    public Damage GetRandomDamage()
    {
        return new Damage(Type, Random.Range(MinValue, MaxValue));
    }
}
