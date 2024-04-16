using UnityEngine;
using UnityEngine.Localization;

public class LocalizedName : MonoBehaviour, INamed
{
    public LocalizedString Name;
    string INamed.Name => Name.GetLocalizedString();
}
