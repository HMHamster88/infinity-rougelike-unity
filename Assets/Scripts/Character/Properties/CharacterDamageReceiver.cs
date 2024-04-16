using UnityEngine;

public class CharacterDamageReceiver : DamageReceiver
{
    public GameObject corpsePrefab;

    protected override float CovertDamage(ComplexDamage damage)
    {
        // TODO aplly equpment, debufs, etc
        return damage.GetOveralDamage();
    }

    protected override void Die()
    {
        if (corpsePrefab != null) 
        {
            var corpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity, gameObject.transform.parent);
            if (corpse.TryGetComponent<ItemsBag>(out var itemsBag) && TryGetComponent<Character>(out var character) && TryGetComponent<LootGenerator>(out var lootGenerator))
            {
                itemsBag.LayItems(lootGenerator.Generate(character.Level));
            }
        }
        Destroy(gameObject);
    }
}
