using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    public string saveLocation;
    private GameObject player;
    private InventoryController inventoryController;

    private const string NewGameFlag = "IsNewGame";

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindAnyObjectByType<InventoryController>();
        
        player = GameObject.FindWithTag("Family");

        bool isNewGame = PlayerPrefs.GetInt(NewGameFlag, 0) == 1;

        if (player != null)
        {
            if (isNewGame)
            {
               
                CreateNewSave();
            }
            else
            {
                LoadGame();
            }
        }
    }

    private void CreateNewSave()
    {
        
        player.transform.position = Vector3.zero; 

        
        if (inventoryController != null)
        {
            inventoryController.ResetInventory();
        }

       
        SaveGame();
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
            CreateNewSave(); 
        }
    }
}
