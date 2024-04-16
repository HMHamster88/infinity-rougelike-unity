using UnityEngine;

public class MelleWeaponPropertyGenerationRule : WeaponPropertyGenerationRule
{
    [SerializeField]
    private float Distance = 1.5f;
    [SerializeField]
    private float MaxDistance = 1.5f;
    [SerializeField]
    private float AttackAngleRange = 20;
    [SerializeField]
    private float AttackTime = 0.2f;

    public override void GenerateProperty(GameObject item, int level)
    {
        var property = item.AddComponent<MelleWeaponProperty>();
        SetParentProps(property);
        property.AttacksPerSecond = AttacksPerSecond;
        property.Distance = Distance;
        property.MaxDistance = MaxDistance;
        property.AttackAngleRange = AttackAngleRange;
        property.AttackTime = AttackTime;
    }
}
