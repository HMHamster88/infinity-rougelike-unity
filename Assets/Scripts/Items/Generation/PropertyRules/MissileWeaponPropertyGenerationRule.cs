using UnityEngine;

public class MissleWeaponPropertyGenerationRule : WeaponPropertyGenerationRule
{
    [SerializeField]
    private float Distance = 10.0f;
    [SerializeField]
    public GameObject ProjectilePrefab;
    [SerializeField]
    private float ProjectileStartSpeed = 10.0f;
    [SerializeField]
    private float AttackAngleScatter = 5.0f;

    public override ItemProperty GenerateProperty(int level)
    {
        var property = new MissleWeaponProperty();
        SetParentProps(property);
        property.Distance = Distance;
        property.ProjectilePrefab = ProjectilePrefab;
        property.ProjectileStartSpeed = ProjectileStartSpeed;
        property.AttackAngleScatter = AttackAngleScatter;
        return property;
    }
}
