using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public string SavesDir = "Saves";
    public string Extension = ".json";

    public GameObject player;

    ItemSaveDataConverter itemConverter = new ItemSaveDataConverter();


    public void Save(SaveGame saveGame)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, SavesDir, saveGame.Name + Extension);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            using (var writer = new StreamWriter(stream))
            {
                var saveGameJson = JsonConvert.SerializeObject(saveGame);
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
                return JsonConvert.DeserializeObject<SaveGame>(reader.ReadToEnd());
            }
        }
    }

    public void SaveCurrentGame()
    {
        var itemConverter = new ItemSaveDataConverter();
        var saveGame = new SaveGame
        {
            PlayerItemsBag = itemConverter.ToItemsBagSaveData(player.GetComponent<ItemsBag>())
        };
        Save(saveGame);
    }

    public void LoadCurrentGame()
    {
        var saveGame = Load("SaveGame");
        itemConverter.FillItemsBag(player.GetComponent<ItemsBag>(), saveGame.PlayerItemsBag);
    }

}
