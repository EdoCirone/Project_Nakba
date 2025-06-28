using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    public string saveLocation;
    private GameObject player;
    private InventoryController inventoryController;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindAnyObjectByType<InventoryController>();
        
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
            playerPosition = player.transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };


        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            DataSave saveData = JsonUtility.FromJson<DataSave>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Family").transform.position = saveData.playerPosition;
            inventoryController.SetInventoryItem(saveData.inventorySaveData);
        }
        else
        {
            SaveGame(); 
        }
    }
}
