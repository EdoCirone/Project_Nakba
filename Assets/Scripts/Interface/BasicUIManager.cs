using UnityEngine;

public class BasicUIManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu UI
    public void OnMenuPress()
    {
        pauseMenu.SetActive(true); // Show the pause menu
        Debug.Log("Menu pressed. Pausing game.");
        Time.timeScale = 0f; // Pause game time
    }




}
