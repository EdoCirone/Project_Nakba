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
            Bisogni eat = Selectable.Selected.GetComponent<Bisogni>();
            if (eat != null)
            {
                eat.Bevi(satisfaction);
                Debug.Log("Pane usato su " + eat.gameObject.name);
            }
        }
    }

}
