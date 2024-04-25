using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

public class ItemSlotSaveData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public ItemSlot.Type Type;
    public ItemSaveData Item;
}
