using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    private bool menuActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu.SetActive(menuActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menuActive = !menuActive;
            menu.SetActive(menuActive);
        }

    }
}
