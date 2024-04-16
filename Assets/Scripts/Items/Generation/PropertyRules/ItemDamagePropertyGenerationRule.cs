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

    public override void GenerateProperty(GameObject item, int level)
    {
        var damageProperty = item.AddComponent<DamageItemProperty>();
        damageProperty.Type = type;
        damageProperty.MinValue = MinDamage.GetMin(level);
        damageProperty.MaxValue = MaxDamage.GetMax(level);
    }
}
