using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public Health playerHealth;
    public ItemsBag playerItemsBag;
    public MapObjectContainer mapObjectContainer;
    public Equipment playerEquipment;
    public Minimap minimap;

    public GameObject atCursorObject;
    public GameObject AtCursorObject
    {
        get { return atCursorObject; }
        set 
        { 
            atCursorObject = value;
            if (atCursorObject != null)
            {
                AtCursorObjectHealth = atCursorObject.GetComponent<Health>();
            }
            else
            {
                AtCursorObjectHealth = null;
            }
        }
    }



    public Health AtCursorObjectHealth;


    public string ItemDescriptionTooltip;

    // Visual elements
    private VisualElement root;
    private VisualElement mainUIElement;
    private VisualElement characterView;
    private VisualElement mapObjectContainerView;
    private Button closeContainerButton;
    private VisualElement itemDescriptionView;

    public bool ItemDescriptionViewVisibility = false;

    public readonly Wrapper<bool> EscapeWindowVisibility = false;
    public readonly Wrapper<bool> CharacterWindowVisibility = false;
    public readonly Wrapper<bool> ContainerWindowVisibility = false;
    public readonly Wrapper<bool> MapWindowVisibility = false;

    private List<Wrapper<bool>> AllWindowsVisibility;

    public bool AnyWindowShown
    {
        get
        {
            return AllWindowsVisibility.Aggregate((a, b) => a |  b);
        }
    }

    [CreateProperty]
    public bool ShowAtCursorObjectBar { get => AtCursorObject != null; }
    [CreateProperty]
    public bool ShowAtCursorObjectHealth { get => AtCursorObjectHealth != null; }

    [CreateProperty]
    public string AtCursorObjectName
    {
        get
        {
            if (AtCursorObject == null)
            {
                return null;
            }
            return AtCursorObject.GetComponent<INamed>().Name;
        }
    }

    private DragAndDropController dragAndDropController;

    public static void RegisterConverters()
    {
        ConverterGroups.RegisterGlobalConverter((ref bool val) => val ? new StyleEnum<Visibility>(Visibility.Visible) : new StyleEnum<Visibility>(Visibility.Hidden));
        ConverterGroups.RegisterGlobalConverter((ref bool val) => val ? new StyleEnum<DisplayStyle>(DisplayStyle.Flex) : new StyleEnum<DisplayStyle>(DisplayStyle.None));
        ConverterGroups.RegisterGlobalConverter((ref Health val) => val == null ? null : $"{MathF.Round(val.Value)} / {MathF.Round(val.MaxValue)}");
    }

    private void OnEnable()
    {
        RegisterConverters();
    }

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        playerItemsBag = player.GetComponent<ItemsBag>();
        playerEquipment = player.GetComponent<Equipment>();

        AllWindowsVisibility = new List<Wrapper<bool>>()
        {
            EscapeWindowVisibility,
            CharacterWindowVisibility,
            ContainerWindowVisibility,
            MapWindowVisibility
        };

        var document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        root.dataSource = this;

        mainUIElement = root.Q("MainUIElement");

        mainUIElement.RegisterCallback<PointerUpEvent>(pointerUpHandler);
        root.RegisterCallback<PointerMoveEvent>(pointerMoveHandler);

        var dragAndDropItem = root.Q("DragAndDropItem");
        dragAndDropController = new DragAndDropController(root, new List<IDragAndDropRule>() { new ItemDragAndDropRule(dragAndDropItem) });
        characterView = root.Q("CharacterView");
        mapObjectContainerView = root.Q("ContainerView");
        itemDescriptionView = root.Q("ItemDescriptionView");
        closeContainerButton = root.Q<Button>("CloseContainerButton");

    }

    public void GetAllItems()
    {
        playerItemsBag.LayFromBag(mapObjectContainer.ItemsBag);
        if (mapObjectContainer.ItemsBag.AllItems.Count() == 0)
        {
            CloseContainerWindow();
        }
    }

    private void pointerMoveHandler(PointerMoveEvent evt)
    {
        var element = (evt.target as VisualElement).GetParentByName("ItemView");
        if (element == null)
        {
            ItemDescriptionViewVisibility = false;
            return;
        }
        var item = element.GetDataSourceWithPathRecursive<Item>();
        if (item != null)
        {
            ItemDescriptionTooltip = item.Name.GetLocalizedString() + "\r\n" +
            String.Join("\r\n", item.GetComponents<ItemProperty>().Select(ip => ip.LocalizedDescription));
            setTooltipPosition(itemDescriptionView, element);
            ItemDescriptionViewVisibility = true;
        }
        else
        {
            ItemDescriptionTooltip = null;
            ItemDescriptionViewVisibility = false;
        }
    }

    private void setTooltipPosition(VisualElement tooltip, VisualElement element)
    {
        var rootSize = root.GetSize();
        var elementBounds = element.GetBounds();

        var right = rootSize.x - elementBounds.x;
        var left = elementBounds.xMax;
        var bottom = rootSize.y - elementBounds.y;
        var top = elementBounds.yMax;

        var tooltipStyle = tooltip.style;
        if (elementBounds.x < rootSize.x / 2)
        {
            tooltipStyle.left = left;
            tooltipStyle.right = StyleKeyword.Null;
        }
        else
        {
            tooltipStyle.right = right;
            tooltipStyle.left = StyleKeyword.Null;
        }

        if (elementBounds.y < rootSize.y / 2)
        {
            tooltipStyle.top = top;
            tooltipStyle.bottom = StyleKeyword.Null;
        } 
        else
        {
            tooltipStyle.bottom = bottom;
            tooltipStyle.top = StyleKeyword.Null;
        }
    }

    private void pointerUpHandler(PointerUpEvent evt)
    {
        if (evt.button == 1) 
        {
            var itemSlot = (evt.target as VisualElement).GetDataSourceWithPathRecursive<ItemSlot>();
            if (itemSlot != null && itemSlot.Item != null)
            {
                var applyProperty = itemSlot.Item.GetComponent<ApplyItemProperty>();
                if (applyProperty != null)
                {
                    if (applyProperty.Apply(player))
                    {
                        var quantity = applyProperty.GetComponent<ItemQuantity>();
                        if (quantity != null)
                        {
                            quantity.Quantity--;
                        }
                        if (quantity == null || quantity.Quantity <= 0)
                        {
                            itemSlot.Item = null;
                            GameObject.Destroy(quantity.gameObject);
                        }
                    }
                }

            }
        }
    }

    public void CloseContainerWindow()
    {
        if (mapObjectContainer != null)
        {
            mapObjectContainer.Close();
        }
        CharacterWindowVisibility.Value = false;
        ContainerWindowVisibility.Value = false;
        mapObjectContainer = null;
    }

    public void OpenContainer(MapObjectContainer mapObjectContainer)
    {
        this.mapObjectContainer = mapObjectContainer;
        CharacterWindowVisibility.Value = true;
        ContainerWindowVisibility.Value = true;
    }

    public void HideShowCharacterWindow()
    {
        CharacterWindowVisibility.Value = !CharacterWindowVisibility.Value;
    }

    public void Escape()
    {
        if (AnyWindowShown)
        {
            foreach (var visibility in AllWindowsVisibility)
            {
                visibility.Value = false;
            }
            CloseContainerWindow();
        }
        else
        {
            EscapeWindowVisibility.Value = true;
        }
    }

    public void ReturnToGame()
    {
        EscapeWindowVisibility.Value = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HideShowMapWindow()
    {
        MapWindowVisibility.Value = !MapWindowVisibility.Value;
    }
}
