using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ContextMenuManager : MonoBehaviour
{
    public static ContextMenuManager Instance;

    [Header("UI")]
    [SerializeField] GameObject buttonPrefab;      // Prefab del bottone (con Text + Button)
    [SerializeField] Transform menuParent;         // Dove appaiono i bottoni (es. un pannello vuoto)
    GameObject currentMenu;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Tasto destro
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null)
            {
                IContextProvider provider = hit.GetComponent<IContextProvider>();
                if (provider != null && Selectable.Selected != null)
                {
                    var actions = provider.GetContextActions(Selectable.Selected.gameObject);
                    OpenMenu(actions, mousePos);
                    return;
                }
            }

            CloseMenu(); // Click fuori: chiudi
        }
    }

    public void OpenMenu(List<ContextAction> actions, Vector3 worldPosition)
    {
        CloseMenu();

        currentMenu = new GameObject("ContextMenu");
        currentMenu.transform.SetParent(menuParent, false);
        currentMenu.transform.position = Camera.main.WorldToScreenPoint(worldPosition);

        foreach (var action in actions)
        {
            GameObject btnObj = Instantiate(buttonPrefab, currentMenu.transform);
            Button btn = btnObj.GetComponent<Button>();
            Text txt = btnObj.GetComponentInChildren<Text>();
            txt.text = action.label;

            btn.onClick.AddListener(() =>
            {
                GameObject player = Selectable.Selected?.gameObject;
                if (player != null)
                {
                    MovePlayerAndExecute(player, worldPosition, action.onClick);
                    CloseMenu();
                }
            });
        }
    }

    void CloseMenu()
    {
        if (currentMenu != null)
            Destroy(currentMenu);
    }

    void MovePlayerAndExecute(GameObject player, Vector3 destination, System.Action<GameObject> action)
    {
        var mover = player.GetComponent<TopDownMovement>();
        if (mover == null)
        {
            Debug.LogWarning("Nessun TopDownMovement sul personaggio.");
            return;
        }

        mover.MoveTo(destination);
        StartCoroutine(WaitUntilArrived(player, destination, action));
    }

    IEnumerator WaitUntilArrived(GameObject player, Vector3 destination, System.Action<GameObject> action)
    {
        while (Vector2.Distance(player.transform.position, destination) > 0.2f)
        {
            yield return null;
        }

        action?.Invoke(player);
    }
}
