using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

[Serializable]
public abstract class ItemProperty: ScriptableObject
{
    protected abstract string descriptionKey { get; }

    private LocalizedString description;

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
}
