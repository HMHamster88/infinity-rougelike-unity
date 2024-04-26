using UnityEngine;

public class HealItemPropertyGenerationRule : ItemPropertyGenerationRule
{
    [Range(0, 1f)]
    [SerializeField]
    private float healAmouth = 0.25f;

    public override ItemProperty GenerateProperty(int level)
    {
        var property = ScriptableObject.CreateInstance<HealItemProperty>();
        property.HealAmouth = healAmouth;
        return property;
    }
}
