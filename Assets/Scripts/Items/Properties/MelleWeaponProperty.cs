using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MelleWeaponProperty : WeaponProperty
{
    [SerializeField]
    public float Distance = 0.5f;
    [SerializeField]
    public float MaxDistance = 1.0f;
    [SerializeField]
    public float AttackAngleRange = 90;
    [SerializeField]
    public float AttackTime = 0.2f;

    private GameObject attacker;
    private Vector2 targetPoint;
    private float currentAttackTime;
    private List<GameObject> alreadyHitted = new List<GameObject>();
    private Item weaponItem;

    protected override string descriptionKey => null;

    public override void Attack(GameObject attacker, Vector2 targetPoint, Item weaponItem)
    {
        this.attacker = attacker;
        this.targetPoint = targetPoint;
        this.weaponItem = weaponItem;
        this.currentAttackTime = 0;
        this.alreadyHitted.Clear();

        var audioSource = attacker.GetComponent<AudioSource>();
        if (audioSource != null) 
        {
            audioSource.clip = AttackSounds.GetRandomElement();
            audioSource.Play();
        }

        GetAnimationLine().GetComponentInChildren<SpriteRenderer>().enabled = true;
        checkColliders();
    }

    private void checkColliders()
    {
        var time = currentAttackTime / AttackTime; // 0 - 1 time

        var attackerPosition = attacker.transform.position;
        var deltaPosition = targetPoint - (Vector2)attackerPosition;
        var centerAttackAngle = Vector2.SignedAngle(Vector2.right, deltaPosition);

        var attackAngle = centerAttackAngle - AttackAngleRange / 2 + AttackAngleRange * time;

        var attackAngleRads = attackAngle * Mathf.Deg2Rad;
        var distance = Distance + (MaxDistance - Distance) * time;
        var endPosition = distance * new Vector2(Mathf.Cos(attackAngleRads), Mathf.Sin(attackAngleRads)) + (Vector2)attackerPosition;

        var animationTransform = GetAnimationLine();
        


        var hits = Physics2D.LinecastAll(attackerPosition, endPosition);
        Debug.Log($"Hits {hits.Length}");

        var filtredHits = hits
            .Where(hit => hit.collider.gameObject != attacker)
            .Where(hit => !alreadyHitted.Contains(hit.collider.gameObject))
            .OrderBy(hit => hit.distance);

        var collidedObjects = filtredHits
            .Select(hit => hit.collider.gameObject)
            .ToList();

        var firstObject = collidedObjects.FirstOrDefault();

        if (firstObject != null)
        {
            alreadyHitted.Add(firstObject);
            var damageReceiver = firstObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
            {
                AttackCalculator.ApplyAttack(attacker, firstObject, weaponItem);
            }
        }

        animationTransform.rotation = Quaternion.Euler(0, 0, attackAngle);
        animationTransform.localScale = new Vector3(distance, 1, 1);
    }

    private Transform GetAnimationLine()
    {
        return attacker.transform.Find("MeleeWeaponAttack");
    }

    public override void Update()
    {
        if (attacker == null)
        {
            return;
        }
        checkColliders();
        Debug.Log("Update melle");
        if (currentAttackTime > AttackTime)
        {
            currentAttackTime = 0;
            GetAnimationLine().GetComponentInChildren<SpriteRenderer>().enabled = false;
            this.attacker = null;
        }
        else
        {
            currentAttackTime += Time.deltaTime;
        }
    }
}
