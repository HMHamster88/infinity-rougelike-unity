using Newtonsoft.Json;
using System;

public class ItemPropertyGenerationRuleConverter : JsonConverter<ItemPropertyGenerationRule>
{
    public override ItemPropertyGenerationRule ReadJson(JsonReader reader, Type objectType, ItemPropertyGenerationRule existingValue, bool hasExistingValue, JsonSerializer serializer)
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
                return new ItemPropertyGenerationRule() { ID = value };
            }

        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", ex);
        }

        throw new JsonSerializationException($"Unexpected token {reader.TokenType.ToString()} when parsing enum.");
    }

    public override void WriteJson(JsonWriter writer, ItemPropertyGenerationRule value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(value.ID);
    }
}

