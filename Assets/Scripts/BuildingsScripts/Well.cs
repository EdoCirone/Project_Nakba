using UnityEngine;
using System.Collections.Generic;

public class Well : Building
{
    [SerializeField] int waterAmount = 1;

    public override void OnPlayerEnter(GameObject player)
    {
        // Il pozzo non ha un comportamento di "ingresso", quindi lasciamo vuoto
    }

    public override List<ContextAction> GetContextActions(GameObject player)
    {
        return new List<ContextAction>
        {
            new ContextAction("Bevi", p => p.GetComponent<Bisogni>()?.Bevi(100)),
            new ContextAction("Raccogli acqua", p =>
            {
                var inventory = Object.FindAnyObjectByType<FamilyInventory>();
                if (inventory != null)
                {
                    inventory.Add("water", waterAmount);
                    Debug.Log($"{p.name} ha raccolto {waterAmount} acqua.");
                }
            })
        };
    }
}
