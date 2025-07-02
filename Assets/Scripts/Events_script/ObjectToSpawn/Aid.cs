using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Aid : MonoBehaviour
{
    public Item[] itemIDs; 
    public float fallSpeed = 2f;
    public float minStopTime = 1f;
    public float maxStopTime = 5f;
    public float lifeTime = 10f;
    [SerializeField] private GameObject menu;
    [SerializeField] private Slot[] itemSlots;
    protected bool isFalling = true;
    protected ItemDictionary itemDictionary;

    private bool itemAlreadyMoved = false;

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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (itemDictionary == null) return;

        if (other.CompareTag("Player"))
        {
            menu.SetActive(true);

            InventoryController playerInventory = other.GetComponentInParent<InventoryController>();

            List<Item> chosenItems = GetRandomItems(itemIDs, 3);

            for (int i = 0; i < chosenItems.Count && i < itemSlots.Length; i++)
            {
                Item item = chosenItems[i];
                Slot targetSlot = itemSlots[i];

                GameObject prefab = itemDictionary.GetItemPrefab(item.ID);
                if (prefab != null)
                {
                    GameObject instance = Instantiate(prefab, targetSlot.transform);
                    instance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    ItemDragHandler dragHandler = instance.GetComponent<ItemDragHandler>();
                    if (dragHandler != null)
                    {
                        dragHandler.SetSourceContainer(this);
                    }

                    targetSlot.currentItem = instance;
                }
                else
                {
                    Debug.LogWarning($"Item con ID '{item.ID}' non trovato nell'ItemDictionary.");
                }
            }
            Destroy(gameObject);
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

    public void NotifyItemMoved()
    {
        if (itemAlreadyMoved) return;

        itemAlreadyMoved = true;
        Destroy(gameObject); 
    }

}
