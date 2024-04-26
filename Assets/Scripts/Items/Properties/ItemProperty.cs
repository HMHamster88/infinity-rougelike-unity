using Newtonsoft.Json;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

[Serializable]
public abstract class ItemProperty: FixedObject
{
    [JsonConverter(typeof(ItemPropertyGenerationRuleConverter))]
    public ItemPropertyGenerationRule GenerationRule;

    protected abstract string descriptionKey { get; }

    private LocalizedString description;

    [JsonIgnore]
    public string LocalizedDescription
    {
        get
        {
            if (descriptionKey == null)
            {
                return null;
            }
            if (description == null)
            {
                description = new LocalizedString("ItemProperties", descriptionKey)
                {
                    { "prop", new ObjectVariable { Value = this } }
                };
            }
            return description.GetLocalizedString();
        }
    }

    public virtual void SetDataFromRule() { }
}
