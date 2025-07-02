using UnityEngine;

public class BasicUIManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu UI
    public void OnMenuPress()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause menu is not assigned in the BasicUIManager.");
            return;
        }

        pauseMenu.SetActive(true); // Show the pause menu
        Debug.Log("Menu pressed. Pausing game.");
        Time.timeScale = 0f; // Pause game time
    }

}
