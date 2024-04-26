using Newtonsoft.Json;
using UnityEngine;

public class MissleWeaponProperty : WeaponProperty
{
    public float Distance = 10.0f;
    [JsonIgnore]
    public GameObject ProjectilePrefab;
    public float ProjectileStartSpeed = 10.0f;
    public float AttackAngleScatter = 5.0f;

    private GameObject attacker;
    private Vector2 targetPoint;

    protected override string descriptionKey => null;

    public override void Attack(GameObject attacker, Vector2 targetPoint, Item weaponItem)
    {
        this.attacker = attacker;
        this.targetPoint = targetPoint;

        var audioSource = attacker.GetComponent<AudioSource>();
        if (audioSource != null) 
        {
            audioSource.clip = AttackSounds.GetRandomElement();
            audioSource.Play();
        }

        var attackerPosition = attacker.transform.position;
        var deltaPosition = targetPoint - (Vector2)attackerPosition;
        var centerAttackAngle = Vector2.SignedAngle(Vector2.right, deltaPosition);

        var attackAngle = centerAttackAngle + Random.Range(-AttackAngleScatter / 2, AttackAngleScatter / 2);
        var attackAngleRads = attackAngle * Mathf.Deg2Rad;

        var attackDirection = new Vector3(Mathf.Cos(attackAngleRads), Mathf.Sin(attackAngleRads));

        var newProjectile = GameObject.Instantiate(ProjectilePrefab);
        newProjectile.transform.SetPositionAndRotation(attackerPosition, Quaternion.Euler(0, 0, attackAngle));

        var projectileRigidBody = newProjectile.GetComponent<Rigidbody2D>();
        projectileRigidBody.velocity = attackDirection * ProjectileStartSpeed;

        Physics2D.IgnoreCollision(attacker.GetComponent<Collider2D>(), newProjectile.GetComponent<Collider2D>());

        var projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.MissleWeaponProperty = this;
        projectileComponent.WeaponItem = weaponItem;
        projectileComponent.Attacker = attacker;
        projectileComponent.TargetPoint = targetPoint;
    }

    public override void SetDataFromRule()
    {
        base.SetDataFromRule();
        var generationRule = (MissleWeaponPropertyGenerationRule) GenerationRule;
        ProjectilePrefab = generationRule.ProjectilePrefab;

    }
}
