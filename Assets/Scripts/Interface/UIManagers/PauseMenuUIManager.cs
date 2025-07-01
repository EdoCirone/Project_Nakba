using UnityEngine;

public class PauseMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Reference to the pause menu UI
    public void OnMenuXpress()
    {

        Debug.Log("Menu exited. Returning to game.");
        pauseMenu.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume game time
    }

    public void OnMenuExitPress()
    {
        Debug.Log("Menu exited. Quitting game.");
        Application.Quit(); // Quit the application
    }

    public void OnMenuSavePress()
    {
        Debug.Log("Menu save pressed. Saving game state.");
        // Implement your save logic here
        // For example, you might call a SaveManager method to save the game state
    }

    public void OnMenuLoadPress()
    {
        Debug.Log("Menu load pressed. Loading game state.");
        // Implement your load logic here
        // For example, you might call a SaveManager method to load the game state
    }

    public void OnMenuSettingsPress()
    {
        Debug.Log("Menu settings pressed. Opening settings menu.");
        // Implement your settings logic here
        // For example, you might open a settings panel or change game settings
    }

    public void OnMenuMainMenuPress()
    {
        Debug.Log("Menu main menu pressed. Returning to main menu.");
        // Implement your logic to return to the main menu
        // For example, you might load a main menu scene or reset the game state
    }
}
