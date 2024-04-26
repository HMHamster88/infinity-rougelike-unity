using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemGenerationRuleConverter : JsonConverter<ItemGenerationRule>
{
    public override ItemGenerationRule ReadJson(JsonReader reader, Type objectType, ItemGenerationRule existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        try
        {
            if (reader.TokenType == JsonToken.String)
            {
                string value = reader.Value?.ToString();
                if (value == null)
                {
                    return null;
                }
                return Resources.Load<ItemGenerationRule>(value);
            }

        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", ex);
        }

        throw new JsonSerializationException($"Unexpected token {reader.TokenType.ToString()} when parsing enum.");
    }

    public override void WriteJson(JsonWriter writer, ItemGenerationRule value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var path = AssetDatabase.GetAssetPath(value).Replace("Assets/Resources/", "").Replace(".asset", "");
        writer.WriteValue(path);
    }
}

