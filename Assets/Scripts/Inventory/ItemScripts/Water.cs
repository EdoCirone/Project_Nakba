using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class Water : Item, IPointerClickHandler
{
    public float satisfaction = 10f;
    Bisogni drink;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            Bisogni drink = Selectable.Selected.GetComponent<Bisogni>();
            if (drink != null)
            {
                drink.Bevi(satisfaction);
                Debug.Log("Pane usato su " + drink.gameObject.name);
            }
        }
    }

}
