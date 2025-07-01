using UnityEngine;

public class FamilyHouse :   Building
{
  

    [SerializeField] float amount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        var bisogni = player.GetComponent<Bisogni>();
        bisogni?.Dormi(amount);
    }

    //public override List<ContextAction> GetContextActions(GameObject player)
    //{
    //    return new List<ContextAction>
    //    {
    //        new ContextAction("Compra cibo", p => p.GetComponent<Bisogni>()?.Mangia(amount))
    //    };
    //}
}


