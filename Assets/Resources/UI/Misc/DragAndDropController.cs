using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropController : PointerManipulator
{
    private readonly List<IDragAndDropRule> rules = new List<IDragAndDropRule>();
    public Vector2 iconShift = new Vector2(8, 8);

    public DragAndDropController(VisualElement target, List<IDragAndDropRule> rules)
    {
        this.target = target;
        this.rules = rules;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        // Register the four callbacks on target.
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (currentRule != null && currentRule.DragAndDropCursorElement != null)
        {
            setIconPosition(evt);
        }
    }

    private void setIconPosition(IPointerEvent evt)
    {
        var style = currentRule.DragAndDropCursorElement.style;
        style.left = evt.position.x - iconShift.x;
        style.top = evt.position.y - iconShift.y;
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
    }

    private VisualElement startDragElement;

    private IDragAndDropRule currentRule;

    private void PointerDownHandler(PointerDownEvent evt)
    {
        if (evt.button != 0)
        {
            return;
        }
        startDragElement = evt.target as VisualElement;
        if (startDragElement == null)
        {
            return;
        }
        currentRule = getRule(startDragElement);
        if (currentRule != null)
        {
            if (currentRule.DragStart(startDragElement))
            {
                target.CapturePointer(evt.pointerId);
                if (currentRule.DragAndDropCursorElement != null)
                {
                    setIconPosition(evt);
                    currentRule.DragAndDropCursorElement.style.display = DisplayStyle.Flex;
                }
            }
        }
    }

    private IDragAndDropRule getRule(VisualElement element)
    {
        var rule = rules.Where(rule => rule.IsDraggable(element)).FirstOrDefault();
        if (rule != null)
        {
            return rule;
        }
        if (element.parent != null)
        {
            return getRule(element.parent);
        }
        return null;
    }

    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (currentRule != null && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
            if (currentRule.DragAndDropCursorElement != null)
            {
                currentRule.DragAndDropCursorElement.style.display = DisplayStyle.None;
            }
            var element = target.panel.Pick(evt.position);
            currentRule.DragEnd(element);
            currentRule = null;
        }
    }
}
