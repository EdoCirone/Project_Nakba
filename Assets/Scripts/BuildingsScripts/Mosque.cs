using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Mosque : Building
{
    [SerializeField] float amount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        Bisogni bisogni = player.GetComponent<Bisogni>();
        bisogni.Prega(amount);
    }
    public override List<ContextAction> GetContextActions(GameObject player)
    {
        return new List<ContextAction>
        {
            new ContextAction("Compra cibo", p => p.GetComponent<Bisogni>()?.Mangia(amount))
        };
    }

}
