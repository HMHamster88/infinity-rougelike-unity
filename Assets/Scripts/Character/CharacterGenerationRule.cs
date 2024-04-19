using UnityEngine;

[CreateAssetMenu(fileName = "ChraracterGenerationRule", menuName = "Scriptable Objects/ChraracterGenerationRule")]
public class CharacterGenerationRule : ScriptableObject
{
    [SerializeField]
    private GameObject basePrefab;

    [SerializeField]
    private IntLevelValue baseHealth = new();

    [SerializeField]
    public LootGenerationRule LootGenerationRule;

    [SerializeField]
    private EquipmentGenerationRule equipmentGenerationRule = new EquipmentGenerationRule();

    public GameObject Generate(Vector3 worldCoords, Transform parent, int level, ItemGenerator itemGenerator)
    {
        var character = Instantiate(basePrefab, worldCoords, Quaternion.identity, parent);
        if (character.TryGetComponent<Health>(out var health))
        {
            health.BaseMaxValue = baseHealth.GetValue(level);
            health.Value = health.BaseMaxValue;
        }
        if (character.TryGetComponent<Equipment>(out var equipment))
        {
            equipmentGenerationRule.Generate(equipment, level);
        }
        if (character.TryGetComponent<Character>(out var characterComponent))
        {
            characterComponent.Level = level;
        }
        if (character.TryGetComponent<LootGenerator>(out var lootGenerator))
        {
            lootGenerator.ItemGenerator = itemGenerator;
            lootGenerator.LootGenerationRule = LootGenerationRule;
        }
        return character;
    }
}
