using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGenerationRuleConverter : JsonConverter<ItemGenerationRule>
{
    private const string allRulesPath = "ScriptableObjects/ItemGenerationRules";
    private Dictionary<string, ItemGenerationRule> allRules;
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
                if (allRules == null)
                {
                    allRules = Resources.LoadAll<ItemGenerationRule>(allRulesPath).ToDictionary(rule => rule.ID, rule => rule);
                }
                var rule = allRules[value];
                if (rule == null)
                {
                    throw new Exception("No ItemGenerationRule with id = " + value);
                }
                return rule;
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
        writer.WriteValue(value.ID);
    }
}

