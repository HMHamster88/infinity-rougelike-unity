using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public abstract class ItemProperty : MonoBehaviour
{
    protected abstract string descriptionKey { get; }

    private LocalizedString description;

    private void OnEnable()
    {
        if (descriptionKey != null)
        {
            description = new LocalizedString("ItemProperties", descriptionKey)
            {
                { "prop", new ObjectVariable { Value = this } }
            };
        }
    }

    public string LocalizedDescription { get => description.GetLocalizedString(); }
}
