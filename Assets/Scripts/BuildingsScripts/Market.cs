using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Market : Building

{
    [SerializeField] float amount = 100;

    public override void OnPlayerEnter(GameObject player)
    {
        Bisogni bisogni = player.GetComponent<Bisogni>();
        bisogni.Mangia(amount);
}

}


