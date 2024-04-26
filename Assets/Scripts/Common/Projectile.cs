using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public List<AudioClip> ImpactSounds = new List<AudioClip>();

    public MissleWeaponProperty MissleWeaponProperty;
    public Item WeaponItem;
    public GameObject Attacker;
    public Vector2 TargetPoint;
    public float MaxLiveTime = 5;

    private float LiveTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Vector2.Distance(this.transform.position, TargetPoint) > MissleWeaponProperty.Distance)
        {
            DestroyProjectile();
        }
        LiveTime += Time.deltaTime;
        if (LiveTime > MaxLiveTime)
        {
            DestroyProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ImpactSounds.Count > 0)
        {
            AudioSource.PlayClipAtPoint(ImpactSounds.GetRandomElement(), collision.transform.position);
        }
        // TODO make onather object with audioSource

        if (collision.gameObject.TryGetComponent<DamageReceiver>(out var damageReceiver))
        {
            AttackCalculator.ApplyAttack(Attacker, collision.gameObject, WeaponItem);
        }

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // TODO apply some effects as explosions
        Destroy(this.gameObject);
    }
}
