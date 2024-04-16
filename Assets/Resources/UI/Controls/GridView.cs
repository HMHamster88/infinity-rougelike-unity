using System.Collections;
using Unity.Properties;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GridView : VisualElement
{
    [UxmlAttribute]
    public string ItemsPropertyPath = "";

    [DontCreateProperty]
    private object items = new object();

    [CreateProperty]
    public object Items
    {
        get
        {
            return items;
        }
        set
        {
            if (value != items)
            {
                items = value;
                rebuildItems();
            }
        }
    }

    private VisualTreeAsset ItemTemplate;

    public GridView()
    {
        RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
        style.flexDirection = FlexDirection.Row;
        style.flexWrap = Wrap.Wrap;
    }

    void OnAttachToPanelEvent(AttachToPanelEvent evt)
    {
        var tc = this.Q<TemplateContainer>();
        if (tc != null)
        {
            tc.style.display = DisplayStyle.None;
            ItemTemplate = tc.templateSource;
        }
        this.SetBinding("Items", new DataBinding()
        {
            dataSourcePath = new PropertyPath(ItemsPropertyPath),
            bindingMode = BindingMode.ToTarget,
            updateTrigger = BindingUpdateTrigger.OnSourceChanged
        });
        rebuildItems();
    }

    private void rebuildItems()
    {
        var dataList = items as IEnumerable;
        if (dataList != null && ItemTemplate != null)
        {
            this.Clear();
            foreach (var data in dataList)
            {
                var item = ItemTemplate.Instantiate();
                item.dataSource = data;
                this.Add(item);
            }
        }
    }
}
