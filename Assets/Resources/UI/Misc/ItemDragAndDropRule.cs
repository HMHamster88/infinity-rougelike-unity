using UnityEngine;
using UnityEngine.UIElements;

public class ItemDragAndDropRule : IDragAndDropRule
{
    private Item draggedItem;
    private ItemSlot dragItemSlot;
    private VisualElement dragItemIcon;

    public VisualElement DragAndDropCursorElement => dragItemIcon;

    public ItemDragAndDropRule(VisualElement dragItemIcon)
    {
        this.dragItemIcon = dragItemIcon;
    }

    public void DragEnd(VisualElement visualElement)
    {
        if (draggedItem != null)
        {
            if (dragItemIcon != null)
            {
                dragItemIcon.dataSource = null;
            }
            var dropItemSlot = visualElement.GetDataSourceWithPathRecursive<ItemSlot>();
            if (canDropItem(draggedItem, dropItemSlot))
            {
                if (dropItemSlot.Item != null)
                {
                    var dropItemQuantity = dropItemSlot.Item.GetComponent<ItemQuantity>();
                    var dragItemQuantity = draggedItem.GetComponent<ItemQuantity>();
                    dropItemQuantity.Quantity += dragItemQuantity.Quantity;
                    GameObject.Destroy(dragItemQuantity.gameObject);
                }
                else
                {
                    dropItemSlot.Item = draggedItem;
                }
            }
            else
            {
                dragItemSlot.Item = draggedItem;
            }
            draggedItem = null;
        }
    }

    private bool canDropItem(Item item, ItemSlot dropItemSlot) {
        if (dropItemSlot == null)
        {
            return false;
        }
        if (dropItemSlot.Item != null)
        {
            var dropItemQuantity = dropItemSlot.Item.GetComponent<ItemQuantity>();
            var dragItemQuantity = item.GetComponent<ItemQuantity>();
            if (dropItemQuantity == null || dragItemQuantity == null)
            {
                return false;
            }
            if (dropItemQuantity.SameItem(dragItemQuantity))
            {
                return true;
            }
            return false;
        }
        if (dropItemSlot.type == ItemSlot.Type.Bag)
        {
            return true;
        }
        if (dropItemSlot.type != item.SlotType)
        {
            return false;
        }
        return true;
    }

    public bool DragStart(VisualElement visualElement)
    {
        var itemSlot = visualElement.GetDataSourceWithPathRecursive<ItemSlot>();
        if (itemSlot == null)
        {
            return false;
        }
        if (itemSlot.Item == null)
        {
            return false;
        }
        dragItemSlot = itemSlot;
        draggedItem = itemSlot.Item;
        itemSlot.Item = null;
        if (dragItemIcon != null)
        {
            dragItemIcon.dataSource = draggedItem;
        }
        return true;
    }

    public Sprite getIcon()
    {
        return draggedItem.Sprite;
    }


    public bool IsDraggable(VisualElement visualElement)
    {
        return visualElement.name == "ItemView";
    }
}
