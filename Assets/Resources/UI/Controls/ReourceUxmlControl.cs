using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public abstract partial class ReourceUxmlControl: VisualElement
{
    protected abstract string UxmlResourcePath { get; }

    public ReourceUxmlControl()
    {
        VisualTreeAsset uiAsset = Resources.Load<VisualTreeAsset>(UxmlResourcePath);
        TemplateContainer ui = uiAsset.CloneTree();
        Add(ui);
    }
}
