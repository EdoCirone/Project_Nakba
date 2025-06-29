using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SpecialAid : Aid
{
   
    public int damageAmount = 10;

    public string groundTag = "Ground";

    public string playerTag = "Player";
    public string npcTag = "NPC";

    public GameObject[] dropItems;

    public override void Update()
    {
        base.Update();
        if(!isFalling)
        {
            DropItemsOnGround();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        
       
        if (other.CompareTag(playerTag) || other.CompareTag(npcTag))
        {
            LifeController healthComponent = other.GetComponent<LifeController>();
            if (healthComponent != null)
            {
                healthComponent.AddHp(-damageAmount);
            }
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
        }
    }

    private void DropItemsOnGround()
    {
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
                        Debug.LogWarning($"Item con ID '{id}' non trovato nell'ItemDictionary.");
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