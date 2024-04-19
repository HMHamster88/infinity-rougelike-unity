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
            if (corpse.TryGetComponent<MapObjectContainer>(out var mapObjectContainer) && TryGetComponent<Character>(out var character) && TryGetComponent<LootGenerator>(out var lootGenerator))
            {
                var corpseLootGenerator = corpse.AddComponent<LootGenerator>();
                corpseLootGenerator.LootGenerationRule = lootGenerator.LootGenerationRule;
                corpseLootGenerator.ItemGenerator = lootGenerator.ItemGenerator;
                mapObjectContainer.Level = character.Level;
            }
        }
        Destroy(gameObject);
    }
}
