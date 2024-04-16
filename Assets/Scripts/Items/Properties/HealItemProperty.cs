using UnityEngine;

public class HealItemProperty : ApplyItemProperty
{
    [Range(0, 1f)]
    public float HealAmouth = 0.25f;

    public float HealAmountPercents => HealAmouth * 100;

    protected override string descriptionKey => "item_heals";

    public override bool Apply(GameObject gameObject)
    {
        var healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null)
        {
            return false;
        }
        if (healthComponent.Value == healthComponent.MaxValue)
        {
            return false;
        }
        var newHealth = healthComponent.Value + healthComponent.MaxValue * HealAmouth;
        healthComponent.Value = Mathf.Min(newHealth, healthComponent.MaxValue);
        return true;
    }
}
