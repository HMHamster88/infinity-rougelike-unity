using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementExtensions
{
    public static void HideShowElement(this VisualElement element)
    {
        if (element.style.display != DisplayStyle.Flex)
        {
            ShowElement(element);
        }
        else
        {
            HideElement(element);
        }
    }

    public static void HideElement(this VisualElement element)
    {
        element.style.display = DisplayStyle.None;
    }

    public static void ShowElement(this VisualElement element)
    {
        element.style.display = DisplayStyle.Flex;
    }

    public static object GetDataSourceWithPath(this VisualElement visualElement)
    {
        var context = visualElement.GetHierarchicalDataSourceContext();
        return context.dataSourcePath.GetValue(context.dataSource);
    }

    public static T GetDataSourceWithPathRecursive<T>(this VisualElement visualElement) where T : class
    {
        while (visualElement != null)
        {
            var source = visualElement.GetDataSourceWithPath() as T;
            if (source != null)
            {
                return (T)source;
            }
            visualElement = visualElement.parent;
        }
        return null;
    }

    /// (Extension) Get the value of the serialized property.
    public static object GetValue(this PropertyPath propertyPath, object obj)
    {
        object value = obj;
        for (int i = 0; i < propertyPath.Length; i++)
        {
            value = GetPathComponentValue(value, propertyPath[i]);
        }
        return value;
    }

    static object GetPathComponentValue(object container, PropertyPathPart propertyPathPart)
    {
        if (propertyPathPart.Kind == PropertyPathPartKind.Index)
            return ((IList)container)[propertyPathPart.Index];
        else
            return GetMemberValue(container, propertyPathPart.Name);
    }

    static object GetMemberValue(object container, string name)
    {
        if (container == null)
            return null;
        var type = container.GetType();
        var members = type.GetMember(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < members.Length; ++i)
        {
            if (members[i] is FieldInfo field)
                return field.GetValue(container);
            else if (members[i] is PropertyInfo property)
                return property.GetValue(container);
        }
        return null;
    }
}
