using UnityEngine;
using System.Collections.Generic;

public class Well : Building
{
    [SerializeField] float _amount = 1;

    public override void OnPlayerEnter(GameObject player)
    {
        var bisogni = player.GetComponent<Bisogni>();
        bisogni?.Bevi(_amount);
    }

    //public override List<ContextAction> GetContextActions(GameObject player)
    //{
    //    return new List<ContextAction>
    //    {
    //        new ContextAction("Bevi", p => p.GetComponent<Bisogni>()?.Bevi(100)),
    //        new ContextAction("Raccogli acqua", p => Object.FindAnyObjectByType<FamilyInventory>()?.Add("water", waterAmount))        };
    //}
}