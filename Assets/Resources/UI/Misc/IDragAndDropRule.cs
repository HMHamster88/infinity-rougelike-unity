using UnityEngine;
using UnityEngine.UIElements;

public interface IDragAndDropRule
{
    bool IsDraggable(VisualElement visualElement);
    bool DragStart(VisualElement visualElement);
    void DragEnd(VisualElement visualElement);

    public Sprite getIcon();

    public VisualElement DragAndDropCursorElement { get; }
}
