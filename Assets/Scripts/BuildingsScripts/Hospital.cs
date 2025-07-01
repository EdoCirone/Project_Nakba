using UnityEngine;

public class Hospital : Building
{

    [SerializeField] float _amount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        var _lifeController = player.GetComponent<LifeController>();
       _lifeController.AddHp(_amount) ;
    }
    //public override List<ContextAction> GetContextActions(GameObject player)
    //{
    //    return new List<ContextAction>
    //    {
    //        new ContextAction("Compra cibo", p => p.GetComponent<Bisogni>()?.Mangia(amount))
    //    };
    //}

}

