using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class Save : MonoBehaviour
{

    public string saveLocation;
    private GameObject player;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        
        player = GameObject.FindWithTag("Family");

        if (player != null)
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        if (player == null) return;

        DataSave saveData = new DataSave()
        {
            playerPosition = player.transform.position
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            DataSave saveData = JsonUtility.FromJson<DataSave>(File.ReadAllText(saveLocation));
            player.transform.position = saveData.playerPosition;
        }
        else
        {
            SaveGame(); 
        }
    }
}
