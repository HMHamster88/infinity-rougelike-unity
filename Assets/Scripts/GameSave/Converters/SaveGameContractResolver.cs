using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

public class SaveGameContractResolver : DefaultContractResolver
{
    private List<string> ignoreFields = new List<string>()
    {
        "name",
        "hideFlags"
    };

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
        return props.Where(p => !ignoreFields.Contains(p.PropertyName)).ToList();
    }
}
