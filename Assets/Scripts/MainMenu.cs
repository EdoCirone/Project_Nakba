using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public Button loadGameButton;
    public GameObject Menu;

    private string savePath;
    private const string NewGameFlag = "IsNewGame";

    void Start()
    {

        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        loadGameButton.interactable = SaveExists();
        PlayerPrefs.DeleteKey(NewGameFlag);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt(NewGameFlag, 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    public void LoadGame()
    {
        PlayerPrefs.DeleteKey(NewGameFlag);
        PlayerPrefs.Save();

        if (SaveExists())
        {
            SceneManager.LoadScene("MainGameScene");
        }
        else
        {
            Debug.LogWarning("Nessun salvataggio trovato!");
            loadGameButton.interactable = false;
        }
    }

    public void OpenOptions()
    {
        Menu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SaveOptions()
    {
        Debug.Log("Le opzioni sono state salvate");
        // Qui è da aggiungere la logica per salvare le opzioni
    }

    public void ReturnToMenu()
    { 
        Menu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
      
    }

    public void OpenCredits()
    {
        Menu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private bool SaveExists()
    {
        if (!File.Exists(savePath)) return false;

        try
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJson<DataSave>(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}
