using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIExtensions
{
    public static VisualElement GetParentByName(this VisualElement element, string name)
    {
        while (element != null)
        {
            if (element.name == name)
            {
                return element;
            }
            element = element.parent;
        }
        return null;
    }

    public static Vector2 GetPosition(this VisualElement element)
    {
        return element.WorldToLocal(Vector2.zero) * -1.0f;
    }

    public static Vector2 GetSize(this VisualElement element)
    {
        return new Vector2(element.resolvedStyle.width, element.resolvedStyle.height);
    }

    public static Rect GetBounds(this VisualElement element)
    {
        return new Rect(GetPosition(element), GetSize(element));
    }
}
