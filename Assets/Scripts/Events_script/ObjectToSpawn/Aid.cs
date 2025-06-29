using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Aid : MonoBehaviour
{
    public Item[] itemIDs; 
    public float fallSpeed = 2f;
    public float minStopTime = 1f;
    public float maxStopTime = 5f;
    public float lifeTime = 10f;

    private bool isFalling = true;
    protected ItemDictionary itemDictionary;

    void Start()
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

    void Update()
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
        if (other.CompareTag("Player"))
        {
            InventoryController playerInventory = other.GetComponent<InventoryController>();

            if (playerInventory != null && itemDictionary != null)
            {
                foreach (Item id in itemIDs)
                {
                    GameObject prefab = itemDictionary.GetItemPrefab(id.ID);
                    if (prefab != null)
                    {
                        playerInventory.AddItem(prefab);
                    }
                    else
                    {
                        Debug.LogWarning($"Item con ID '{id}' non trovato nell'ItemDictionary.");
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
