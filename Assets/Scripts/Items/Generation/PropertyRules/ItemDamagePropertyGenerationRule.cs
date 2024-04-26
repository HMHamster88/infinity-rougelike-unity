using System;
using UnityEngine;

[Serializable]
public class ItemDamagePropertyGenerationRule : ItemPropertyGenerationRule
{
    [SerializeField]
    private DamageType type = DamageType.Physical;

    [SerializeField]
    private IntLevelValueRange MinDamage = new();

    [SerializeField]
    private IntLevelValueRange MaxDamage = new();

    public override ItemProperty GenerateProperty(int level)
    {
        var damageProperty = new DamageItemProperty();
        damageProperty.GenerationRule = this;
        damageProperty.Type = type;
        damageProperty.MinValue = MinDamage.GetMin(level);
        damageProperty.MaxValue = MaxDamage.GetMax(level);
        return damageProperty;
    }
}
