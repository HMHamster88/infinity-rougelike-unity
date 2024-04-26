using System;
using UnityEngine;

[Serializable]
public class ItemPropertyGenerationRule
{
    [SerializeField]
    public string ID = Guid.NewGuid().ToString();
    public virtual ItemProperty GenerateProperty(int level) 
    {
        throw new NotImplementedException();
    }
}
