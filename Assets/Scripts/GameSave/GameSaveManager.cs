using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public string SavesDir = "Saves";
    public string Extension = ".json";

    public void Save(SaveGame saveGame)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, SavesDir, saveGame.Name + Extension);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            using (var writer = new StreamWriter(stream))
            {
                var saveGameJson = JsonUtility.ToJson(saveGame);
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
                return JsonUtility.FromJson<SaveGame>(reader.ReadToEnd());
            }
        }
    }
}
