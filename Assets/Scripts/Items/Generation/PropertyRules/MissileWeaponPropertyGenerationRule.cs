using UnityEngine;

public class MissleWeaponPropertyGenerationRule : WeaponPropertyGenerationRule
{
    [SerializeField]
    private float Distance = 10.0f;
    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private float ProjectileStartSpeed = 10.0f;
    [SerializeField]
    private float AttackAngleScatter = 5.0f;

    public override void GenerateProperty(GameObject item, int level)
    {
        var property = item.AddComponent<MissleWeaponProperty>();
        SetParentProps(property);
        property.Distance = Distance;
        property.ProjectilePrefab = ProjectilePrefab;
        property.ProjectileStartSpeed = ProjectileStartSpeed;
        property.AttackAngleScatter = AttackAngleScatter;
    }
}
