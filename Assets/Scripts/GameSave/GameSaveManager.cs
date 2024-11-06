using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public string SavesDir = "Saves";
    public string Extension = ".json";

    public GameObject player;

    readonly JsonSerializerSettings settings = new JsonSerializerSettings 
    { 
        TypeNameHandling = TypeNameHandling.All, 
        ContractResolver = new SaveGameContractResolver(), 
        Formatting = Formatting.Indented 
    };

    public void Save(SaveGame saveGame)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, SavesDir, saveGame.Name + Extension);
        Debug.Log("Save game to: " + fullPath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            using (var writer = new StreamWriter(stream))
            {
                var saveGameJson = JsonConvert.SerializeObject(saveGame, settings);
                writer.Write(saveGameJson);
            }
        }
    }

    public SaveGame Load(string fileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, SavesDir, fileName + Extension);

        using (var stream = new FileStream(fullPath, FileMode.Open))
        {
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<SaveGame>(reader.ReadToEnd(), settings);
            }
        }
    }

    public void SaveCurrentGame()
    {
        var saveGame = new SaveGame
        {
            PlayerItems = player.GetComponent<ItemsBag>().itemsSlots,
            PlayerEquipment = EquipmentSaveData.FromEquipment(player.GetComponent<Equipment>())
        };
        Save(saveGame);
    }

    public void LoadCurrentGame()
    {
        var saveGame = Load("SaveGame");
        var itemsBag = player.GetComponent<ItemsBag>();
        itemsBag.itemsSlots = saveGame.PlayerItems;
        var equipment = player.GetComponent<Equipment>();
        saveGame.PlayerEquipment.SetItems(equipment);
    }

}
