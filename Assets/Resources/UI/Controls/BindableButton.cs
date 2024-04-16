using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[UxmlElement]
public partial class BindableButton : Button
{
    [UxmlAttribute]
    public string OnClickDataSourceMethodName;

    public BindableButton() 
    {
        this.clickable.clicked += () =>
        {
            if (OnClickDataSourceMethodName != null)
            {
                var dataSource = this.GetDataSourceWithPath();
                if (dataSource != null)
                {
                    var type = dataSource.GetType();
                    var method = type.GetMethod(OnClickDataSourceMethodName);
                    method.Invoke(dataSource, null);
                }
            }

        };
    }
}
