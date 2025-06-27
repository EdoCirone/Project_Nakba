using UnityEngine;
using System.Collections.Generic;

public class Well : Building
{
    [SerializeField] int waterAmount = 1;

    public override void OnPlayerEnter(GameObject player)
    {
        // Vuoto per ora
    }

    public override List<ContextAction> GetContextActions(GameObject player)
    {
        return new List<ContextAction>
        {
            new ContextAction("Bevi", p => p.GetComponent<Bisogni>()?.Bevi(100)),
            new ContextAction("Raccogli acqua", p => Object.FindAnyObjectByType<FamilyInventory>()?.Add("water", waterAmount))        };
    }
}