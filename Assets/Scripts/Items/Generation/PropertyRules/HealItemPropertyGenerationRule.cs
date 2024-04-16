using UnityEngine;

public class HealItemPropertyGenerationRule : ItemPropertyGenerationRule
{
    [Range(0, 1f)]
    [SerializeField]
    private float healAmouth = 0.25f;

    public override void GenerateProperty(GameObject item, int level)
    {
        var property = item.AddComponent<HealItemProperty>();
        property.HealAmouth = healAmouth;
    }
}
