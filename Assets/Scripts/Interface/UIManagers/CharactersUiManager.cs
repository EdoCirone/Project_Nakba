using UnityEngine;

public class CharactersUiManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterUI; // Reference to the pause menu UI
    public void OnMenuXpress()
    {

        Debug.Log("Menu exited. Returning to game.");
        CharacterUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume game time
    }

}
