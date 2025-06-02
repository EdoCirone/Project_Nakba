using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Mosque : Building
{
    [SerializeField] float ammount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        Bisogni bisogni = player.GetComponent<Bisogni>();
        bisogni.Prega(ammount);
}

}
