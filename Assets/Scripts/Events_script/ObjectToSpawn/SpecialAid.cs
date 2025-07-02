using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SpecialAid : Aid
{

    public int damageAmount = 10;

    public string groundTag = "Ground";

    public string playerTag = "Player";
    public string npcTag = "NPC";

    public GameObject[] dropItems;

    private bool hasDropped = false;

    public override void Start()
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
    public override void Update()
    {
        base.Update();
        if (!isFalling)
        {
            DropItemsOnGround();
        }
    }

    void StopFalling()
    {
        isFalling = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            InventoryController playerInventory = other.GetComponentInParent<InventoryController>();


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

    private void DropItemsOnGround()
    {
        if (hasDropped) return; // Evita drop multipli
        hasDropped = true;

        if (dropItems == null || dropItems.Length == 0)
        {
            if (itemIDs != null && itemIDs.Length > 0 && itemDictionary != null)
            {
                foreach (Item id in itemIDs)
                {
                    GameObject prefab = itemDictionary.GetItemPrefab(id.ID);
                    if (prefab != null)
                    {
                        Instantiate(prefab, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning($"Item con ID '{id.ID}' non trovato nell'ItemDictionary.");
                    }
                }
            }
        }
        else
        {
            foreach (GameObject drop in dropItems)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
    }
}