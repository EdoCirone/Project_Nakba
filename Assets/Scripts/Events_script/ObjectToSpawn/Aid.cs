using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aid : MonoBehaviour
{
    public Item[] itemIDs;
    public float fallSpeed = 2f;
    public float minStopTime = 1f;
    public float maxStopTime = 5f;
    public float lifeTime = 10f;
    private GameObject menu;

    [SerializeField] private Slot[] itemSlots;
    

    protected bool isFalling = true;
    protected ItemDictionary itemDictionary;

    private bool itemAlreadyMoved = false;
    private bool hasTriggered = false;

    private void Awake()
    {
        menu = GameObject.FindWithTag("AidMenu");
        
        if (menu == null)
        {
            Debug.LogError("GameObject con tag 'AidMenu' non trovato!");
            return;
      
        }


        menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public virtual void Start()
    {
       

        float stopTime = Random.Range(minStopTime, maxStopTime);
        Invoke(nameof(StopFalling), stopTime);

        itemDictionary = FindAnyObjectByType<ItemDictionary>();
        if (itemDictionary == null)
        {
            Debug.LogError("ItemDictionary non trovato nella scena!");
        }

        Destroy(gameObject, lifeTime);
    }

    public virtual void Update()
    {
        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    void StopFalling()
    {
        isFalling = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        if (menu == null || itemDictionary == null) return;

        menu.SetActive(true);
        Time.timeScale = 0f;

        InventoryController playerInventory = GameObject.FindWithTag("Player")?.GetComponentInParent<InventoryController>();
        List<Item> chosenItems = GetRandomItems(itemIDs, 3);

        if (menu.transform.childCount == 0)
        {
            for (int i = 0; i < chosenItems.Count && i < itemSlots.Length; i++)
            {
                Item item = chosenItems[i];
                Slot targetSlot = Instantiate(itemSlots[i],menu.transform).GetComponent<Slot>();


                GameObject prefab = itemDictionary.GetItemPrefab(item.ID);
                if (prefab != null)
                {
                    GameObject instance = Instantiate(prefab, targetSlot.transform);
                    instance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    ItemDragHandler dragHandler = instance.GetComponent<ItemDragHandler>();
                    if (dragHandler != null)
                    {
                        dragHandler.SetSourceContainer(this); // Permette il callback NotifyItemMoved
                    }

                    targetSlot.currentItem = instance;
                }
                else
                {
                    Debug.LogWarning($"Item con ID '{item.ID}' non trovato nell'ItemDictionary.");
                }
            }
        }
    }

    public void NotifyItemMoved()
    {
        if (itemAlreadyMoved) return;

        itemAlreadyMoved = true;
        CloseMenu();
        Destroy(gameObject);
    }

    private void CloseMenu()
    {
        if (menu != null)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private List<Item> GetRandomItems(Item[] array, int count)
    {
        List<Item> result = new List<Item>();
        List<Item> pool = new List<Item>(array);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return result;
    }
}
