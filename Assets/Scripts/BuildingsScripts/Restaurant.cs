using UnityEngine;
using System.Collections.Generic;

public class Restaurant : Building
{
    [SerializeField] float _amount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        var bisogni = player.GetComponent<Bisogni>();
        bisogni?.Mangia(_amount);
    }

    //public override List<ContextAction> GetContextActions(GameObject player)
    //{
    //    return new List<ContextAction>
    //    {
    //        new ContextAction("Compra cibo", p => p.GetComponent<Bisogni>()?.Mangia(amount))
    //    };
    //}
}
